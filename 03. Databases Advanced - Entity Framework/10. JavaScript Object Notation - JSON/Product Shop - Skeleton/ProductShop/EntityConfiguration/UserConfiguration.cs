﻿namespace ProductShop.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.ProductsBought)
                .WithOne(x => x.Buyer)
                .HasForeignKey(x => x.BuyerId);

            builder.HasMany(x => x.ProductsSold)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId);
        }
    }
}
