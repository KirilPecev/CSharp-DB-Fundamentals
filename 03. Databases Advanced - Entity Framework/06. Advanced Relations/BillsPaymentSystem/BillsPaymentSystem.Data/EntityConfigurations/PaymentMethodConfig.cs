namespace BillsPaymentSystem.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasOne(e => e.BankAccount)
                .WithOne(e => e.PaymentMethod);

            builder.HasOne(e => e.CreditCard)
                .WithOne(e => e.PaymentMethod);
        }
    }
}
