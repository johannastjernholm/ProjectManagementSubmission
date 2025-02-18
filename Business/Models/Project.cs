namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string? Notes { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = null!;
    public int CustomerId { get; set; }
}
