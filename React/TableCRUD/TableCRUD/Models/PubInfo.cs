using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[Table("pub_info")]
public partial class PubInfo
{
    [Key]
    [Column("pub_id")]
    [StringLength(4)]
    [Unicode(false)]
    public string PubId { get; set; } = null!;

    [Column("logo", TypeName = "image")]
    public byte[]? Logo { get; set; }

    [Column("pr_info", TypeName = "text")]
    public string? PrInfo { get; set; }

    [ForeignKey("PubId")]
    [InverseProperty("PubInfo")]
    public virtual Publisher Pub { get; set; } = null!;
}
