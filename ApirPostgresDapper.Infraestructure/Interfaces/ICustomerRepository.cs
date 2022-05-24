using ApiPostgresDapper.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<bool> Add(Customer entity);
        Task<bool> Update(Customer entity);
        Task<bool> Delete(int id);
    }
}
