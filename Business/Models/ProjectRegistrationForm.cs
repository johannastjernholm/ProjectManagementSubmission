using System;
using System.Collections.Generic;
namespace Business.Models;

public class ProjectRegistrationForm
{
    public string Description { get; set; } = null!;
    public string? Notes { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
}
