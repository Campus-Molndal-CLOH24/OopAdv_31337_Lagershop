using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HenriksHobbylager.Models;

public class Product
{
    // MongoDB
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }

    // SQLite
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [NotMapped] 
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? LastUpdated { get; set; }
    
    // DisplayId: Returnerar _id om det finns
    [NotMapped]
    public string DisplayId => _id ?? Id.ToString();
}