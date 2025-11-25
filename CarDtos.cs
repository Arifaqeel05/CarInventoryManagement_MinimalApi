namespace CarManagementSystem
{
    public class CarDtos
    {
        public int Id { get; set; }
        public string model { get; set; }
        public decimal price { get; set; }

        public Boolean isAvailable { get; set; }

        public CarDtos(){ } //parameterless constructor.if i do not create it , it will give error while fetching data from database

        public CarDtos(Car car)//constructor with parameter of Car type
        {
            Id = car.Id;
            model = car.model;
            price = car.price;
            isAvailable = car.isAvailable;

        }
    }
}
