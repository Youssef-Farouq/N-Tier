using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N_Tier.BLL;
using N_Tier.DTOs;

namespace N_Tier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllProductsAsync());

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            await _productService.AddProductAsync(dto);
            return Ok();
        }
    }
}
