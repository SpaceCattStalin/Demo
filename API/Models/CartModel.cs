using API.Models;

namespace API.Models
{
    public class CartModel  
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }


        public virtual ProductModel Product { get; set; } = null!;
    }
}
