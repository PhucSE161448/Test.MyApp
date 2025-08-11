using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test.MyApp.Domain.EntityModels;

[Table("SeatColor")]
public partial class SeatColor
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Color { get; set; }

    public int Price { get; set; }

    [InverseProperty("SeatColor")]
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
