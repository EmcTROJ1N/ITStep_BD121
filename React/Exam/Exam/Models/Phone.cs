using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Exam.Models;

public partial class Phone
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("phone_number")]
    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("full_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [Column("note")]
    [StringLength(999)]
    [Unicode(false)]
    public string? Note { get; set; }

    [Column("creation_date", TypeName = "date")]
    public DateTime? CreationDate { get; set; }

    [InverseProperty("Phone")]
    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
