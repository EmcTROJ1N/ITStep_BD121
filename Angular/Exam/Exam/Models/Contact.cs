using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Exam.Models;

public partial class Contact
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("PhoneID")]
    public int PhoneId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Birthday { get; set; }

    [StringLength(999)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Contacts")]
    public virtual Category? Category { get; set; }

    [ForeignKey("PhoneId")]
    [InverseProperty("Contacts")]
    public virtual Phone Phone { get; set; } = null!;
}
