using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class Category
{
    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    public virtual ICollection<MccCode> MccCodes { get; set; } = new List<MccCode>();

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    public virtual ICollection<TrnCode> TrnCodes { get; set; } = new List<TrnCode>();
}
