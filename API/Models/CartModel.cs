using API.Models;

namespace API.Models
{
    public class CartModel
    {
        public int Id { get; set; }
    }

    public class AddCartItemDTO
    {
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
    }
    public class UpdateCartItemDTO
    {
        public int Quantity { get; set; }
        public int ProductVariantId { get; set; }
    }

    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int ProductVariantId { get; set; }

        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public string Color { get; set; }
        public int? SizeId { get; set; }
        public CartItemSizeDTO Size { get; set; }
        public string ImgUrl { get; set; }
    }

    public class CartItemSizeDTO
    {
        public string SizeType { get; set; }
        public int StockQuantity { get; set; }
    }

    //public class ProductVariantCartItemDTO
    //{
    //    public int ProductVariantId { get; set; }
    //    public string VariantCode { get; set; }
    //    public string Name { get; set; }
    //    public string Color { get; set; }
    //    public string Size { get; set; }
    //    public string ImgUrl { get; set; }

    //}

    //public class ProductVariantImageCartItemDTO
    //{
    //    public string Url { get; set; }
    //}

    //public class ProductVariantSizeCartItemDTO
    //{
    //    public string Size { get; set; }
    //}

}
