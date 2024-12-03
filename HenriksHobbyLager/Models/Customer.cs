using System.ComponentModel.DataAnnotations;

namespace HenriksHobbylager.Models;

public class Customer
{
    [Key]
    public int Id { get; set; } 
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<Order>? Orders { get; set; } 
    
}   
