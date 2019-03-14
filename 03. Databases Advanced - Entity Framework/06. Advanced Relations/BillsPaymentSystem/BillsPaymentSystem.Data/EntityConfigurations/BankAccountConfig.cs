namespace BillsPaymentSystem.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(e => e.SwiftCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
