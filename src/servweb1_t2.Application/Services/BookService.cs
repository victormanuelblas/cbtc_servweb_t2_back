using AutoMapper;
using servweb1_t2.Application.DTOs.Book;
using servweb1_t2.Domain.Entities;
using servweb1_t2.Application.Interfaces;
using servweb1_t2.Domain.Exceptions;
using servweb1_t2.Domain.Ports.Out;

namespace servweb1_t2.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.BooksRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new NotFoundException(id,"Book");
            }
            return _mapper.Map<BookDto>(book);
        }   
        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var existingBook = await _unitOfWork.BooksRepository.GetBookByIsbnAsync(createBookDto.Isbn);
            if (existingBook != null)
            {
                throw new DuplicateEntityException("Loan","Isbn",createBookDto.Isbn);
            }
            var book = _mapper.Map<Book>(createBookDto);
            await _unitOfWork.BooksRepository.SaveBookAsync(book);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<BookDto>(book);
        }
        public async Task<BookDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new NotFoundException(id,"Book");
            }
            _mapper.Map(updateBookDto, book);
            _unitOfWork.BooksRepository.UpdateBookAsync(book);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<BookDto>(book);
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new NotFoundException(id,"Book");   
            }
            _unitOfWork.BooksRepository.DeleteBookAsync(book);
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
    }
}
