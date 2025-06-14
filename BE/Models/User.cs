using System;
using System.Collections.Generic;

namespace SummerPracticeWebApi.Models;

public class User
{
    public uint Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Egn { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();
}
