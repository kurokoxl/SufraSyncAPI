using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SufraSyncAPI.Models.DTOs.ProductDto;

namespace SufraSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int pageNumber, int pageSize,[FromQuery] int? categoryId)
        {
            var products = await _productService.GetAllProductsAsync( pageNumber,  pageSize,categoryId);
            return Success(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFoundError<ProductDTO>("Product not found");
            }
            return Success(product);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO request)
        {
            var product = await _productService.UpdateProductAsync(id, request);
            if (product == null)
            {
                return NotFoundError<ProductDTO>("Product not found");
            }

            return Success(product);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{productId}/ingredients")]
        public async Task<IActionResult> AddIngredient(int productId, [FromBody] CreateProductIngredientDto createDto)
        {
            try
            {
                var result = await _productService.AddIngredientToProductAsync(productId, createDto);

                if (result == null)
                {
                    return NotFoundError<object>("Product not found");
                }
                return Success(result);
            }
            catch (InvalidOperationException ex)
            {
                return ConflictError<object>(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequestError<object>(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}/ingredients/{ingredientId}")]
        public async Task<IActionResult> UpdateIngredient(int productId, int ingredientId, [FromBody] UpdateProductIngredientDto request)
        {
            var updatedIngredient = await _productService.UpdateProductIngredientAsync(productId, ingredientId, request);

            if (updatedIngredient == null)
            {
                return NotFoundError<ProductIngredientDto>("Product or Ingredient not found");
            }

            return Success(updatedIngredient, "Ingredient quantity updated successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto createDto)
        {
            var createdProduct = await _productService.AddProductAsync(createDto);
            if (createdProduct == null)
            {
                return ConflictError<ProductDTO>("Product with this name already exists");
            }

            return CreatedSuccess(nameof(GetProduct), new { id = createdProduct.ProductId }, createdProduct, "Product Added Successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var deletedProduct = await _productService.DeleteProductAsync(id);

                if (deletedProduct == null)
                {
                    return NotFoundError<ProductDTO>("Product doesn't exist");
                }

                return Success(deletedProduct, "Product Deleted Successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestError<object>(ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryOld(categoryId);
            return Success(products);
        }

        [HttpDelete("{productId}/ingredients/{ingredientId}")]
        public async Task<IActionResult> RemoveIngredient(int productId, int ingredientId)
        {
            try
            {
                var success = await _productService.RemoveIngredientFromProductAsync(productId, ingredientId);

                if (!success)
                {
                    return NotFoundError<bool>("Ingredient not found in this product's recipe.");
                }

                return Success(true, "Removed successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFoundError<bool>(ex.Message);
            }
        }
    }
}