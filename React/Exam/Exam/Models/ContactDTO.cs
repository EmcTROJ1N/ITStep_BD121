namespace Exam.Models;

public class ContactDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Notes { get; set; }
    public int PhoneId { get; set; }
    public int? CategoryId { get; set; }
}