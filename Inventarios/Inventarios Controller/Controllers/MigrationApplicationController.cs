using Inventarios_Controller.Services;
using InventariosModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios_Controller.Controllers
{
    public class MigrationApplicationController : ControllerBase
    {
        private readonly InventariosContext _context;
        private readonly ConstantsAPI _constants;

        public MigrationApplicationController(InventariosContext context, ConstantsAPI constants)
        {
            _context = context;
            _constants = constants;
        }

        [HttpGet("applyMigrations/{hash}")]
        public async Task<ActionResult> applyMigrations(string hash)
        {
            try
            {
                if (hash != null)
                {
                    var newHash = new UserServices(_context).GetSHA256(hash);
                    if (newHash == _constants.APIKey)
                    {
                        await _context.Database.MigrateAsync();
                        return StatusCode(StatusCodes.Status200OK, new { message = "Migration apply successfully" });
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "The Key doesn´t match" });
                }
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "You don´t have permission for execute this Endpoint" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}
