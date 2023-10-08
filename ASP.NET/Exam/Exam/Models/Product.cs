using System.ComponentModel.DataAnnotations;

namespace Exam.Models;

public class Product
{
    [Key] [Required]
    public int ProductId { get; set; }
    [Required]
    public uint CategoryId { get; set; }
    
    public uint SerialNumber { get; set; }
    public uint Price { get; set; }
    public DateTime IssueYear { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
}