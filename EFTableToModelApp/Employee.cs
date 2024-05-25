using System;
using System.Collections.Generic;

namespace EFTableToModelApp;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public int? CompanyId { get; set; }

    public int? PositionId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public List<Project> Projects { get; set; } = new();
    public List<EmployeeProject> Middle { get; set; } = new();
}
