using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentarlCars.Tests
{
    public class AvailabilityServiceTests
    {
        private List<Book> _existing;

        public AvailabilityServiceTests()
        {
            Book existingBooking1 = new Book()
            {
                StartDay = new DateTime(2023, 09, 1),
                ReturnDay = new DateTime(2023, 09, 3),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(17, 00)
            };

            Book existingBooking2 = new Book()
            {
                StartDay = new DateTime(2023, 09, 6),
                ReturnDay = new DateTime(2023, 09, 6),
                HourGetCar = new TimeOnly(09, 00),
                HourReturnCar = new TimeOnly(17, 00)
            };

            _existing = new List<Book>() { existingBooking1, existingBooking2 };
        }

        [Fact]
        public void testing_scenario_reservation_30_ago_8h_to_31_ago_17h()
        {
            //30/08, 08:00 → 31/08, 17:00 → Disponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 08, 30),
                ReturnDay = new DateTime(2023, 08, 31),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(17,00)
            };

            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeTrue();
        }

        [Fact]
        public void testing_scenario_reservation_30_ago_8h_to_01_Sep_16h()
        {
            //30/08, 08:00 → 01/09, 16:00 → Indisponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 08, 30),
                ReturnDay = new DateTime(2023, 09, 1),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(16, 00)
            };

            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeFalse();
        }

        [Fact]
        public void testing_scenario_reservation_30_ago_8h_to_04_Sep_15h()
        {
            //30/08, 08:00 → 04/09, 15:00 → Indisponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 08, 30),
                ReturnDay = new DateTime(2023, 09, 4),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(15, 00)
            };

           
            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeFalse();
        }

        [Fact]
        public void testing_scenario_reservation_30_ago_8h_to_06_Sep_14h()
        {
            //30/08, 08:00 → 06/09, 14:00 → Indisponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 08, 30),
                ReturnDay = new DateTime(2023, 09, 6),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(14, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeFalse();
        }

        [Fact]
        public void testing_scenario_reservation_30_ago_8h_to_08_Sep_17h()
        {
            //30/08, 08:00 → 08/09, 17:00 → Indisponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 08, 30),
                ReturnDay = new DateTime(2023, 09, 8),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(17, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeFalse();
        }

        [Fact]
        public void testing_scenario_reservation_04_sep_8h_to_05_Sep_17h()
        {
            //04/09, 08:00 → 05/09, 17:00 → Disponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 09, 4),
                ReturnDay = new DateTime(2023, 09, 5),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(17, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeTrue();
        }

        [Fact]
        public void testing_scenario_reservation_04_sep_8h_to_06_Sep_15h()
        {
            //04/09, 08:00 → 06/09, 15:00 → Indisponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 09, 4),
                ReturnDay = new DateTime(2023, 09, 6),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(15, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeFalse();
        }


        [Fact]
        public void testing_scenario_reservation_06_sep_8h_to_10_Sep_17h()
        {
            //06/09, 08:00 → 10/09, 17:00 → Indisponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 09, 6),
                ReturnDay = new DateTime(2023, 09, 10),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(17, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeFalse();
        }

        [Fact]
        public void testing_scenario_reservation_07_sep_8h_to_09_Sep_17h()
        {
            //07/09, 08:00 → 09/09, 17:00 → Disponivel
            Book candidate = new Book()
            {
                StartDay = new DateTime(2023, 09, 7),
                ReturnDay = new DateTime(2023, 09, 9),
                HourGetCar = new TimeOnly(08, 00),
                HourReturnCar = new TimeOnly(17, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeTrue();
        }


    }
}
