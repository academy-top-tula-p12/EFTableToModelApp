using System;
using System.Collections.Generic;

namespace EFTableToModelApp;

public partial class Country
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    //public int CapitalId { get; set; }
    public Capital? Capital { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}

public class Capital
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int CountryId { get; set; }
    public Country? Country { get; set; }
}
