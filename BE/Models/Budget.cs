using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class Budget
{
    public uint Id { get; set; }

    public uint Amount { get; set; }

    public string Currency { get; set; } = null!;

    public uint UserId { get; set; }

    public string? CategoryCode { get; set; }

    public byte? Month { get; set; }

    public virtual Category? CategoryCodeNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
