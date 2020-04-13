using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalChow.Domain.Repository.OrderRepository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(LocalChowDbContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return FindAll();
        }

        public Order GetOrderById(int id)
        {
            return FindByCondition(x => x.OrderID == id).FirstOrDefault();
        }
    }
}
