using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SufraSync.Data;
using SufraSyncAPI.Models.DTOs;
using SufraSyncAPI.Models.Entities;
using SufraSyncAPI.Services.Interfaces;

namespace SufraSyncAPI.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IngredientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
        {
            var ingredients = await _context.Ingredients
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        public async Task<IngredientDto?> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _context.Ingredients
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IngredientId == id);

            return ingredient == null ? null : _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto?> AddIngredientAsync(CreateIngredientDto createDto)
        {
            // Optional: Check if name exists
            if (await _context.Ingredients.AnyAsync(i => i.Name.ToLower() == createDto.Name.ToLower()))
                return null; // Duplicate name

            var ingredient = _mapper.Map<Ingredient>(createDto);

            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();

            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto?> UpdateIngredientAsync(int id, UpdateIngredientDto updateDto)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null) return null;

            _mapper.Map(updateDto, ingredient);
            await _context.SaveChangesAsync();

            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var ingredient = await _context.Ingredients
                .Include(i => i.ProductIngredients) // Check usage
                .FirstOrDefaultAsync(i => i.IngredientId == id);

            if (ingredient == null) return false;

            //Is this ingredient used in any product?
            if (ingredient.ProductIngredients != null && ingredient.ProductIngredients.Any())
            {
                throw new InvalidOperationException("Cannot delete ingredient because it is used in product recipes.");
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}