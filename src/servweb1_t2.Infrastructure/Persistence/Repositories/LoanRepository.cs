using Microsoft.EntityFrameworkCore;
using servweb1_t2.Domain.Entities;
using servweb1_t2.Domain.Ports.Out;
using servweb1_t2.Infrastructure.Persistence.Context;

namespace servweb1_t2.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> GetLoanByIdAsync(int loanId)
        {
            return await _context.Loans.FindAsync(loanId);
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task SaveLoanAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLoanAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }
    }
}