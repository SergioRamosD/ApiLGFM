using LGFM_Abstraction;
using LGFM_Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_LGFM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _productRepository;
        private readonly ILogger<ProductsController> _logger;
        private readonly ISales _sales;
        private readonly ISalesDetails _salesDetails;

        public ProductsController(ILogger<ProductsController> logger, IProduct productRepository, ISales sales, ISalesDetails salesDetails)
        {
            _logger = logger;
            _productRepository = productRepository;
            _sales = sales;
            _salesDetails = salesDetails;
        }



        [HttpGet("Productos")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }
        [HttpGet("Sales")]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _sales.GetAll();
            return Ok(sales);
        }

        [HttpGet("SalesDetails")]
        public async Task<IActionResult> GetAllSalesDetails()
        {
            var salesDetails = await _salesDetails.GetAll();
            return Ok(salesDetails);
        }

        [HttpPost("SendProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }
            var createdProduct = await _productRepository.Add(product);
            return CreatedAtAction(nameof(GetAllProducts), new { id = createdProduct.ProductoID }, createdProduct);
        }
    }

}
