namespace servweb1_t2.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Active"; // Active | Returned
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relación
        public Book? Book { get; set; }    
    }
}