using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SufraSync.Data;
using SufraSyncAPI.Models.DTOs.CategoryDtos;
using SufraSyncAPI.Models.Entities;
using SufraSyncAPI.Services.Interfaces;

namespace SufraSyncAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CategoryDto> AddCategory(CreateCategoryDto categoryDto)
        {
           
            var category = _mapper.Map<Category>(categoryDto);

            //duplicated name
            if (await _context.Categories
                .AnyAsync(c => c.Name == categoryDto.Name))
            {
                return null;
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null) return false;
            _context.Remove(category);
           await _context.SaveChangesAsync();
           return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            return _mapper
               .Map<IEnumerable<CategoryDto>>
               (await _context.Categories.AsNoTracking().ToListAsync());
        }

        public async Task<CategoryDto> GetCategory(int categoryId)
        {
            return _mapper
                .Map<CategoryDto>(await _context.Categories.FirstOrDefaultAsync(c=>c.CategoryId==categoryId));
        }

        public async Task<CategoryDto> UpdateCategory(CategoryDto categoryDto)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == categoryDto.CategoryId);

            if (category == null) return null;

            category.Name = categoryDto.Name;

            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
