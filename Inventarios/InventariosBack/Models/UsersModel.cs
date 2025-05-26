using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventariosModel.Models
{
    [Table("users")]
    public class UsersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId {  get; set; }
        public string Name { get; set; }
        public string LastName {  get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        [ForeignKey("Role")]
        public int RoleId {  get; set; }
        public virtual RolesModel  Role { get; set; }
        public int UserStatus { get; set; }
    }
}
