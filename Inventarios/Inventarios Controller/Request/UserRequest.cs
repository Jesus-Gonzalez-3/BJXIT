using Microsoft.EntityFrameworkCore;

namespace Inventarios_Controller.Request
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
    
    public class UpdateUserRequest
    {
        public int UserId {  get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
