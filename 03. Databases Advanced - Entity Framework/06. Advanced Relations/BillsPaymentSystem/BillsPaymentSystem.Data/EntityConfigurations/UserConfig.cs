namespace BillsPaymentSystem.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(p => p.Email)
                .HasMaxLength(80)
                .IsUnicode(false);

            builder.Property(p => p.Password)
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.HasMany(e => e.PaymentMethods)
                .WithOne(e => e.User);
        }
    }
}
