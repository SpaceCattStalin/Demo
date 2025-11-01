using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ImgUrl { get; set; }
    }
}
