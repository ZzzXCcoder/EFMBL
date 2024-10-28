using EFMBL.Entities;
using EFMBL.OrderDto;

namespace EFMBL.IOrderServise
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(AddOrderDto order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> SortOrdersBetweenAsync(string districtName, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<Order>> SortOrdersAsync(string districtName);
        Task<IEnumerable<FilteredOrderResult>> GetAllSortedOrdersAsync();
    }
}

