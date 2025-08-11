using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test.MyApp.Domain.EntityModels;

[Table("Seat")]
public partial class Seat
{
    [Key]
    public int Id { get; set; }

    [StringLength(5)]
    public string RowChar { get; set; } = null!;

    public int ColNumber { get; set; }

    public int SeatColorId { get; set; }

    [StringLength(50)]
    public string? Floor { get; set; }

    [ForeignKey("SeatColorId")]
    [InverseProperty("Seats")]
    public virtual SeatColor SeatColor { get; set; } = null!;
}
