using FluentAssertions;
using RentalCarService.Mappers;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;

namespace RentarlCars.Tests.Mappers
{
    public class CarMapperTests
    {
        [Fact]
        public void Checking_if_CarRequest_is_Equal_Car()
        {
            CarRequest carRequest = new()
            {
                BranchId = 1,
                Model = "Chevrolet Onix",
                Year = new DateTime(2023, 01, 10),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = "S",
                TrunkSize = 5,
                NumberPlate = "KLO-74TR",
                CategoryId = 1002,
                BrandId = 1003
            };

            CarMapper carMapper = new();
            Car car = carMapper.ConvertCarRequest(carRequest);

            car.Branch.Id.Should().Be(carRequest.BranchId);
            car.Model.Should().Be(carRequest.Model);
            car.Year.Should().Be(carRequest.Year);
            car.Transmission.Should().Be(carRequest.Transmission);
            car.Doors.Should().Be(carRequest.Doors);
            car.Seats.Should().Be(carRequest.Seats);
            car.AirConditioner.Should().Be(carRequest.AirConditioner);
            car.TrunkSize.Should().Be(carRequest.TrunkSize);
            car.NumberPlate.Should().Be(carRequest.NumberPlate);
            car.Category.Id.Should().Be(carRequest.CategoryId);
            car.Brand.Id.Should().Be(carRequest.BrandId);

        }

        [Fact]
        public void Checking_if_CarResponse_is_Equal_Car()
        {
            List<Car> cars = new()
            {
                new Car
                {
                    Brand = new(){Brand = "Ford"},
                    Model = "Chevrolet Onix",
                    Year = new DateTime(2023, 01, 10),
                    Transmission = "Automatic",
                    Doors = 5, 
                    Seats = 5,
                    AirConditioner = "S",
                    TrunkSize = 5,
                    NumberPlate = "JJJ-52P9",
                    Category = new() {Description = "Economy"},
                    Branch = new() {Name = "Happy Cars Ltda"}
                }
            };

            CarMapper carMapper = new();
            List<CarResponse> carResponse = carMapper.ConvertCarResponse(cars);

            carResponse[0].Brand.Should().Be(cars[0].Brand.Brand);
            carResponse[0].Model.Should().Be(cars[0].Model);
            carResponse[0].Year.Should().Be(cars[0].Year);
            carResponse[0].Transmission.Should().Be(cars[0].Transmission);
            carResponse[0].Doors.Should().Be(cars[0].Doors);
            carResponse[0].Seats.Should().Be(cars[0].Seats);
            carResponse[0].AirConditioner.Should().Be(cars[0].AirConditioner);
            carResponse[0].TrunkSize.Should().Be(cars[0].TrunkSize);
            carResponse[0].NumberPlate.Should().Be(cars[0].NumberPlate);
            carResponse[0].Category.Should().Be(cars[0].Category.Description);
            carResponse[0].Branch.Should().Be(cars[0].Branch.Name);
        }
    }
}
