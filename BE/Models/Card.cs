using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class Card
{
    public uint Id { get; set; }

    public string CardNumber { get; set; } = null!;

    public string CardHolder { get; set; } = null!;

    public DateTime Validity { get; set; }

    public uint AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
