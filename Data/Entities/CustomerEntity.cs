
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string CustomerName { get; set; } = null!;
}
