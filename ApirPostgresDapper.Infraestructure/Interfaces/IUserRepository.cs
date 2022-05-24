using ApiPostgresDapper.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> Add(User entity);
        Task<bool> Update(User entity);
        Task<bool> Delete(int id);
    }
}
