namespace BillsPaymentSystem.App.Core.Commands
{
    using Models.Enums;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class PayBillsCommand : ICommand
    {
        private readonly BillsPaymentSystemContext _context;

        public PayBillsCommand(BillsPaymentSystemContext context)
        {
            this._context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);
            decimal amount = decimal.Parse(args[1]);

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

            return "Bills was paid successfully!";
        }
    }
}
