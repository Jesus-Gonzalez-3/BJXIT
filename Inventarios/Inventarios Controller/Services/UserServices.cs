using InventariosModel.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Inventarios_Controller.Services
{
    public class UserServices
    {
        private readonly InventariosContext _context;

        public UserServices(InventariosContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, object? Data)> InsertUserAsync(string name, string lastName, string email, string password, int roleId)
        {
            using (SqlConnection conn = new SqlConnection(_context.Database.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var resultado = reader["Resultado"].ToString();

                            if (resultado == "Email ya registrado")
                            {
                                return (false, resultado, null);
                            }

                            if("Ocurrió un error al insertar el usuario" == resultado)
                            {
                                return (false, resultado, null);
                            }

                            // Si tiene más campos, leerlos
                            int userId = Convert.ToInt32(reader["UserId"]);
                            string userName = reader["Name"].ToString();
                            string userEmail = reader["Email"].ToString();
                            string roleName = reader["RoleName"].ToString();

                            return (true, resultado, new
                            {
                                UserId = userId,
                                Name = userName,
                                Email = userEmail,
                                Role = roleName
                            });
                        }
                    }
                }
            }

            return (false, "Error inesperado", null);

        }
    
        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
