/* 
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;
using HenriksHobbyLager.Interface;
using HenriksHobbyLager.services;

namespace HenriksHobbyLager.Facades;

public class OrderFacade : DatabaseService<Order>, IOrderFacade
{
	private readonly IRepository<Order> _repository;

	public OrderFacade(IRepository<Order> repository) : base(repository)
	{
		_repository = repository;
	}

	public async Task CreateOrderAsync(Order order)
	{
		if (order.OrderItems == null || !order.OrderItems.Any())
		{
			throw new ArgumentException("En order måste innehålla minst ett orderitem.");
		}

		order.TotalPrice = order.OrderItems.Sum(item => item.TotalPrice);
		await _repository.AddAsync(order);
	}

	public async Task DeleteOrderAsync(string orderId)
	{
		var order = await GetOrderByIdAsync(orderId);
		if (order == null)
		{
			throw new ArgumentException($"Order med ID {orderId} hittades ej.");
		}

		await _repository.DeleteAsync(order);
	}

	public async Task UpdateOrderAsync(Order order)
	{
		var existingOrder = await GetOrderByIdAsync(order.Id.ToString());
		if (existingOrder == null)
		{
			throw new InvalidOperationException("Order kunde inte hittas.");
		}

		existingOrder.OrderItems = order.OrderItems;
		existingOrder.TotalPrice = order.OrderItems.Sum(item => item.TotalPrice);
		existingOrder.OrderDate = order.OrderDate;

		await _repository.UpdateAsync(existingOrder);
	}

	public async Task<Order?> GetOrderByIdAsync(string orderId)
	{
		return await GetOrderByIdAsync(_repository, orderId); // Using the base class
	}

    

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
	{
		return await _repository.GetAllAsync(order => true);
	}

	public Task<Order?> GetOrderByAsync(string orderId)
	{
		throw new NotImplementedException();
	}
}
 */