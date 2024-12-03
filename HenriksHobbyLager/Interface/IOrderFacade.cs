using HenriksHobbylager.Models;


namespace HenriksHobbyLager.Interface;

public interface IOrderFacade
{
	Task CreateOrderAsync(Order order);
	Task DeleteOrderAsync(string orderId);
	Task UpdateOrderAsync(Order order);
	Task<Order?> GetOrderByAsync(string orderId);
	Task<IEnumerable<Order>> GetAllOrdersAsync();
}
