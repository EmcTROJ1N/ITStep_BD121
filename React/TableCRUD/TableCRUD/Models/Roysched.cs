using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[Keyless]
[Table("roysched")]
[Index("TitleId", Name = "titleidind")]
public partial class Roysched
{
    [Column("title_id")]
    [StringLength(6)]
    [Unicode(false)]
    public string TitleId { get; set; } = null!;

    [Column("lorange")]
    public int? Lorange { get; set; }

    [Column("hirange")]
    public int? Hirange { get; set; }

    [Column("royalty")]
    public int? Royalty { get; set; }

    [ForeignKey("TitleId")]
    public virtual Title Title { get; set; } = null!;
}
