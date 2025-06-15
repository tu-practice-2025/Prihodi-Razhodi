using SummerPracticeWebApi.Enums;
using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class Account
{
    public uint Id { get; set; }

    public string Iban { get; set; } = null!;

    public decimal Balance { get; set; }

    public Currency Currency { get; set; }

    public uint UserId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    public virtual User User { get; set; } = null!;
}
