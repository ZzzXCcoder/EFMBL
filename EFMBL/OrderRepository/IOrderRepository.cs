using EFMBL.Entities;
using EFMBL.OrderDto;

namespace EFMBL.OrderRepository
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();

        public Task<bool> AddOrderAsync(AddOrderDto order);

        public Task<IEnumerable<Order>> SortOrdersBetweenAsync(string DistictName, DateTime fromDate, DateTime toDate);

        public Task<IEnumerable<Order>> SortOrdersAsync(string DistrictName);

        public Task<IEnumerable<FilteredOrderResult>> GetAllSortedOrdersAsync();
    }
}
