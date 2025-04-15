using Domain.Common;

namespace Domain.Entities
{
    public class User : AuditableBaseClass
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Loan> Loans { get; private set; }

        public User()
        {
            Loans = new List<Loan>();
        }

        public void AddLoan(Loan loan) => Loans.Add(loan);
        public void RemoveLoan(Loan loan) => Loans.Remove(loan);

    }
}
