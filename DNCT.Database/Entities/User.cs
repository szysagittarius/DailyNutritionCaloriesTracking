using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NT.Database.Entities;

[Table("Users")]
public record User
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public required Guid Id { get; init; }

    [Column("Name", TypeName = "nvarchar(max)")]
    public required string Name { get; init; }

    [Column("Email", TypeName = "nvarchar(max)")]
    public required string Email { get; init; }

    [Column("Password", TypeName = "nvarchar(max)")]
    public required string Password { get; init; }
}
