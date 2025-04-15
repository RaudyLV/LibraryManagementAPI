namespace Application.Interfaces
{
    public interface ITransaction : IDisposable
    {
        Task BeginTransactionAsync();
        void Commit();
        void RollBack();
    }
}
