using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[Keyless]
[Table("discounts")]
public partial class Discount
{
    [Column("discounttype")]
    [StringLength(40)]
    [Unicode(false)]
    public string Discounttype { get; set; } = null!;

    [Column("stor_id")]
    [StringLength(4)]
    [Unicode(false)]
    public string? StorId { get; set; }

    [Column("lowqty")]
    public short? Lowqty { get; set; }

    [Column("highqty")]
    public short? Highqty { get; set; }

    [Column("discount", TypeName = "decimal(4, 2)")]
    public decimal Discount1 { get; set; }

    [ForeignKey("StorId")]
    public virtual Store? Stor { get; set; }
}
