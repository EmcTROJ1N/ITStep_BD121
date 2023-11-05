using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[PrimaryKey("StorId", "OrdNum", "TitleId")]
[Table("sales")]
[Index("TitleId", Name = "titleidind")]
public partial class Sale
{
    [Key]
    [Column("stor_id")]
    [StringLength(4)]
    [Unicode(false)]
    public string StorId { get; set; } = null!;

    [Key]
    [Column("ord_num")]
    [StringLength(20)]
    [Unicode(false)]
    public string OrdNum { get; set; } = null!;

    [Column("ord_date", TypeName = "datetime")]
    public DateTime OrdDate { get; set; }

    [Column("qty")]
    public short Qty { get; set; }

    [Column("payterms")]
    [StringLength(12)]
    [Unicode(false)]
    public string Payterms { get; set; } = null!;

    [Key]
    [Column("title_id")]
    [StringLength(6)]
    [Unicode(false)]
    public string TitleId { get; set; } = null!;

    [ForeignKey("StorId")]
    [InverseProperty("Sales")]
    public virtual Store Stor { get; set; } = null!;

    [ForeignKey("TitleId")]
    [InverseProperty("Sales")]
    public virtual Title Title { get; set; } = null!;
}
