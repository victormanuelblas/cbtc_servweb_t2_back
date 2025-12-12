namespace servweb1_t2.Application.DTOs.Loan
{
    public class UpdateLoanDto
    {
        public int BookId { get; set; }
        public string StudentName { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}