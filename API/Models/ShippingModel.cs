using Repositories.Entities;

namespace API.Models
{
    public class ShippingModel
    {
        public int Id { get; set; }

        public string Status { get; set; } = null!;

        public string StartAddress { get; set; } = null!;

        public string EndAddress { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }
    }
}
