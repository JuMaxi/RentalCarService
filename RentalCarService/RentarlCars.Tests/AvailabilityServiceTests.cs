using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Services;

namespace RentarlCars.Tests
{
    public class AvailabilityServiceTests
    {
        private List<Booking> _existing;

        public AvailabilityServiceTests()
        {
            Booking existingBooking1 = new Booking()
            {
                StartDay = new DateTime(2023, 09, 1, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 3, 17, 00, 00),
            };

            Booking existingBooking2 = new Booking()
            {
                StartDay = new DateTime(2023, 09, 6, 09, 00, 00),
                ReturnDay = new DateTime(2023, 09, 6, 17, 00, 00),
            };

            _existing = new List<Booking>() { existingBooking1, existingBooking2 };

        }

        [Fact]
        public void testing_scenario_reservation_30_ago_8h_to_31_ago_17h()
        {
            //30/08, 08:00 → 31/08, 17:00 → Disponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 08, 30, 08, 00, 00),
                ReturnDay = new DateTime(2023, 08, 31, 17, 00, 00),
            };

            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeTrue();
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void testing_scenario_reservation_30_ago_8h_to_01_Sep_16h(int amountCars, bool result)
        {
            //30/08, 08:00 → 01/09, 16:00 → Indisponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 08, 30, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 1, 16, 00, 00),
            };

            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing, amountCars);

            IsAvailable.Should().Be(result);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void testing_scenario_reservation_30_ago_8h_to_04_Sep_15h(int amountCars, bool result)
        {
            //30/08, 08:00 → 04/09, 15:00 → Indisponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 08, 30, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 4, 15, 00, 00),
            };

           
            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing, amountCars);

            IsAvailable.Should().Be(result);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void testing_scenario_reservation_30_ago_8h_to_06_Sep_14h(int amountCars, bool result)
        {
            //30/08, 08:00 → 06/09, 14:00 → Indisponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 08, 30, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 6, 14, 00, 00),
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing, amountCars);

            IsAvailable.Should().Be(result);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void testing_scenario_reservation_30_ago_8h_to_08_Sep_17h(int amountCars, bool result)
        {
            //30/08, 08:00 → 08/09, 17:00 → Indisponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 08, 30, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 8, 17, 00, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing, amountCars);

            IsAvailable.Should().Be(result);
        }

        [Fact]
        public void testing_scenario_reservation_04_sep_8h_to_05_Sep_17h()
        {
            //04/09, 08:00 → 05/09, 17:00 → Disponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 09, 4, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 5, 17, 00, 00),
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeTrue();
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void testing_scenario_reservation_04_sep_8h_to_06_Sep_15h(int amountCars, bool result)
        {
            //04/09, 08:00 → 06/09, 15:00 → Indisponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 09, 4, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 6, 15, 00, 00),
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing, amountCars);

            IsAvailable.Should().Be(result);
        }


        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        public void testing_scenario_reservation_06_sep_8h_to_10_Sep_17h(int amountCars, bool result)
        {
            //06/09, 08:00 → 10/09, 17:00 → Indisponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 09, 6, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 10, 17, 00, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing, amountCars);

            IsAvailable.Should().Be(result);
        }

        [Fact]
        public void testing_scenario_reservation_07_sep_8h_to_09_Sep_17h()
        {
            //07/09, 08:00 → 09/09, 17:00 → Disponivel
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 09, 7, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 9, 17, 00, 00)
            };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing);

            IsAvailable.Should().BeTrue();
        }

        [Fact]
        public void should_consider_time_when_checking_availability()
        {
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 09, 3, 10, 00, 00),
                ReturnDay = new DateTime(2023, 09, 4, 15, 00, 00),
            };

            Booking existingBooking1 = new Booking()
            {
                StartDay = new DateTime(2023, 09, 1, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 3, 08, 00, 00)
            };

            Booking existingBooking2 = new Booking()
            {
                StartDay = new DateTime(2023, 09, 4, 17, 00, 00),
                ReturnDay = new DateTime(2023, 09, 5, 17, 00, 00)
            };

            var existing = new List<Booking>() { existingBooking1, existingBooking2 };
            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, existing);

            IsAvailable.Should().BeTrue();
        }

        [Fact]
        public void testing_scenario_2_cars_reservation_06_sep_8h_to_10_Sep_17h()
        {
            //06/09, 08:00 → 10/09, 17:00 → Disponivel (2 cars)
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 09, 6, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 10, 17, 00, 00)
            };

            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, _existing, 2);

            IsAvailable.Should().BeTrue();
        }

        [Fact]
        public void testing_scenario_reservation_where_all_cars_are_booked()
        {
            Booking candidate = new Booking()
            {
                StartDay = new DateTime(2023, 08, 30, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 6, 14, 00, 00),
            };

            Booking existingBooking1 = new Booking()
            {
                StartDay = new DateTime(2023, 09, 1, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 3, 17, 00, 00)
            };

            Booking existingBooking2 = new Booking()
            {
                StartDay = new DateTime(2023, 09, 1, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 3, 17, 00, 00)
            };

            Booking existingBooking3 = new Booking()
            {
                StartDay = new DateTime(2023, 09, 6, 09, 00, 00),
                ReturnDay = new DateTime(2023, 09, 6, 17, 00, 00),
            };

            var existing = new List<Booking>() { existingBooking1, existingBooking2, existingBooking3 };


            AvailabilityService service = new AvailabilityService();
            bool IsAvailable = service.ExistsAvailabilityForBooking(candidate, existing, 2);

            IsAvailable.Should().BeFalse();
        }
    }
}
