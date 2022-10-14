using AppBackEnd.Domain.Models;

namespace AppBackEnd.Domain.IRepositories
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}
