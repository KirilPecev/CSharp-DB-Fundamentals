namespace PetClinic.DataProcessor
{
    using System;
    using System.Linq;
    using Models;
    using PetClinic.Data;

    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            Vet vet = context.Vets.FirstOrDefault(v => v.PhoneNumber == phoneNumber);

            if (vet == null)
            {
                return $"Vet with phone number {phoneNumber} not found!";
            }

            var oldProfession = vet.Profession;

            vet.Profession = newProfession;

            context.SaveChanges();

            return $"{vet.Name}'s profession updated from {oldProfession} to {newProfession}.";
        }
    }
}
