using AutoMapper;
using servweb1_t2.Application.DTOs.Loan;
using servweb1_t2.Domain.Entities;
using servweb1_t2.Application.Interfaces;
using servweb1_t2.Domain.Exceptions;
using servweb1_t2.Domain.Ports.Out;

namespace servweb1_t2.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _unitOfWork.LoansRepository.GetAllLoansAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }
       
        public async Task<LoanDto> GetLoanByIdAsync(int id)
        {
            var loan = await _unitOfWork.LoansRepository.GetLoanByIdAsync(id);
            if (loan == null)
            {
                throw new NotFoundException(id,"Loan");
            }
            return _mapper.Map<LoanDto>(loan);
        }
        public async Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(createLoanDto.BookId);
                if (book == null)
                {
                    throw new NotFoundException(createLoanDto.BookId, "Book");
                }
                if (book.Stock <= 0)
                {
                    throw new BookNoStockException(book.Id, book.Title);
                }

                // decrease stock and persist
                book.DecreaseStock();
                await _unitOfWork.BooksRepository.UpdateBookAsync(book);

                var loan = _mapper.Map<Loan>(createLoanDto);
                await _unitOfWork.LoansRepository.SaveLoanAsync(loan);

                await _unitOfWork.CommitTransactionAsync();
                return _mapper.Map<LoanDto>(loan);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<LoanDto> UpdateLoanAsync(int id, UpdateLoanDto updateLoanDto)
        {
            var loan = await _unitOfWork.LoansRepository.GetLoanByIdAsync(id);
            if (loan == null)
            {
                throw new NotFoundException(id,"Loan");
            }
            _mapper.Map(updateLoanDto, loan);
            await _unitOfWork.LoansRepository.UpdateLoanAsync(loan);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<LoanDto>(loan);
        }
        public async Task<bool> DeleteLoanAsync(int id)
        {
            var loan = await _unitOfWork.LoansRepository.GetLoanByIdAsync(id);
            if (loan == null)
            {
                throw new NotFoundException(id,"Loan");
            }
            await _unitOfWork.LoansRepository.DeleteLoanAsync(loan);
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }

        public async Task ReturnLoanAsync(int id)
        {
            var loan = await _unitOfWork.LoansRepository.GetLoanByIdAsync(id);
            if (loan == null)
            {
                throw new NotFoundException(id, "Loan");
            }

            // If already returned, no-op or throw
            if (loan.ReturnDate != null || loan.Status == "Returned")
            {
                throw new DomainException("Loan already returned");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                loan.ReturnDate = DateTime.Now;
                loan.Status = "Returned";
                await _unitOfWork.LoansRepository.UpdateLoanAsync(loan);

                var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(loan.BookId);
                if (book != null)
                {
                    book.IncreaseStock();
                    await _unitOfWork.BooksRepository.UpdateBookAsync(book);
                }

                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}