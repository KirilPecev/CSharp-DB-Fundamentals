namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using Models;
    using System;
    using Models.Enums;
    using ViewModels.Categories;
    using ViewModels.Employees;
    using ViewModels.Items;
    using ViewModels.Orders;
    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>();

            //Orders
            this.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(x => x.DateTime, y => y.MapFrom(s => DateTime.Now))
                .ForMember(x => x.Type, y => y.MapFrom(s => Enum.Parse<OrderType>(s.OrderType)));

            this.CreateMap<CreateOrderInputModel, OrderItem>()
                .ForMember(x=>x.Order,y=>y.MapFrom(s=>s))
                .ForMember(x=>x.ItemId,y=>y.MapFrom(s=>s.ItemId))
                .ForMember(x => x.Quantity, y => y.MapFrom(s => s.Quantity));

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.OrderId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.Employee, y => y.MapFrom(s => s.Employee.Name))
                .ForMember(x=>x.Type,y=>y.MapFrom(s=>s.Type.ToString()))
                .ForMember(x => x.DateTime, y => y.MapFrom(s => s.DateTime.ToString("dd-MM-yyyy -> HH:mm")));

            //Items
            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(s => s.Id));

            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Category.Name));

            //Employees
            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(s => s.Id));

            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x=>x.Position,y=>y.MapFrom(s=>s.Position.Name));

            //Categories
            this.CreateMap<Category, CreateCategoryInputModel>()
               .ForMember(x => x.CategoryName, y => y.MapFrom(s => s.Name))
                .ReverseMap();

            this.CreateMap<Category, CategoryAllViewModel>();
        }
    }
}
