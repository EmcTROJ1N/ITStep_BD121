using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[Table("stores")]
public partial class Store
{
    [Key]
    [Column("stor_id")]
    [StringLength(4)]
    [Unicode(false)]
    public string StorId { get; set; } = null!;

    [Column("stor_name")]
    [StringLength(40)]
    [Unicode(false)]
    public string? StorName { get; set; }

    [Column("stor_address")]
    [StringLength(40)]
    [Unicode(false)]
    public string? StorAddress { get; set; }

    [Column("city")]
    [StringLength(20)]
    [Unicode(false)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(2)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("zip")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Zip { get; set; }

    [InverseProperty("Stor")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
