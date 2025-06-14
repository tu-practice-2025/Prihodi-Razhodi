using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class TrnCode
{
    public string Code { get; set; } = null!;

    public string TrnDescription { get; set; } = null!;

    public string CategoryCode { get; set; } = null!;

    public virtual Category CategoryCodeNavigation { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
