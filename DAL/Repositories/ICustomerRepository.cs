using CW2.DAL.Entities;

namespace CW2.DAL.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        IList<Customer> GetAll();
        Customer? GetById(long customerId);
        Customer Insert(Customer customer);
        void Update(Customer customer);
        void Delete(long customerId);
        PagedResult<Customer> Filter
            (
             string firstName, 
             string lastName, 
             string postalCode,
             string city, 
             string street,
             string flatNo,
             string buildingNo,
             string phoneNumber,
             int pageNumber, 
             int pageSize, 
             string sortColumn, 
             bool sortDesc = true
            );

        void IDisposable.Dispose()
        {
            // no op implementation
        }
    }
}
