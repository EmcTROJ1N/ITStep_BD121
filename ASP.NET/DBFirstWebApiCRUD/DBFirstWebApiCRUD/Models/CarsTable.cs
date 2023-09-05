using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstWebApiCRUD.Models;

[Table("CarsTable")]
public partial class CarsTable
{
    [Key]
    [Column("Car_id")]
    public int CarId { get; set; }

    [Column("Owner_id")]
    public int OwnerId { get; set; }

    public string Model { get; set; } = null!;

    public int MaxSpeed { get; set; }
}
