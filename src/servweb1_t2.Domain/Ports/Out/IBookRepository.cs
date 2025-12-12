using servweb1_t2.Domain.Entities;

namespace servweb1_t2.Domain.Ports.Out
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task SaveBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task<Book> GetBookByIsbnAsync(string isbn);
        Task SaveArticuloBajaAsync(Entities.ArticuloBaja articuloBaja);
        Task SaveArticuloLiquidacionAsync(Entities.ArticuloLiquidacion articuloLiquidacion);
    }
}