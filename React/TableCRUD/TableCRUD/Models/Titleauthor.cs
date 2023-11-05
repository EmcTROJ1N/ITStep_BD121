using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[PrimaryKey("AuId", "TitleId")]
[Table("titleauthor")]
[Index("AuId", Name = "auidind")]
[Index("TitleId", Name = "titleidind")]
public partial class Titleauthor
{
    [Key]
    [Column("au_id")]
    [StringLength(11)]
    [Unicode(false)]
    public string AuId { get; set; } = null!;

    [Key]
    [Column("title_id")]
    [StringLength(6)]
    [Unicode(false)]
    public string TitleId { get; set; } = null!;

    [Column("au_ord")]
    public byte? AuOrd { get; set; }

    [Column("royaltyper")]
    public int? Royaltyper { get; set; }

    [ForeignKey("AuId")]
    [InverseProperty("Titleauthors")]
    public virtual Author Au { get; set; } = null!;

    [ForeignKey("TitleId")]
    [InverseProperty("Titleauthors")]
    public virtual Title Title { get; set; } = null!;
}
