using Azure;
using System.Data;
using System.IO;
using CW2.DAL.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using CW2.Models;

namespace CW2.DAL.Repositories
{
    public class OrderSPDapperRepository : IOrderRepository
    {

        private readonly string _connStr;

        public OrderSPDapperRepository(string connStr)
        {
            _connStr = connStr;
        }
        public void Delete(long orderId)
        {
            throw new NotImplementedException();
        }

        public PagedResult<OrderJoinViewModel> Filter(
            string customerName,
            DateTime orderDateFrom,
            string state,
            string shipperName,
            int page,
            int pageSize,
            string sortColumn,
            bool sortDesc)
        {
            using var conn = new SqlConnection(_connStr);

            if (page < 1)
            {
                page = 1;
            }

            int bitValue = sortDesc ? 1 : 0;

            var p = new DynamicParameters();
            p.Add("CustomerName", customerName);
            p.Add("OrderDateFrom", orderDateFrom);
            p.Add("State", state);
            p.Add("ShipperName", shipperName);
            p.Add("Page", page);
            p.Add("PageSize", pageSize);
            p.Add("SortColumn", sortColumn);
            p.Add("SortDesc", bitValue);
            p.Add("TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var orders = conn.Query<OrderJoinViewModel>("dbo.udpGetFilteredOrders", p, commandType: CommandType.StoredProcedure).ToList();

            int totalCount = p.Get<int>("TotalCount");

            return new PagedResult<OrderJoinViewModel>
            {
                Items = orders,
                TotalCount = totalCount
            };
        }

        public IList<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order? GetById(long customerId)
        {
            throw new NotImplementedException();
        }

        public Order Insert(Order order)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
