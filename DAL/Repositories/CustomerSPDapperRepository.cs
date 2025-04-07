using System.Data;
using Azure;
using CW2.DAL.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CW2.DAL.Repositories
{
    public class CustomerSPDapperRepository : ICustomerRepository
    {
        private readonly string _connStr;

        public CustomerSPDapperRepository(string connStr)
        {
            _connStr = connStr;
        }

         public void Delete(long customerId)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Execute("dbo.udpDeleteCustomer", new { customerId });
        }

        public PagedResult<Customer> Filter(
            string firstName, 
            string lastName, 
            string postalCode, 
            string city, 
            string street, 
            string flatNo, 
            string buildingNo, 
            string phoneNumber,
            int page, 
            int pageSize, 
            string sortColumn, 
            bool sortDesc
            )
        {
            using var conn = new SqlConnection(_connStr);

            if (page < 1)
            {
                page = 1;
            }


            int bitValue = sortDesc ? 1 : 0;

            var p = new DynamicParameters();
            p.Add("FirstName", firstName);
            p.Add("LastName", lastName);
            p.Add("PostalCode", postalCode);
            p.Add("Street", street);
            p.Add("BuildingNo", buildingNo);
            p.Add("FlatNo", flatNo);
            p.Add("City", city);
            p.Add("PhoneNumber", phoneNumber);
            p.Add("Page", page);
            p.Add("PageSize", pageSize);
            p.Add("SortColumn", sortColumn);
            p.Add("SortDesc", bitValue);
            p.Add("TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var customers = conn.Query<Customer>("dbo.udpFilterCustomers", p, commandType: CommandType.StoredProcedure).ToList();

            int totalCount = p.Get<int>("TotalCount");

            return new PagedResult<Customer>
            {
                Items = customers,
                TotalCount = totalCount
            };
        }

        public IList<Customer> GetAll()
        {
            using var conn = new SqlConnection(_connStr);
            return conn.Query<Customer>("dbo.udpGetAllCustomers").ToList();
        }

        public Customer? GetById(long customerId)
        {
            using var conn = new SqlConnection(_connStr);
            return conn.QueryFirstOrDefault<Customer>("dbo.udpGetCustomerById", new { CustomerId = customerId });
        }

        public Customer Insert(Customer customer)
        {
            var p = new DynamicParameters();

            p.Add("FirstName", customer.FirstName, DbType.String);
            p.Add("LastName", customer.LastName, DbType.String);
            p.Add("BirthDate", customer.BirthDate, DbType.Date);
            p.Add("ProfilePic", customer.ProfilePic, DbType.Binary, ParameterDirection.Input);
            p.Add("Login", customer.Login, DbType.String);
            p.Add("PasswordHash", customer.PasswordHash, DbType.String);
            p.Add("PostalCode", customer.PostalCode, DbType.String);
            p.Add("Street", customer.Street, DbType.String);
            p.Add("BuildingNo", customer.BuildingNo, DbType.String);
            p.Add("FlatNo", customer.FlatNo, DbType.String);
            p.Add("City", customer.City, DbType.String);
            p.Add("PhoneNumber", customer.PhoneNumber, DbType.String);
            p.Add("@AcceptsMarketing", customer.AcceptsMarketing, dbType: DbType.Boolean); 

            p.Add("@CustomerId", dbType: DbType.Int64, direction: ParameterDirection.Output);
            p.Add("@Error", dbType: DbType.String, size: 2000, direction: ParameterDirection.Output);
            p.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = new SqlConnection(_connStr);
            conn.Execute("dbo.udpCreateCustomer", p, commandType: CommandType.StoredProcedure);

            // Retrieve outputs
            customer.CustomerId = p.Get<long>("@CustomerId");
            var error = p.Get<string>("@Error");
            var statusCode = p.Get<int>("@StatusCode");

            if (statusCode > 1)
            {
                throw new Exception($"Cannot create customer, code {statusCode}, error: {error}");
            }

            return customer;
        }


        public void Update(Customer customer)
        {
            var p = new DynamicParameters();
            p.Add("CustomerId", customer.CustomerId, DbType.Int64);
            p.Add("FirstName", customer.FirstName, DbType.String);
            p.Add("LastName", customer.LastName, DbType.String);
            p.Add("BirthDate", customer.BirthDate, DbType.Date);
            p.Add("ProfilePic", customer.ProfilePic, DbType.Binary, ParameterDirection.Input);
            p.Add("Login", customer.Login, DbType.String);
            p.Add("PasswordHash", customer.PasswordHash, DbType.String);
            p.Add("PostalCode", customer.PostalCode, DbType.String);
            p.Add("Street", customer.Street, DbType.String);
            p.Add("BuildingNo", customer.BuildingNo, DbType.String);
            p.Add("FlatNo", customer.FlatNo, DbType.String);
            p.Add("City", customer.City, DbType.String);
            p.Add("PhoneNumber", customer.PhoneNumber, DbType.String);
            p.Add("@AcceptsMarketing", customer.AcceptsMarketing, dbType: DbType.Boolean); 

            p.Add("Error", dbType: DbType.String, size: 2000, direction: ParameterDirection.Output);
            p.Add("StatusCode", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = new SqlConnection(_connStr);
            conn.Execute("dbo.udpUpdateCustomer", p);

            string error = p.Get<string>("Error");
            int statusCode = p.Get<int>("StatusCode");

            if (statusCode > 1)
            {
                throw new Exception($"Can not change customer, code {statusCode}, error: {error}");
            }
        }
    }
}
