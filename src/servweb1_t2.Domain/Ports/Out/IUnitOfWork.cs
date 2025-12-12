namespace servweb1_t2.Domain.Ports.Out
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BooksRepository { get; }
        ILoanRepository LoansRepository { get; }
        
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}