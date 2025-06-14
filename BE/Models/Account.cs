using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class Account
{
    public uint Id { get; set; }

    public string Iban { get; set; } = null!;

    public decimal Balance { get; set; }

    public string Currency { get; set; } = null!;

    public uint UserId { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    public virtual User User { get; set; } = null!;
}
