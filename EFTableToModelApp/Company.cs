using System;
using System.Collections.Generic;

namespace EFTableToModelApp;

public partial class Company
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
