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
            var loan = _mapper.Map<Loan>(createLoanDto);
            await _unitOfWork.LoansRepository.SaveLoanAsync(loan);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<LoanDto>(loan);
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
            _unitOfWork.LoansRepository.DeleteLoanAsync(loan);
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
    }
}