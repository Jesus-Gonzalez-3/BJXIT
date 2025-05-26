using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventariosModel.Models
{
    [Table("order")]
    public class OrdersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Created")]
        public int CreatedBy { get; set; }
        public virtual UsersModel Created {  get; set; }
        public int Quantity { get; set; }
        public string CustomerName { get; set; }
        [ForeignKey("Products")]
        public int ProductId {  get; set; }
        public virtual ProductModel Product { get; set; }
        public int OrderStatus { get; set; }
        public float Total {  get; set; }
    }
}
