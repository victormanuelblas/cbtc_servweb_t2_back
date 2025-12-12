namespace servweb1_t2.Application.DTOs.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}