using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models;

public class ContactWithPhoneCategory
{
    [Column("id")]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Notes { get; set; }
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }
    [Column("category_name")]
    public string? CategoryName { get; set; }
}