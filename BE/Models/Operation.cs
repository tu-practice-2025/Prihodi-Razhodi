using SummerPracticeWebApi.Enums;
using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class Operation
{
    public uint Id { get; set; }

    public decimal AmountLcy { get; set; }

    public decimal AmountFcy { get; set; }

    public Currency LocalCurrency { get; set; }

    public Currency ForeignCurrency { get; set; }

    public DateTime DateTime { get; set; }

    public string? Description { get; set; }

    public string? OwnIban { get; set; }

    public string? OwnName { get; set; }

    public string? OtherIban { get; set; }

    public string? OtherName { get; set; }

    public string? MccCode { get; set; }

    public string? TrnCode { get; set; }

    public string? CategoryCode { get; set; }

    public bool IsExpense { get; set; }

    public uint? CardId { get; set; }

    public uint AccId { get; set; }

    public virtual Account Acc { get; set; } = null!;

    public virtual Card? Card { get; set; }

    public virtual Category? CategoryCodeNavigation { get; set; }

    public virtual MccCode? MccCodeNavigation { get; set; }

    public virtual TrnCode? TrnCodeNavigation { get; set; }
}
