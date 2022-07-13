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
}

public class Configuration
{
    public int ConfigurationId { get; set; }
    public DateOnly ValidFrom { get; set; }
    public float StairsCleaningFee { get; set; }
    public int CleaningsPerMonth { get; set; }
    public float MonthlyDues { get; set; }
}

public class Account
{
    public int AccountId { get; set; }
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
    public string Description { get; set; }
}

