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

    // Read a text file and return its contents as a string.
    private static string ReadFile(string path)
    {
        using var reader = new System.IO.StreamReader(path);
        return reader.ReadToEnd();
    }

    private void SetupJournalEntries()
    {
        var entries = new List<JournalEntry>();
        var lines = ReadFile(System.IO.Path.Join(System.AppContext.BaseDirectory, "journal.csv")).Split('\n');
        foreach (var line in lines)
        {
            Console.WriteLine(line);
            var parts = line.Split(',');
            var date = parts[0].Split('-');
            var accountId = int.Parse(parts[3]);
            var account = Accounts.Find(accountId);
            var debit = float.Parse(parts[1]);
            var credit = float.Parse(parts[2]);
            var apartmentId = accountId == 4 ? int.Parse(parts[4]) : (int?)null;

            var entry = new JournalEntry
            {
                AccountId = accountId,
                ApartmentId = apartmentId,
                Date = new DateOnly(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2])),
                Debit = debit,
                Credit = credit,
            };
            entries.Add(entry);
        }
        JournalEntries.AddRange(entries);
    }

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

        if (!JournalEntries.Any())
        {
            // Create
            Console.WriteLine("Initializing journal entries");
            SetupJournalEntries();
            SaveChanges();
        }
    }
    // Calculate how many months are beetween two DateOnly objects.
    private int MonthsBetween(DateOnly from, DateOnly to)
    {
        var months = 0;
        var current = from;
        while (current <= to)
        {
            current = current.AddMonths(1);
            months++;
        }
        return months;
    }
    // Calculate the total balance for all accounts, debit is negative and credit positive
    public float TotalBalance()
    {
        var balance = 0F;

        var entries = JournalEntries.ToList(); // Select all journal entries
        var totalDebit = entries.Sum(e => e.Debit); // Sum all debit entries
        var totalCredit = entries.Sum(e => e.Credit); // Sum all credit entries
        balance = totalCredit - totalDebit; // Subtract debit from credit

        return balance;
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
    public int? ApartmentId { get; set; }
    public Apartment? Apartment { get; set; }
    public string? Description { get; set; }
}

