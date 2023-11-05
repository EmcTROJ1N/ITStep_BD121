using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableCRUD.Models;

[Table("employee")]
public partial class Employee
{
    [Key]
    [Column("emp_id")]
    [StringLength(9)]
    [Unicode(false)]
    public string EmpId { get; set; } = null!;

    [Column("fname")]
    [StringLength(20)]
    [Unicode(false)]
    public string Fname { get; set; } = null!;

    [Column("minit")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Minit { get; set; }

    [Column("lname")]
    [StringLength(30)]
    [Unicode(false)]
    public string Lname { get; set; } = null!;

    [Column("job_id")]
    public short JobId { get; set; }

    [Column("job_lvl")]
    public byte? JobLvl { get; set; }

    [Column("pub_id")]
    [StringLength(4)]
    [Unicode(false)]
    public string PubId { get; set; } = null!;

    [Column("hire_date", TypeName = "datetime")]
    public DateTime HireDate { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("Employees")]
    public virtual Job Job { get; set; } = null!;

    [ForeignKey("PubId")]
    [InverseProperty("Employees")]
    public virtual Publisher Pub { get; set; } = null!;
}
