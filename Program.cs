using System;
using System.Linq;

using var db = new ScaleContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

db.SetupDb();
Console.WriteLine($"Total balance: {db.TotalBalance()}");