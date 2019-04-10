namespace SoftJail
{
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using DataProcessor.ExportDto;


    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            this.CreateMap<Prisoner, InboxForPrisonerDTO>()
                .ForMember(x => x.IncarcerationDate, o => o.MapFrom(s => s.IncarcerationDate.ToString("yyyy-MM-dd")))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.FullName))
                .ForMember(x => x.Messages, o => o.MapFrom(s => s.Mails));
        }
    }
}
