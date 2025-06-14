using SummerPracticeWebApi.Enums;
using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class Budget
{
    public uint Id { get; set; }

    public uint Amount { get; set; }

    public Currency Currency { get; set; }

    public bool IsIncome { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public uint UserId { get; set; }

    public string? CategoryCode { get; set; }

    public virtual Category? CategoryCodeNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
