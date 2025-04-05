using CW2.DAL.Entities;

namespace CW2.DAL.Repositories
{
    public interface ICustomerRepository
    {
        IList<Customer> GetAll();
        Customer? GetById(long customerId);
        Customer Insert(Customer customer);
        void Update(Customer customer);
        void Delete(long customerId);
    }
}
