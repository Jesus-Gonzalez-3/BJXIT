using FluentValidation.Results;
using Inventarios_Controller.Request;
using Inventarios_Controller.Validators;
using InventariosModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios_Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly InventariosContext _context;

        public ProductsController(InventariosContext context)
        {
            _context = context;
        }

        [HttpGet("productsList")]
        public async Task<ActionResult> GetProductsList()
        {
            try
            {
                var productsList =await _context.ProductModel.Where(x=> x.ProductStatus==1).ToListAsync();
                if(null!= productsList && productsList.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new {message= "Products List", response = productsList});
                }
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Productos no encontrados" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult> InsertProduct(ProductRequest request)
        {
            try
            {
                ValidationResult result = new ProductValidator().Validate(request);
                if (result.IsValid)
                {
                    var model = new ProductModel
                    {
                        ProductKey = request.ProductKey,
                        ProductName = request.ProductName,
                        ProductCount = request.ProductCount,
                        CreatedAt = DateTime.Now,
                        ProductPrice = request.ProductPrice,
                        ProductStatus = 1
                    };
                    _context.ProductModel.Add(model);
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, new { message = "Producto agregado Exitosamente" });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { message = result.Errors.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("updateInformation")]
        public async Task<ActionResult> UpdateProductInfo(UpdateProductRequest request)
        {
            try
            {
                ValidationResult result = new UpdateProductValidator().Validate(request);
                if (result.IsValid)
                {
                    var product = _context.ProductModel.FirstOrDefault(x => x.ProductId == request.ProductId && x.ProductStatus == 1);
                    if (product != null)
                    {
                        product.ProductPrice = request.ProductPrice;
                        product.ProductName = request.ProductName;
                        product.ProductKey = request.ProductKey;
                        _context.ProductModel.Entry(product).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return StatusCode(StatusCodes.Status200OK, new { message = "Producto Actualizado correctamente" });
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Producto no encontrado" });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { message = result.Errors.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("addStock")]
        public async Task<ActionResult> AddStockProducts(int productId, int quantity)
        {
            try
            {
                if(productId != 0 || quantity > 0)
                {
                    var product = _context.ProductModel.FirstOrDefault(x => x.ProductId == productId && x.ProductStatus == 1);
                    if (null != product)
                    {
                        product.ProductCount = product.ProductCount + quantity;
                        _context.ProductModel.Entry(product).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return StatusCode(StatusCodes.Status200OK, new { message = "Stock agregado correctamente" });
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Producto no encontrado" });

                }
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Paramentros no validos" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
