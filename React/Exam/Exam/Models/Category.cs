using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Exam.Models;

public partial class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("category_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string CategoryName { get; set; } = null!;

    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
