using AutoMapper;
using SufraSyncAPI.Models.DTOs.ProductDto;
using SufraSyncAPI.Models.Entities;

namespace SufraSyncAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductIngredient, ProductIngredientDto>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name));
            CreateMap<UpdateProductDTO, Product>();
            CreateMap<UpdateProductIngredientDto, ProductIngredient>();      
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateProductIngredientDto, ProductIngredient>();
        }
    }
}