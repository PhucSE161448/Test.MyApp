using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test.MyApp.Domain.EntityModels;

[Table("User")]
[Index("Username", Name = "UQ__User__536C85E45D001951", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? RefreshToken { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RefreshTokenExpiryTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
