using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SufraSync.Data;
using SufraSyncAPI.Models.DTOs.ProductDto;
using SufraSyncAPI.Models.Entities;

namespace SufraSync.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? categoryId = null)
        {
            var query = _context.Products.AsNoTracking().AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = await query
                .Include(p => p.Category) 
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        public async Task<ProductDTO?> GetProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductIngredients)!
                .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO?> UpdateProductAsync(int id, UpdateProductDTO updateDto)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return null;

            _mapper.Map(updateDto, product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }


        public async Task<ProductDTO?> AddProductAsync(CreateProductDto createDto)
        {
            if (await _context.Products.AnyAsync(p => p.Name.ToLower() == createDto.Name.ToLower()))
                return null;


            if (!await _context.Categories.AnyAsync(c => c.CategoryId == createDto.CategoryId))
                throw new ArgumentException("Invalid Category ID");

            var product = _mapper.Map<Product>(createDto);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO?> DeleteProductAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return null;

            //if (product.OrderItems != null && product.OrderItems.Any())
            //{
            //    throw new InvalidOperationException("Cannot delete product because it has active orders.");
            //}

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryOld(int categoryId)
        {

            var products = await _context.Products
                    .Where(p => p.CategoryId == categoryId)
                    .Include(p => p.Category)
                    .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        public async Task<ProductIngredientDto?> UpdateProductIngredientAsync(int id, int ingredientId, UpdateProductIngredientDto updateDto)
        {
            var productIngredient = await _context.ProductIngredients
                .Include(pi => pi.Ingredient)
                .FirstOrDefaultAsync(pi => pi.ProductId == id && pi.IngredientId == ingredientId);

            if (productIngredient == null)
                return null;

            _mapper.Map(updateDto, productIngredient);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductIngredientDto>(productIngredient);
        }
        public async Task<ProductIngredientDto?> AddIngredientToProductAsync(int productId, CreateProductIngredientDto createDto)
        {
            var product = await _context.Products
                .Include(p => p.ProductIngredients)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return null;

            if (product.ProductIngredients!.Any(pi => pi.IngredientId == createDto.IngredientId))
                throw new InvalidOperationException("This ingredient is already added to the product.");

            var ingredient = await _context.Ingredients.FindAsync(createDto.IngredientId);
            if (ingredient == null)
                throw new ArgumentException($"Ingredient with ID {createDto.IngredientId} does not exist.");

            var pIngredient = _mapper.Map<ProductIngredient>(createDto);
            pIngredient.Ingredient = ingredient;

            product.ProductIngredients.Add(pIngredient);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductIngredientDto>(pIngredient);
        }

        public async Task<bool> RemoveIngredientFromProductAsync(int productId, int ingredientId)
        {
      
            var productExists = await _context.Products.AnyAsync(p => p.ProductId == productId);

            if (!productExists)
            {
                throw new ArgumentException($"Product with ID {productId} not found.");
            }

            var productIngredient = await _context.ProductIngredients
                .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.IngredientId == ingredientId);

            if (productIngredient == null)
            {
                return false;
            }

            _context.ProductIngredients.Remove(productIngredient);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}