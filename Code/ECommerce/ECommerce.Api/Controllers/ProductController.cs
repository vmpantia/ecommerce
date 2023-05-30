using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;
        public ProductController(IProductService product) => _product = product;

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var response = await _product.GetProductsAsync();
                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin"), HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProductAsync([FromForm] SaveProductRequest request)
        {
            try
            {
                await _product.SaveProductAsync(request);
                return Ok(Success.SAVED_PRODUCT);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
