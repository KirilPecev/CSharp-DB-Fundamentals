namespace BillsPaymentSystem.App.Core.Commands
{
    using Models;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class UserInfoCommand : ICommand
    {
        private readonly BillsPaymentSystemContext _context;

        public UserInfoCommand(BillsPaymentSystemContext context)
        {
            this._context = context;
        }

        public string Execute(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            int userId = int.Parse(args[0]);

            User user = this._context.Users.Find(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with id {userId} not found!");
            }

            sb.AppendLine($"User: {user.FirstName} {user.LastName}");
            sb.AppendLine("Bank Accounts:");

            var bankAccounts = this._context.PaymentMethods
                .Where(u => u.User.UserId == userId)
                .Select(b => b.BankAccount)
                .ToList();

            foreach (var bankAccount in bankAccounts)
            {
                if (bankAccount == null)
                {
                    continue;
                }

                sb.AppendLine($"-- ID: {bankAccount.BankAccountId}");
                sb.AppendLine($"--- Balance: {bankAccount.Balance:F2}");
                sb.AppendLine($"--- Bank: {bankAccount.BankName}");
                sb.AppendLine($"--- SWIFT: {bankAccount.SwiftCode}");
            }

            sb.AppendLine("Credit Cards:");
            var creditCards = this._context.PaymentMethods
                .Include(e => e.CreditCard)
                .Where(u => u.UserId == userId)
                .Select(c => c.CreditCard)
                .ToList();

            foreach (var creditCard in creditCards)
            {
                if (creditCard == null)
                {
                    continue;
                }

                sb.AppendLine($"-- ID: {creditCard.CreditCardId}");
                sb.AppendLine($"--- Limit: {creditCard.Limit:F2}");
                sb.AppendLine($"--- Money Owned: {creditCard.MoneyOwned:F2}");
                sb.AppendLine($"--- Limit Left: {creditCard.LimitLeft:F2}");
                sb.AppendLine($"--- Expiration Date: {creditCard.ExpirationDate.ToString("yyyy/MM", CultureInfo.GetCultureInfo("US-us"))}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
