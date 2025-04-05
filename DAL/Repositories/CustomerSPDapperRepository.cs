using System.Data;
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
            p.AddDynamicParams(
                new
                {
                    customer.FirstName,
                    customer.LastName,
                    customer.ProfilePic,
                    customer.Login,
                    customer.PasswordHash,
                    customer.PostalCode,
                    customer.Street,
                    customer.BuildingNo,
                    customer.FlatNo,
                    customer.City,
                    customer.Tin,
                    customer.PhoneNumber
                }
            );
            p.Add("Error",
                dbType: System.Data.DbType.String,
                size: 2000,
                direction: System.Data.ParameterDirection.Output
            );
            p.Add("StatusCode",
                dbType: System.Data.DbType.Int32,
                direction: System.Data.ParameterDirection.ReturnValue
            );

            using var conn = new SqlConnection(_connStr);
            customer.CustomerId = conn.ExecuteScalar<int>("dbo.udpCreateCustomer", p);

            string error = p.Get<string>("Error");
            int statusCode = p.Get<int>("StatusCode");

            if (statusCode > 1)
            {
                throw new Exception($"Can not create customer, code {statusCode}, error: {error}");
            }

            return customer;
        }

        public void Update(Customer customer)
        {
            var p = new DynamicParameters();
            p.Add("CustomerId", customer.CustomerId, DbType.Int32);  // Ensure ID is passed
            p.Add("FirstName", customer.FirstName, DbType.String);
            p.Add("LastName", customer.LastName, DbType.String);
            p.Add("ProfilePic", customer.ProfilePic, DbType.Binary, ParameterDirection.Input);
            p.Add("Login", customer.Login, DbType.String);
            p.Add("PasswordHash", customer.PasswordHash, DbType.String);
            p.Add("PostalCode", customer.PostalCode, DbType.String);
            p.Add("Street", customer.Street, DbType.String);
            p.Add("BuildingNo", customer.BuildingNo, DbType.String);
            p.Add("FlatNo", customer.FlatNo, DbType.String);
            p.Add("City", customer.City, DbType.String);
            p.Add("TIN", customer.Tin, DbType.String);
            p.Add("PhoneNumber", customer.PhoneNumber, DbType.String);

            p.Add("Error", dbType: DbType.String, size: 2000, direction: ParameterDirection.Output);
            p.Add("StatusCode", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


            using var conn = new SqlConnection(_connStr);
            conn.Execute("dbo.udpUpdateCustomer", p);

            string error = p.Get<string>("Error");
            int statusCode = p.Get<int>("StatusCode");

            if (statusCode > 1)
            {
                throw new Exception($"Can not create customer, code {statusCode}, error: {error}");
            }
        }
    }
}
