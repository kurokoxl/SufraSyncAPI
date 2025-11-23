using AutoMapper;
using SufraSyncAPI.Models.DTOs;
using SufraSyncAPI.Models.DTOs.CategoryDtos;
using SufraSyncAPI.Models.DTOs.ProductDto;
using SufraSyncAPI.Models.Entities;

namespace SufraSyncAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //product
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductIngredient, ProductIngredientDto>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Ingredient.Unit));
            CreateMap<UpdateProductDTO, Product>();
            CreateMap<UpdateProductIngredientDto, ProductIngredient>();      
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateProductIngredientDto, ProductIngredient>();

            //category
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();

            //ingredient
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<CreateIngredientDto, Ingredient>();
            CreateMap<UpdateIngredientDto, Ingredient>();

        }
    }
}