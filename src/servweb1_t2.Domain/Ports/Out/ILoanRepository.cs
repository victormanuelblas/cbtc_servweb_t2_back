using servweb1_t2.Domain.Entities;

namespace servweb1_t2.Domain.Ports.Out
{
    public interface ILoanRepository
    {
        Task<Loan> GetLoanByIdAsync(int loanId);
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task SaveLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task DeleteLoanAsync(Loan loan);
    }
}