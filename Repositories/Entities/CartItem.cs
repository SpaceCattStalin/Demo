using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }     // PK
        public int CartId { get; set; }         // FK to Cart
        public Cart Cart { get; set; } = null!;

        public int ProductId { get; set; }      // FK to Product
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }

}
