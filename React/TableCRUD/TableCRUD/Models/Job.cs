using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[Table("jobs")]
public partial class Job
{
    [Key]
    [Column("job_id")]
    public short JobId { get; set; }

    [Column("job_desc")]
    [StringLength(50)]
    [Unicode(false)]
    public string JobDesc { get; set; } = null!;

    [Column("min_lvl")]
    public byte MinLvl { get; set; }

    [Column("max_lvl")]
    public byte MaxLvl { get; set; }

    [InverseProperty("Job")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
