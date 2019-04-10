﻿namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using Data.Models;

    public class Bonus
    {
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {
            Prisoner prisoner = context.Prisoners.Find(prisonerId);

            if (prisoner.ReleaseDate == null)
            {
                return $"Prisoner {prisoner.FullName} is sentenced to life";
            }

            prisoner.ReleaseDate = DateTime.Now;
            prisoner.Cell = null;

            context.SaveChanges();

            return $"Prisoner {prisoner.FullName} released";
        }
    }
}
