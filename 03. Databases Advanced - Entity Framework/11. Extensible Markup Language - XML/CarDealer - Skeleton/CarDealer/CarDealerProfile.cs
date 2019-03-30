using AutoMapper;

namespace CarDealer
{
    using System.Linq;
    using Dtos.Export;
    using Dtos.Import;
    using Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierDTO, Supplier>();

            this.CreateMap<PartDTO, Part>();

            this.CreateMap<CustomerDTO, Customer>();

            this.CreateMap<SalesDTO, Sale>();

            this.CreateMap<Car, ExportCarWithDistanceDTO>();

            this.CreateMap<Car, ExpotCarFromModelDTO>();

            this.CreateMap<Supplier, ExportSupplierDTO>()
                .ForMember(x => x.PartsCount, o => o.MapFrom(s => s.Parts.Count));

            this.CreateMap<Sale, ExportSaleWithAppliedDiscountDTO>()
                .ForMember(x => x.CustomerName, o => o.MapFrom(s => s.Customer.Name))
                .ForMember(x => x.Car, o => o.MapFrom(s => s.Car))
                .ForMember(x => x.Price, o => o.MapFrom(s => s.Car.PartCars.Sum(x => x.Part.Price)))
                .ForMember(x => x.PriceWithDiscount, o => o.MapFrom(s =>
                    $"{s.Car.PartCars.Sum(x => x.Part.Price) - s.Car.PartCars.Sum(x => x.Part.Price) * (s.Discount / 100)}".TrimEnd('0')));
        }
    }
}
