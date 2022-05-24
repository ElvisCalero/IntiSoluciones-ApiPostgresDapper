using ApiPostgresDapper.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task<int> Add(Order entity);
        Task<bool> Update(Order entity);
        Task<bool> Delete(int id);
    }
}
