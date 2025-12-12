using servweb1_t2.Application.DTOs.Loan;

namespace servweb1_t2.Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto);
        Task<LoanDto> GetLoanByIdAsync(int id);
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<LoanDto> UpdateLoanAsync(int id, UpdateLoanDto updateLoanDto);
        Task<bool> DeleteLoanAsync(int id);
    }
}
