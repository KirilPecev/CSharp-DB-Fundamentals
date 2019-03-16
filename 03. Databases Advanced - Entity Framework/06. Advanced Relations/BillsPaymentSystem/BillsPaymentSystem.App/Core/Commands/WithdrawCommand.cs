namespace BillsPaymentSystem.App.Core.Commands
{
    using System;
    using System.Linq;
    using Data;

    public class WithdrawCommand : Command
    {
        public WithdrawCommand(BillsPaymentSystemContext context)
            : base(context)
        {

        }

        public override string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);
            decimal amount = decimal.Parse(args[1]);
            string type = args[2];
            int typeId = int.Parse(args[3]);

            var user = this.Context.Users.Find(userId);

            var bankAccount = this.Context.BankAccounts
                .FirstOrDefault(e => e.PaymentMethod.UserId == userId && e.BankAccountId == typeId);

            var creditCard = this.Context.CreditCards
                .FirstOrDefault(e => e.PaymentMethod.UserId == userId && e.CreditCardId == typeId);

            if (type == "BankAccount" && bankAccount != null)
            {
                bankAccount.Withdraw(amount);
            }
            else if (type == "CreditCard" && creditCard != null)
            {
                creditCard.Withdraw(amount);
            }
            else
            {
                throw new ArgumentNullException("Bank account/Credit card Id","Invalid command!");
            }

            this.Context.SaveChanges();
            return $"Amount of {amount:F2} was withdrawn for user {user.FirstName} {user.LastName}.";
        }
    }
}
