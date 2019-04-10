namespace PetClinic.App
{
    using System;
    using System.Globalization;
    using AutoMapper;
    using DataProcessor.DTOs.Import;
    using Microsoft.EntityFrameworkCore.Storage;
    using Models;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            this.CreateMap<PassportDTO, Passport>()
                .ForMember(x => x.RegistrationDate,
                    o => o.MapFrom(s => 
                        DateTime.ParseExact(s.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
