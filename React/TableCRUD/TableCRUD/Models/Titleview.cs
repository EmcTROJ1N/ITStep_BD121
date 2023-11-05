using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[Keyless]
public partial class Titleview
{
    [Column("title")]
    [StringLength(80)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("au_ord")]
    public byte? AuOrd { get; set; }

    [Column("au_lname")]
    [StringLength(40)]
    [Unicode(false)]
    public string AuLname { get; set; } = null!;

    [Column("price", TypeName = "money")]
    public decimal? Price { get; set; }

    [Column("ytd_sales")]
    public int? YtdSales { get; set; }

    [Column("pub_id")]
    [StringLength(4)]
    [Unicode(false)]
    public string? PubId { get; set; }
}
