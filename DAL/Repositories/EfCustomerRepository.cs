using CW2.DAL.EF;
using CW2.DAL.Entities;

namespace CW2.DAL.Repositories
{
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly BookStoreDbContext _dbContext;

        public EfCustomerRepository(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(long customerId)
        {
            var customer = _dbContext.Customers.Find(customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }
                
        }

        public PagedResult<Customer> Filter(string firstName, string lastName, string postalCode, string city, string street, string flatNo, string buildingNo, string phoneNumber, int pageNumber, int pageSize, string sortColumn, bool sortDesc = true)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer? GetById(long customerId)
        {
            return _dbContext.Customers.Find(customerId);
        }

        public Customer Insert(Customer customer)
        {
            var insertedCustomer = _dbContext.Add(customer).Entity;
            _dbContext.SaveChanges();

            return insertedCustomer;
        }

        public void Update(Customer customer)
        {
            _dbContext.Update(customer);
            _dbContext.SaveChanges();
        }
    }
}
