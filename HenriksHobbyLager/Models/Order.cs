using System.ComponentModel.DataAnnotations;

namespace HenriksHobbylager.Models;

public class Order
{
	[Key]
	public int Id { get; set; }
	public DateTime OrderDate { get; set; } = DateTime.Now;
	public decimal TotalPrice { get; set; }
	public ICollection<OrderItem> OrderItems { get; set; } = null!;

	public int CustomerId { get; set; }
	public Customer Customer { get; set; } = null!;
}