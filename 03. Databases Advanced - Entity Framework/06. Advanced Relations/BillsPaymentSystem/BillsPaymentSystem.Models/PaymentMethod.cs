namespace BillsPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Enums;

    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public int? BankAccountId { get; set; }
        [Xor(nameof(CreditCard))]
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
