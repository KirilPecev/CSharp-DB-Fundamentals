namespace BillsPaymentSystem.App.Models
{
    using BillsPaymentSystem.Models;
    using BillsPaymentSystem.Models.Enums;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;

    public class DbController
    {
        private readonly BillsPaymentSystemContext _context;

        public DbController(BillsPaymentSystemContext context)
        {
            this._context = context;
        }

        public void Read()
        {
            try
            {
                Console.Write("Enter UserId: ");
                int userId = int.Parse(Console.ReadLine());

                User user = this._context.Users.Find(userId);

                if (user == null)
                {
                    Console.WriteLine($"User with id {userId} not found!");
                    return;
                }

                Console.WriteLine($"User: {user.FirstName} {user.LastName}");
                Console.WriteLine("Bank Accounts:");

                var bankAccounts = this._context.PaymentMethods
                    .Where(u => u.User.UserId == userId)
                    .Select(b => b.BankAccount)
                    .ToList();

                foreach (var ba in bankAccounts)
                {
                    if (ba == null)
                    {
                        continue;
                    }

                    Console.WriteLine($"-- ID: {ba.BankAccountId}");
                    Console.WriteLine($"--- Balance: {ba.Balance:F2}");
                    Console.WriteLine($"--- Bank: {ba.BankName}");
                    Console.WriteLine($"--- SWIFT: {ba.SwiftCode}");
                }

                Console.WriteLine("Credit Cards:");
                var creditCards = this._context.PaymentMethods
                    .Include(e => e.CreditCard)
                    .Where(u => u.UserId == userId)
                    .Select(c => c.CreditCard)
                    .ToList();

                foreach (var cc in creditCards)
                {
                    if (cc == null)
                    {
                        continue;
                    }

                    Console.WriteLine($"-- ID: {cc.CreditCardId}");
                    Console.WriteLine($"--- Limit: {cc.Limit:F2}");
                    Console.WriteLine($"--- Money Owned: {cc.MoneyOwned:F2}");
                    Console.WriteLine($"--- Limit Left: {cc.LimitLeft:F2}");
                    Console.WriteLine($"--- Expiration Date: {cc.ExpirationDate.ToString("yyyy/MM", CultureInfo.GetCultureInfo("US-us"))}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void PayBills()
        {
            try
            {
                Console.Write("Enter UserId: ");
                int userId = int.Parse(Console.ReadLine());

                Console.Write("Enter bills amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                if (amount <= 0)
                {
                    throw new ArgumentException("Amount should be more than 0!");
                }

                var userBankAccounts = this._context.PaymentMethods
                    .Include(e => e.BankAccount)
                    .Where(pm => pm.User.UserId == userId && pm.Type == PaymentType.BankAccount)
                    .Select(e => e.BankAccount)
                    .OrderBy(pm => pm.BankAccountId)
                    .ToList();

                var userCreditCards = this._context.PaymentMethods
                    .Include(e => e.CreditCard)
                    .Where(pm => pm.User.UserId == userId && pm.Type == PaymentType.CreditCard)
                    .Select(e => e.CreditCard)
                    .OrderBy(pm => pm.CreditCardId)
                    .ToList();

                decimal totalBalance = userBankAccounts.Select(u => u.Balance).Sum() +
                                       userCreditCards.Select(u => u.MoneyOwned).Sum();

                if (totalBalance < amount)
                {
                    throw new ArgumentException("Insufficient funds!");
                }

                foreach (var account in userBankAccounts)
                {
                    if (amount == 0)
                    {
                        break;
                    }

                    decimal moneyInAccount = account.Balance;

                    if (moneyInAccount < amount)
                    {
                        account.Withdraw(moneyInAccount);
                        amount -= moneyInAccount;
                        continue;
                    }

                    account.Withdraw(amount);
                    amount = 0;
                }

                foreach (var creditCard in userCreditCards)
                {
                    if (amount == 0)
                    {
                        break;
                    }

                    decimal moneyInCreditCard = creditCard.MoneyOwned;

                    if (moneyInCreditCard < amount)
                    {
                        creditCard.Withdraw(moneyInCreditCard);
                        amount -= moneyInCreditCard;
                        continue;
                    }

                    creditCard.Withdraw(amount);
                    amount = 0;
                }

                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
