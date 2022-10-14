using AppBackEnd.Domain.Models;

namespace AppBackEnd.Domain.IServices
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}
