using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Models;

public class Category
{
    [Key] [Required]
    public uint CategoryId { get; set; }
    
    [Remote("ValidateName", "Categories", ErrorMessage = "Title should be longer than 2")]
    public string Name { get; set; }
}