namespace CarManagementSystem
{
    public class Car
    {
        public int Id { get; set; }
        public string model { get; set; }
        public decimal price { get; set; }
       
        public Boolean isAvailable { get; set; }
        public string? secret { get; set; }
    }
}
