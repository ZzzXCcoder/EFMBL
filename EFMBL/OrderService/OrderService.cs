using EFMBL.Entities;
using EFMBL.IOrderServise;
using EFMBL.OrderDto;
using EFMBL.OrderRepository;

namespace EFMBL.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<bool> AddOrderAsync(AddOrderDto order) => _orderRepository.AddOrderAsync(order);

        public Task<IEnumerable<Order>> GetAllOrdersAsync() => _orderRepository.GetAllOrdersAsync();

        public Task<IEnumerable<Order>> SortOrdersBetweenAsync(string districtName, DateTime fromDate, DateTime toDate)
            => _orderRepository.SortOrdersBetweenAsync(districtName, fromDate, toDate);

        public Task<IEnumerable<Order>> SortOrdersAsync(string districtName)
            => _orderRepository.SortOrdersAsync(districtName);

        public Task<IEnumerable<FilteredOrderResult>> GetAllSortedOrdersAsync() => _orderRepository.GetAllSortedOrdersAsync();
    }

}
