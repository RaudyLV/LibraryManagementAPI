
namespace Domain.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public DateTime LoanDate { get; set; }
        public bool isReturned { get; set; }
        public DateTime ReturnDate { get; set; }

        //FK's
        public Book Book { get; set; }
        public int BookId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
