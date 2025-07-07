using AutoMapper;
using KBDTypeServer.Application.DTOs.AuthDtos;
using KBDTypeServer.Application.DTOs.UserDtos;
using KBDTypeServer.Application.DTOs.AddressDtos;
using KBDTypeServer.Application.DTOs.OrderDtos;
using KBDTypeServer.Domain.Entities.AddressEnity;
using KBDTypeServer.Domain.Entities.OrderEntity;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Application.DTOs.ProductDtos;
using KBDTypeServer.Domain.Entities.ProductEntity;
using KBDTypeServer.Domain.ValueObjects;


namespace KBDTypeServer.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();


            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
            
            CreateMap<Product, ProductUniversalDto>()
                .ForMember(dest => dest.Characteristics, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    var characteristics = new Dictionary<string, object?>();

                    // Автоматично додаємо всі публічні властивості (крім базових з Product)
                    var type = src.GetType();
                    var baseType = typeof(Product);
                    var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                    foreach (var prop in properties)
                    {
                        // Пропускаємо властивості з базового класу Product, якщо потрібно
                        if (baseType.GetProperty(prop.Name) != null)
                            continue;

                        characteristics[prop.Name] = prop.GetValue(src);
                    }

                    dest.Characteristics = characteristics;
                });
            // In MappingProfile.cs

            CreateMap<OrderCreateDto, OrderInitData>()
                .ForCtorParam("FirstName", opt => opt.MapFrom(src => src.FirstName))
                .ForCtorParam("LastName", opt => opt.MapFrom(src => src.LastName))
                .ForCtorParam("Email", opt => opt.MapFrom(src => src.Email))
                .ForCtorParam("PhoneNumber", opt => opt.MapFrom(src => src.PhoneNumber))
                .ForCtorParam("Region", opt => opt.MapFrom(src => src.Region))
                .ForCtorParam("City", opt => opt.MapFrom(src => src.City))
                .ForCtorParam("Street", opt => opt.MapFrom(src => src.Street))
                .ForCtorParam("Building", opt => opt.MapFrom(src => src.Building))
                .ForCtorParam("Apartment", opt => opt.MapFrom(src => src.Apartment))
                .ForCtorParam("PostalCode", opt => opt.MapFrom(src => src.PostalCode))
                .ForCtorParam("Comment", opt => opt.MapFrom(src => src.Comment))
                .ForCtorParam("Items", opt => opt.MapFrom(src => src.Items));

            // Also add mapping for items if not present
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<Order, OrderShowDto>();
            CreateMap<OrderItem, OrderItemShowDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)) // Вказуємо, що ProductName в DTO береться з Product.Name
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl)) // А ImageUrl береться з Product.ImageUrl
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
                

        }
    }
}
