using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.Models;
using SummerPracticeWebApi.Enums;

namespace SummerPracticeWebApi.DataAccess.Context;

public class IncomeExpensesContext : DbContext
{
    public IncomeExpensesContext()
    {
    }

    public IncomeExpensesContext(DbContextOptions<IncomeExpensesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<MccCode> MccCodes { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<TrnCode> TrnCodes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost;user=root;password=root;database=income_expenses;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Iban, "iban").IsUnique();

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasPrecision(15)
                .HasColumnName("balance");
            entity.Property(e => e.Currency)
                .HasConversion<string>()
                .HasColumnType("enum('BGN','EUR','USD','GBP')")
                .HasColumnName("currency");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Iban)
                .HasMaxLength(34)
                .HasColumnName("iban");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("accounts_ibfk_1");
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("budgets");

            entity.HasIndex(e => e.CategoryCode, "category_code");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CategoryCode)
                .HasMaxLength(4)
                .HasColumnName("category_code");
            entity.Property(e => e.Currency)
                .HasColumnType("enum('BGN','EUR','USD','GBP')")
                .HasColumnName("currency");
            entity.Property(e => e.Month).HasColumnName("month");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CategoryCodeNavigation).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.CategoryCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("budgets_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("budgets_ibfk_1");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cards");

            entity.HasIndex(e => e.AccountId, "account_id");

            entity.HasIndex(e => e.CardNumber, "card_number").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CardHolder)
                .HasMaxLength(255)
                .HasColumnName("card_holder");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .HasColumnName("card_number");
            entity.Property(e => e.Validity)
                .HasColumnType("date")
                .HasColumnName("validity");

            entity.HasOne(d => d.Account).WithMany(p => p.Cards)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("cards_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.Code)
                .HasMaxLength(4)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
        });

        modelBuilder.Entity<MccCode>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PRIMARY");

            entity.ToTable("mcc_codes");

            entity.HasIndex(e => e.CategoryCode, "category_code");

            entity.Property(e => e.Code)
                .HasMaxLength(4)
                .HasColumnName("code");
            entity.Property(e => e.CategoryCode)
                .HasMaxLength(4)
                .HasColumnName("category_code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.CategoryCodeNavigation).WithMany(p => p.MccCodes)
                .HasForeignKey(d => d.CategoryCode)
                .HasConstraintName("mcc_codes_ibfk_1");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("operations");

            entity.HasIndex(e => e.AccId, "acc_id");

            entity.HasIndex(e => e.CardId, "card_id");

            entity.HasIndex(e => e.CategoryCode, "category_code");

            entity.HasIndex(e => e.MccCode, "mcc_code");

            entity.HasIndex(e => e.TrnCode, "trn_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccId).HasColumnName("acc_id");
            entity.Property(e => e.AmountFcy)
                .HasColumnType("decimal(15,2) unsigned")
                .HasColumnName("amount_fcy");
            entity.Property(e => e.AmountLcy)
                .HasColumnType("decimal(15,2) unsigned")
                .HasColumnName("amount_lcy");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.CategoryCode)
                .HasMaxLength(4)
                .HasColumnName("category_code");
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("date_time");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ForeignCurrency)
                .HasColumnType("enum('BGN','EUR','USD','GBP')")
                .HasColumnName("foreign_currency");
            entity.Property(e => e.IsExpense).HasColumnName("is_expense");
            entity.Property(e => e.LocalCurrency)
                .HasColumnType("enum('BGN','EUR','USD','GBP')")
                .HasColumnName("local_currency");
            entity.Property(e => e.MccCode)
                .HasMaxLength(4)
                .HasColumnName("mcc_code");
            entity.Property(e => e.OtherIban)
                .HasMaxLength(34)
                .HasColumnName("other_iban");
            entity.Property(e => e.OtherName)
                .HasMaxLength(255)
                .HasColumnName("other_name");
            entity.Property(e => e.OwnIban)
                .HasMaxLength(34)
                .HasColumnName("own_iban");
            entity.Property(e => e.OwnName)
                .HasMaxLength(255)
                .HasColumnName("own_name");
            entity.Property(e => e.TrnCode)
                .HasMaxLength(3)
                .HasColumnName("trn_code");

            entity.HasOne(d => d.Acc).WithMany(p => p.Operations)
                .HasForeignKey(d => d.AccId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("operations_ibfk_5");

            entity.HasOne(d => d.Card).WithMany(p => p.Operations)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("operations_ibfk_4");

            entity.HasOne(d => d.CategoryCodeNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.CategoryCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("operations_ibfk_3");

            entity.HasOne(d => d.MccCodeNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.MccCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("operations_ibfk_1");

            entity.HasOne(d => d.TrnCodeNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.TrnCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("operations_ibfk_2");
        });

        modelBuilder.Entity<TrnCode>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PRIMARY");

            entity.ToTable("trn_codes");

            entity.HasIndex(e => e.CategoryCode, "category_code");

            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .HasColumnName("code");
            entity.Property(e => e.CategoryCode)
                .HasMaxLength(4)
                .HasColumnName("category_code");
            entity.Property(e => e.TrnDescription)
                .HasMaxLength(255)
                .HasColumnName("trn_description");

            entity.HasOne(d => d.CategoryCodeNavigation).WithMany(p => p.TrnCodes)
                .HasForeignKey(d => d.CategoryCode)
                .HasConstraintName("trn_codes_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Egn, "egn").IsUnique();

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Egn)
                .HasMaxLength(10)
                .HasColumnName("egn");
            entity.Property(e => e.FirstName)
                .HasMaxLength(63)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(63)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(63)
                .HasColumnName("middle_name");
            entity.Property(e => e.Username).HasColumnName("username");
        });
    }
}
