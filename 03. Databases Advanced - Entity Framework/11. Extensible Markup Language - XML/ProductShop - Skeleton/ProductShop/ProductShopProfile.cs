using AutoMapper;

namespace ProductShop
{
    using System.Linq;
    using Dtos.Export;
    using Dtos.Import;
    using Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //User
            this.CreateMap<UserImportDTO, User>();

            this.CreateMap<User, ExportUserSoldProductDTO>()
                .ForMember(x => x.Products, o => o.MapFrom(s => s.ProductsSold));

            //Product
            this.CreateMap<ProductImportDTO, Product>();

            this.CreateMap<Product, ProductExportInRangeDTO>()
                .ForMember(x => x.BuyerName, o => o.MapFrom(s => s.Buyer.FirstName + " " + s.Buyer.LastName));

            //Category
            this.CreateMap<CategoryInputDTO, Category>();

            this.CreateMap<Category, ExportCategoryDTO>()
                .ForMember(x => x.Count, o => o.MapFrom(s => s.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, o => o.MapFrom(s => s.CategoryProducts.Average(p => p.Product.Price)))
                .ForMember(x => x.TotalRevenue, o => o.MapFrom(s => s.CategoryProducts.Sum(p => p.Product.Price)));

            //CategoryProduct
            this.CreateMap<CategoryProductDTO, CategoryProduct>();
        }
    }
}
