using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstWebApiCRUD.Models;

[Table("OwnersTable")]
public partial class OwnersTable
{
    [Key]
    [Column("Owner_Id")]
    public int OwnerId { get; set; }
    
    [Required]
    public string FullName { get; set; } = null!;
}
