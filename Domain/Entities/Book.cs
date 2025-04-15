using Domain.Common;

namespace Domain.Entities
{
    public class Book : AuditableBaseClass
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; private set; }
        public List<Loan> Loans { get; set; } = new List<Loan>();

        public void InitializeStock(int initialStock)
        {
            if (initialStock < 0)
            {
                throw new ArgumentException("El stock inicial no puede ser negativo.");
            }
            Stock = initialStock;
        }
    }
}
