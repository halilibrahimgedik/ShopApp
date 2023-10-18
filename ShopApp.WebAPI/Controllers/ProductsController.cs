using BusinessLayer.Abstract;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.WebAPI.DTO;

namespace ShopApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAll();

            var productDto = new List<ProductDTO>();

            foreach (var product in products)
            {
                productDto.Add(productToDTO(product));
            }

            return Ok(productDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var p = await _productService.GetById(id);

            if (p == null)
            {
                return NotFound(); //! 404 hatası
            }

            return Ok(productToDTO(p));   //! 200 başarılı
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product p)
        {
            await _productService.AddAsync(p);
            return CreatedAtAction(nameof(GetProduct), new { id = p.Id }, productToDTO(p));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,Product entity)
        {
            if (id != entity.Id)
            {
                return BadRequest(); //! 400 lü hata 
            }

            var product = await _productService.GetById(id);

            if(product == null)
            {
                return NotFound(); //! 404 hatası
            }

            await _productService.UpdateAsync(product,entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetById(id);

            if( product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(product);

            return NoContent(); //! 204 status kodu
        }

        private static ProductDTO productToDTO(Product product)
        {
            var productDto = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Url = product.Url,
                ImageUrl = product.ImageUrl,
                Description = product.Description
            };

            return productDto;
        }
    }
}

