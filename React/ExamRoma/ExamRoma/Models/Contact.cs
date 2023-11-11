using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExamRoma.Models;

public partial class Contact
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [Column("PhoneID")]
    public int PhoneId { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }
}
