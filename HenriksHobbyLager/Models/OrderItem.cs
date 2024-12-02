using System.ComponentModel.DataAnnotations;

namespace HenriksHobbylager.Models;
public class OrderItem
{
	[Key]
	public int Id { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; } = null!;
	public int ProductId { get; set; }
	public Product Product { get; set; } = null!;
}
