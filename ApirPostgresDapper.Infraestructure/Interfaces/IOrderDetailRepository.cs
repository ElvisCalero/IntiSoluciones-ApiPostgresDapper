using ApiPostgresDapper.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAll();
        Task<IEnumerable<OrderDetail>> GetById(int id);
        Task<bool> Add(OrderDetail entity);
        Task<bool> Update(OrderDetail entity);
        Task<bool> Delete(int id);
    }
}
