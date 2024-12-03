using System.ComponentModel.DataAnnotations;

namespace HenriksHobbylager.Models;

// order
public class Order
{
	[Key]
	public int Id { get; set; }
	public DateTime OrderDate { get; set; } = DateTime.Now;
	public decimal TotalPrice { get; set; }
	public required ICollection<OrderItem> OrderItems { get; set; }
}