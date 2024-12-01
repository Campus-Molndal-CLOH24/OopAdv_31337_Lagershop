using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HenriksHobbylager.Models;

public class Product
{
    [Key]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? LastUpdated { get; set; }
}