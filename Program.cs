using CarManagementSystem;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarDbContext>(x=>x.UseInMemoryDatabase("CarDb"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); 

var app = builder.Build();

RouteGroupBuilder carApi=app.MapGroup("/api/v1/cars");

carApi.MapGet("/", GetAllCars);
carApi.MapGet("/{id:int}", GetCarById);
carApi.MapPost("/AddCar", CreateACar);
carApi.MapPut("/updateCar/{id:int}", UpdateCar);
carApi.MapDelete("/deleteCar/{id:int}", DeleteCar);


app.Run();

static async Task<IResult> GetAllCars(CarDbContext db)
{
    return TypedResults.Ok(await db.Cars.Select(x=>new CarDtos(x)).ToListAsync());
   
}

static async Task<IResult>CreateACar(Car car, CarDbContext db)

{
    db.Cars.Add(car);
   await db.SaveChangesAsync();
    
    var result=new CarDtos(car);
    return TypedResults.Created($"AddCar/{car.Id}",result);   
}

static async Task<IResult> GetCarById(int id, CarDbContext db)
{
    var foundcar=await db.Cars.FindAsync(id);
    if(foundcar==null)
    {
        return TypedResults.NotFound();
    }
    var result = new CarDtos(foundcar);
    return TypedResults.Ok(result);
    //return TypedResults.Ok(foundcar); this will return all properties including secret property
}

static async Task<IResult> DeleteCar(int id, CarDbContext db)
{
    if (id <= 0)
        return TypedResults.BadRequest(new { message = "Invalid ID, Try Again" });

    var foundcar = await db.Cars.FindAsync(id);
    if (foundcar == null)
        return TypedResults.NotFound(new { message = $"Car with ID {id} not found" });

    var result= new CarDtos(foundcar);
    db.Cars.Remove(foundcar);
    await db.SaveChangesAsync();
    return TypedResults.Ok(new { message = "Car deleted successfully", result });

}


static async Task<IResult> UpdateCar(int id, CarDtos cardto ,CarDbContext db)
{
    if (id <= 0)
        return TypedResults.BadRequest(new { message = "Invalid ID, Try Again" });
    if (cardto == null)
        return TypedResults.BadRequest(new { message = "Request body cannot be empty" });

    var foundcar = await db.Cars.FindAsync(id);
    if (foundcar == null)
        return TypedResults.NotFound(new { message = $"Car with ID {id} not found" });

    foundcar.model=cardto.model;
    foundcar.price=cardto.price;
    foundcar.isAvailable=cardto.isAvailable;
    await db.SaveChangesAsync();

    return TypedResults.Ok(new CarDtos(foundcar));


}

















