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
    public class OrdersController : ControllerBase
    {
        private readonly InventariosContext _context;

        public OrdersController(InventariosContext context)
        {
            _context = context;
        }

        [HttpGet("ordersList")]
        public async Task<ActionResult> GetOrdersList()
        {
            try
            {
                var orders = await _context.OrdersModel.Where(x => x.OrderStatus == 1).ToListAsync();
                if (null != orders && orders.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = "Ordenes", response = orders });
                }
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Listado de ordenes no encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("insertOrder")]
        public async Task<ActionResult> InsertOrder(OrderRequest request)
        {
            try
            {
                ValidationResult result = new OrdersValidator().Validate(request);
                if (result.IsValid)
                {
                    //obtenemos el producto
                    var product = _context.ProductModel.FirstOrDefault(x => x.ProductId == request.ProductId && x.ProductStatus == 1);
                    if (null != product)
                    {
                        if (product.ProductCount >= request.Quantity)
                        {
                            //insertamos la orden
                            var model = new OrdersModel
                            {
                                Quantity = request.Quantity,
                                CreatedAt = DateTime.Now,
                                CreatedBy = request.UserId,
                                CustomerName = request.CustomerName,
                                OrderStatus = 1,
                                ProductId = request.ProductId,
                                Total = product.ProductPrice * request.Quantity,
                            };
                            _context.OrdersModel.Add(model);
                            await _context.SaveChangesAsync();

                            product.ProductCount = product.ProductCount - request.Quantity;
                            _context.ProductModel.Entry(product).State = EntityState.Modified;
                            _context.SaveChanges();
                            return StatusCode(StatusCodes.Status200OK, new { message = "Orden Registrada correctamente" });
                        }
                        return StatusCode(StatusCodes.Status202Accepted, new { message = "La cantidad solicitada supera la disponible en stock" });
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
    }
}
