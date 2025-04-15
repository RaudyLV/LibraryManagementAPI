using Application.DTOs.Users;
using Application.Wrrappers;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticateRequest request, string ipAdress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
    }
}
