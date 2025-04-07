using CW2.DAL.Entities;
using CW2.Models;

namespace CW2.DAL.Repositories
{
    public interface IOrderRepository
    {
        IList<Order> GetAll();
        Order? GetById(long customerId);
        Order Insert(Order order);
        void Update(Order order);
        void Delete(long orderId);
        PagedResult<OrderJoinViewModel> Filter
            (
             string customerName,
             DateTime orderDateFrom,
             string state,
             string shipperName,
             int page,
             int pageSize,
             string sortColumn,
             bool sortDesc
            );

    }
}
