using Microsoft.EntityFrameworkCore.Storage;
using servweb1_t2.Domain.Ports.Out;
using servweb1_t2.Infrastructure.Persistence.Context;

namespace servweb1_t2.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public IBookRepository BooksRepository { get; }
        public ILoanRepository LoansRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
                        IBookRepository bookRepository,
                        ILoanRepository loanRepository)
        {
            _context = context;
            BooksRepository = bookRepository;
            LoansRepository = loanRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }
    }
}