namespace servweb1_t2.Application.DTOs.Loan
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string StudentName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}