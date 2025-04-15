namespace Domain.Common
{
    public class AuditableBaseClass
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
