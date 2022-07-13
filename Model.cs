using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ScaleContext : DbContext
{
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<JournalEntry> JournalEntries { get; set; }

    public string DbPath { get; }

    public ScaleContext()
    {
        var path = System.AppContext.BaseDirectory;
        DbPath = System.IO.Path.Join(path, "scale.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public void SetupDb()
    {
        if (!Configurations.Any())
        {
            // Create
            Console.WriteLine("Initializing configurations");
            Add(new Configuration
            {
                ValidFrom = new DateOnly(2019, 7, 1),
                StairsCleaningFee = 20F,
                CleaningsPerMonth = 2,
                MonthlyDues = 12F
            });
            SaveChanges();
        }

        if (!Accounts.Any())
        {
            // Create
            Console.WriteLine("Initializing accounts");
            Add(new Account { Flow = FlowType.Debit, Name = "Stairs Payment", Nome = "Pagamento scale" });
            Add(new Account { Flow = FlowType.Debit, Name = "Loan", Nome = "Prestito" });
            Add(new Account { Flow = FlowType.Credit, Name = "Repayment", Nome = "Restituzione" });
            Add(new Account { Flow = FlowType.Credit, Name = "Dues Payment", Nome = "Pagamento quote" });
            Add(new Account { Flow = FlowType.Debit, Name = "Expenditure", Nome = "Altre spese" });
            Add(new Account { Flow = FlowType.Credit, Name = "Revenue", Nome = "Altre entrate" });
            SaveChanges();
        }

        if (!Apartments.Any())
        {
            // Create
            Console.WriteLine("Initializing apartments");
            Add(new Apartment { Floor = 1, Tenant = "Barbara" });
            Add(new Apartment { Floor = 2, Tenant = "Elena" });
            Add(new Apartment { Floor = 2, Tenant = "Gerardo" });
            Add(new Apartment { Floor = 2, Tenant = "Michela" });
            SaveChanges();
        }
    }
}

public class Configuration
{
    public int ConfigurationId { get; set; }
    public DateOnly ValidFrom { get; set; }
    public float StairsCleaningFee { get; set; }
    public int CleaningsPerMonth { get; set; }
    public float MonthlyDues { get; set; }
}

public enum FlowType
{
    Debit,
    Credit
}
public class Account
{
    public int AccountId { get; set; }
    public FlowType Flow { get; set; }
    public string Name { get; set; }
    public string Nome { get; set; }

    public List<JournalEntry> JournalEntries { get; } = new();
}

public class Apartment
{
    public int ApartmentId { get; set; }
    public int Floor { get; set; }
    public string Tenant { get; set; }


    public List<JournalEntry> JournalEntries { get; } = new();
}


public class JournalEntry
{
    public int JournalEntryId { get; set; }
    public DateOnly Date { get; set; }
    public float Debit { get; set; }
    public float Credit { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
    public int ApartmentId { get; set; }
    public Apartment Apartment { get; set; }
    public string? Description { get; set; }
}

