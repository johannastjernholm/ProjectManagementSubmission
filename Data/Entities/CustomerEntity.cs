
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Index(nameof(CustomerEmail), IsUnique = true)]
public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string CustomerName { get; set; } = null!;
    [Required]
    public string CustomerEmail { get; set; } = null!;
    public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}
