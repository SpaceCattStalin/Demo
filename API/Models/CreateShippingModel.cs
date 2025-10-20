namespace API.Models
{
    public class CreateShippingModel
    {
        public int Id { get; set; }

        public string StartAddress { get; set; } = null!;

        public string EndAddress { get; set; } = null!;
    }
}
