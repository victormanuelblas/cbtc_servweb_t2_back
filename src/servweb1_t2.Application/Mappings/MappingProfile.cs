using AutoMapper;
using servweb1_t2.Application.DTOs.Book;
using servweb1_t2.Application.DTOs.Loan;
using servweb1_t2.Domain.Entities;

namespace servweb1_t2.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();

            CreateMap<CreateLoanDto, Loan>().ReverseMap();
            CreateMap<Loan, LoanDto>().ReverseMap();

            CreateMap<UpdateLoanDto, Loan>().ReverseMap();
        }
    }
}