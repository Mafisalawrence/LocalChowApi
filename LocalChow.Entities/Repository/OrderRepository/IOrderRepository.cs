using LocalChow.Persistence.Models;
using LocalChow.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.Repository.OrderRepository
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}
