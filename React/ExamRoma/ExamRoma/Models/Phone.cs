using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExamRoma.Models;

public partial class Phone
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("phone_number")]
    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;
}
