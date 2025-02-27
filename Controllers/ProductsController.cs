
using Microsoft.AspNetCore.Mvc;
using ShopApi.DTO.ProductDtos;
using ShopApi.Interfaces;
using ShopApi.Responses;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts(GetProductFilterRequestQuery request)
        {
            return Ok(new ApiResponse<IEnumerable<ProductFilterResponseDto>>(await productService.GetProductsAsync(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(new ApiResponse<ProductFilterResponseDto>(await productService.GetProductAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto request)
        {
            return Ok(new ApiResponse<bool>(await productService.CreateProductAsync(request), "Product created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDto requestDto)
        {
            return Ok(new ApiResponse<bool>(await productService.UpdateProductAsync(id, requestDto)));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(new ApiResponse<bool>(await productService.DeleteProductAsync(id)));
        }
    }
}
