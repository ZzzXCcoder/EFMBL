using EFMBL.ApplicationDbContext;
using EFMBL.Entities;
using EFMBL.OrderDto;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace EFMBL.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrderAsync(AddOrderDto order)
        {

            Log.Information($"Получен заказ с DistrictName: '{order.DistrictName}', Weight: {order.Weight}, DeliveryTime: {order.DeliveryTime}");

            var district = await _context.Districts
                .FirstOrDefaultAsync(d => d.DistrictName == order.DistrictName);

            if (district == null)
            {
                Log.Error($"Район '{order.DistrictName}' не найден.");
                throw new ArgumentException($"Район '{order.DistrictName}' не найден.");
            }

          
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                Weight = order.Weight,
                DistrictId = district.Id,
                District = district,
                DeliveryTime = order.DeliveryTime
            };

            try
            {
                
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                Log.Information($"Заказ добавлен успешно: {newOrder.Id}");
                return true;
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Ошибка при сохранении заказа: {OrderId}", newOrder.Id);
                throw new Exception("Ошибка при сохранении заказа", ex);
            }
        }





        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {

                var orders = await _context.Orders
                .Include(o => o.District) 
                .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error fetching orders: {ex.Message}");
                throw; 
            }
        }
        public async Task<IEnumerable<Order>> SortOrdersBetweenAsync(string DistictName, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var SortedOrdersList = await _context.Orders
                .Where(d => d.DeliveryTime >= fromDate && d.DeliveryTime < toDate)
                .ToListAsync();
                return SortedOrdersList;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching orders: {ex.Message}");
                throw;
            }

        }
        public async Task<IEnumerable<Order>> SortOrdersAsync(string DistrictName)
        {
            try
            {
                // Получаем время первого заказа в указанном районе
                var firstOrderTime = await _context.Orders
                    .Where(o => o.District.DistrictName == DistrictName)
                    .OrderBy(o => o.DeliveryTime)
                    .Select(o => o.DeliveryTime)
                    .FirstOrDefaultAsync();

                if (firstOrderTime == default)
                {
                    Console.WriteLine("No orders found for the specified district.");
                    return Enumerable.Empty<Order>();
                }

                
                DateTime startTime = firstOrderTime;
                DateTime endTime = firstOrderTime.AddMinutes(30);

             
                var sortedOrdersList = await _context.Orders
                    .Include(o => o.District)
                    .Where(o => o.DeliveryTime >= startTime && o.DeliveryTime <= endTime
                                && o.District.DistrictName == DistrictName)
                    .ToListAsync();

               
                var filteredOrders = new List<FilteredOrderResult>();

                foreach (var order in sortedOrdersList)
                {
                    var filteredOrder = new FilteredOrderResult
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        Weight = order.Weight,
                        DistrictName = order.District.DistrictName,
                        DeliveryTime = order.DeliveryTime,
                        RequestTime = DateTime.UtcNow
                    };

                    filteredOrders.Add(filteredOrder);
                }

               
                await _context.FilteredOrderResults.AddRangeAsync(filteredOrders);
                await _context.SaveChangesAsync();

                return sortedOrdersList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


     
        public async Task<IEnumerable<FilteredOrderResult>> GetAllSortedOrdersAsync()
        {
            try
            {

                var orderList = await _context.FilteredOrderResults.ToListAsync();
                return orderList;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching orders: {ex.Message}");
                throw;
            }
        }


    }
}

