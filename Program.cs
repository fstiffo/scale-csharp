using System;
using System.Linq;

using var db = new ScaleContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new account");
db.Add(new Account
{
    Flow = FlowType.Debit,
    Name = "Stairs Payment",
    Nome = "Pagamento scale"
});
db.Add(new Account
{
    Flow = FlowType.Debit,
    Name = "Loan",
    Nome = "Prestito"
});
db.Add(new Account
{
    Flow = FlowType.Credit,
    Name = "Repayment",
    Nome = "Restituzione"
});
db.Add(new Account
{
    Flow = FlowType.Credit,
    Name = "Dues Payment",
    Nome = "Pagamento quote"
});
db.Add(new Account
{
    Flow = FlowType.Debit,
    Name = "Expenditure",
    Nome = "Altre spese"
});
db.Add(new Account
{
    Flow = FlowType.Credit,
    Name = "Revenue",
    Nome = "Altre entrate"
});
db.SaveChanges();
