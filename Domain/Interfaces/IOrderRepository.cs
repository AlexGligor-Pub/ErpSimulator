using Domain.Entities.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderGroupResponse>> GetOrdersGroupedByPriorityAsync();
        Task<IEnumerable<OrderGroupResponse>> GetOrdersGroupedByStatusAsync();
    }
}

