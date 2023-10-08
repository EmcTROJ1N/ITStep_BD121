using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Exam.Models;

public class Order
{
    [Required] [Key]
    public int OrderId { get; set; }
    [Required]
    public string ClientId { get; set; }
    
    [Required]
    public DateTime OrderBegin { get; set; }
    public DateTime OrderCompletion { get; set; }
    [Required]
    public uint Salary { get; set; }
}