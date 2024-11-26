﻿/* using System.ComponentModel.DataAnnotations;

namespace HenriksHobbylager.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? LastUpdated { get; set; }
} */


namespace HenriksHobbylager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}