using Azure.Core;
using FluentValidation.Results;
using Inventarios_Controller.Request;
using Inventarios_Controller.Services;
using Inventarios_Controller.Validators;
using InventariosModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios_Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly InventariosContext _context;
        private readonly UserServices _userServices;

        public UsersController(InventariosContext context, UserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        [HttpGet("usersList/{status}")]
        public async Task<ActionResult> GetListUsers(int status)
        {
            try
            {
                var users = await (from u in _context.UserModel
                                   join r in _context.RoleModel on u.RoleId equals r.RoleId
                                   where u.UserStatus == status
                                   select new
                                   {
                                       u.UserId,
                                       u.Name,
                                       u.LastName,
                                       u.Email,
                                       u.Password,
                                       u.UserStatus,
                                       r.RoleName,
                                       r.RoleId
                                   }).ToListAsync();
                return StatusCode(StatusCodes.Status200OK, new { mesage = "OK", response = users });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult> InsertUser(UserRequest request)
        {
            try
            {
                var validator = new UserValidator();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    var password = _userServices.GetSHA256(request.Password);
                    var data = await _userServices.InsertUserAsync(request.Name, request.LastName, request.Email, password, request.RoleId);
                    if (!data.Success)
                        return StatusCode(StatusCodes.Status400BadRequest, new { message = data.Message });

                    return StatusCode(StatusCodes.Status200OK, new { message = data.Message, data = data.Data });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { message = result.Errors.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                ValidationResult result = new UpdateUserValidator().Validate(request);
                if (result.IsValid)
                {
                    var user = _context.UserModel.FirstOrDefault(x => x.UserId == request.UserId && x.UserStatus == 1);
                    if (null != user)
                    {
                        user.LastName = request.LastName;
                        user.Email = request.Email;
                        user.RoleId = request.RoleId;
                        _context.UserModel.Entry(user).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return StatusCode(StatusCodes.Status200OK, new { message = "Usuario actualizado correctamente" });
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Usuario no encontrado" });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { message = result.Errors.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("changeStatus")]
        public async Task<ActionResult> ChangeStatus(int userId, int status)
        {
            try
            {
                if (userId != 0)
                {
                    var user = _context.UserModel.FirstOrDefault(x => x.UserId == userId && x.UserStatus == 1);
                    if (null != user)
                    {
                        user.UserStatus = status;
                        _context.UserModel.Entry(user).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return StatusCode(StatusCodes.Status200OK, new { message = "Estatus actualizado correctamente" });
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Usuario no encontrado" });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Id usuario no valido" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
