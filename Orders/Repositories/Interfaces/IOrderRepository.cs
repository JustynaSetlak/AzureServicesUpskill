using Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
    }
}
