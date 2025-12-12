namespace servweb1_t2.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Loan>? Loans { get; set; }

        public Book(string title, string author, string isbn, int stock)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            Stock = stock;
        }
        
        public void DecreaseStock()
        {
            if (Stock <= 0)
            {
                throw new Exceptions.BookNoStockException(Id, Title);
            }
            Stock--;
        }

        public void IncreaseStock()
        {
            Stock++;
        }
    }

}