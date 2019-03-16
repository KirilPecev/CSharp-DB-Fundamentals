namespace BillsPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }

        [Required]
        public decimal Limit { get; set; }

        [Required]
        public decimal MoneyOwned { get; set; }

        public decimal LimitLeft => this.Limit - this.MoneyOwned;

        [Required]
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (this.MoneyOwned <= 0 || this.MoneyOwned - amount < 0)
            {
                throw new ArgumentException("Not enough money!");
            }

            this.MoneyOwned -= amount;
        }

        public void Deposit(decimal amount)
        {
            if (this.LimitLeft < amount)
            {
                throw new ArgumentException("Invalid operation!");
            }

            this.MoneyOwned += amount;
        }
    }
}
