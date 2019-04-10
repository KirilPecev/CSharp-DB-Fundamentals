﻿using System;
using FastFood.Data;

namespace FastFood.DataProcessor
{
    using System.Linq;
    using Models;

    public static class Bonus
    {
        public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
        {
            Item item = context.Items.FirstOrDefault(i => i.Name == itemName);

            if (item == null)
            {
                return $"Item {itemName} not found!";
            }

            decimal oldPrice = item.Price;
            item.Price = newPrice;
            context.SaveChanges();

            return $"{item.Name} Price updated from ${oldPrice:F2} to ${newPrice:F2}";
        }
    }
}
