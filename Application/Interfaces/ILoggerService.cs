
namespace Application.Interfaces
{
    public interface ILoggerService<T> 
    {
        public void LogInformation(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}
