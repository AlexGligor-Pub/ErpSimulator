using Domain.Entities.DTO.Response;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly SqlDbContext _dbContext;

        public OrderRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderGroupResponse>> GetOrdersGroupedByPriorityAsync()
        {
            return await _dbContext.UnsOrders
           .GroupBy(o => o.Priority)
           .Select(g => new OrderGroupResponse
           {
               Group = g.Key,
               Number = g.Count()
           })
           .ToListAsync();
        }


        public async Task<IEnumerable<OrderGroupResponse>> GetOrdersGroupedByStatusAsync()
        {
            return await _dbContext.UnsOrders
                .GroupBy(o => o.Status)
                .Select(g => new OrderGroupResponse
                {
                    Group = g.Key,
                    Number = g.Count()
                })
                .ToListAsync();
        }
    }
}
