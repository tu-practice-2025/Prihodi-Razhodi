using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class MccCode
{
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public string Description { get; set; } = null!;

    public string CategoryCode { get; set; } = null!;

    public virtual Category CategoryCodeNavigation { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
