using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using System;
using System.Collections.Generic;

namespace RentalCarService.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingDBAccess _bookingDBAccess;
        IValidateBooking _validateBook;
        IAvailabilityService _availabilityService;
        private readonly ICategoriesDBAccess _categoriesDBAccess;
        private readonly IBranchesDBAccess _branchesDBAccess;
        private readonly IUserDBAccess _userDBAccess;
        private readonly IExtraDBAccess _extraDBAccess;

        public BookingService(IBookingDBAccess bookingDBAccess, IValidateBooking validateBook, 
            IAvailabilityService availabilityService, ICategoriesDBAccess categoriesDBAccess, 
            IBranchesDBAccess branchesDBAccess, IUserDBAccess userDBAccess, IExtraDBAccess extraDBAccess)
        {
            _bookingDBAccess = bookingDBAccess;
            _validateBook = validateBook;
            _availabilityService = availabilityService;
            _categoriesDBAccess = categoriesDBAccess;
            _branchesDBAccess = branchesDBAccess;
            _userDBAccess = userDBAccess;
            _extraDBAccess = extraDBAccess;
        }

        public List<AvailabilityResponse> ReturnAvailabilityCategories(AvailabilityRequest availability)
        {
            // Teste 1: Quando ha categorias no banco de dados, mas estao todas indisponiveis para a data selecionada, deve retornar uma lista vazia (0 items)
            // Teste 2: Quando ha 3 categorias e 2 estao disponiveis nas datas, deve retornar 2 e nao 3.
            // Teste 3: Dado que ha 1 categoria e esta disponivel, devemos retornar a estimativa de preco correta

            List<Categories> categories = _availabilityService.SaveListAvailableCategories(availability);
            List<AvailabilityResponse> availabilityCategories = new List<AvailabilityResponse>();

            Branchs branch = FindBranchDB(availability.BranchGetCar);


            foreach (Categories category in categories)
            {
                AvailabilityResponse av = new AvailabilityResponse();

                av.Branch = branch.Name;
                av.Category = category.Description;
                av.StartDay = availability.StartDay;
                av.ReturnDay = availability.ReturnDay;

                Booking booking = new Booking();

                booking.Category = category;
                booking.StartDay = availability.StartDay;
                booking.ReturnDay = availability.ReturnDay;

                double price = ValueToPayPerDay(booking);

                av.Estimative = price;

                availabilityCategories.Add(av);
            }

            return availabilityCategories;
        }

        public void InsertNewBook(Booking booking)
        {
            // Teste 1: Quando a validacao falhar, deve lancar exception (ou seja, nao insere no banco)
            // Teste 2: Quando a validacao passar, deve incluir no banco de dados
            // Teste 3: O numero da reserva deve ter 6 posicoes, e elas podem ser apenas letras e numeros
            // Teste 4: CALCULO PRECO: Dado que o horario de devolucao seja SUPERIOR ao de coleta, deve cobrar uma diaria adicional
            // Teste 5: CALCULO PRECO: Dado que o horario de devolucao seja IGUAL, NAO COBRAR uma diaria adicional
            // Teste 6: CALCULO PRECO: Dado que temos duas reservas iguais, o valor a pagar deve ser igual
            // Teste 7: CALCULO PRECO: Dado que temos uma reserva com booking extra e outra sem, o valor a pagar deve ser diferente
            // Teste 8: CALCULO PRECO: (Testar cenario com DayCost)
            // Teste 9: CALCULO PRECO: (Testar cenario com FixedCost)

            _validateBook.Validate(booking);

            booking.Category = FindCategoryDB(booking);
            booking.BranchGet = FindBranchDB(booking.BranchGet.Id);
            booking.BranchReturn = FindBranchDB(booking.BranchReturn.Id);
            booking.User = FindUserFromDB(booking.User.Id);
            booking.Code = GenerateBookNumber();
            booking.BookExtra = SaveExtraToBookExtra(booking);
            booking.ValueToPay = CalculateValueToPay(booking);

            _bookingDBAccess.AddNewBook(booking);
        }

        private List<BookingExtra> SaveExtraToBookExtra(Booking booking)
        {
            List<Extraa> listExtra = FindExtraDB(booking);
            for (int i = 0; i < listExtra.Count; i++)
            {
                booking.BookExtra[i].Extra = listExtra[i];
            }
            return booking.BookExtra;
        }
        private string GenerateBookNumber()
        {
            int size = 6;
            Random random = new Random();
            string AlphabetNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = "";


            char[] chars = new char[size];
            for (int Position = 0; Position < size; Position++)
            {
                chars[Position] = AlphabetNumbers[random.Next(AlphabetNumbers.Length)];
                code = code + chars[Position];
            }

            return CheckBookNumberCodeDB(code);
        }

        private string CheckBookNumberCodeDB(string code)
        {
            var Book = _bookingDBAccess.GetBookingByCode(code);

            if (Book != null)
            {
                code = GenerateBookNumber();
            }
            return code;
        }

        private double CalculateValueToPay(Booking booking)
        {
            double TotalValueToPay = ValueToPayPerDay(booking) + CalculateExtraCosts(booking);

            return TotalValueToPay;
        }

        private double ValueToPayPerDay(Booking booking)
        {
            Categories category = FindCategoryDB(booking);

            int daysTotal = CalculateDaysBook(booking);

            double PriceDay = 0;


            foreach (PriceBands p in category.PriceBands)
            {
                if (daysTotal >= p.MinDays && daysTotal <= p.MaxDays)
                {
                    PriceDay = p.Price;
                    break;
                }
            }

            double PriceTotalDays = PriceDay * daysTotal;
            return PriceTotalDays;
        }

        private double CalculateExtraCosts(Booking booking)
        {
            int DaysTotal = CalculateDaysBook(booking);

            double ExtraCosts = 0;

            List<Extraa> listExtra = FindExtraDB(booking);

            foreach (Extraa b in listExtra)
            {
                ExtraCosts = ExtraCosts + b.DayCost * DaysTotal;
                ExtraCosts += b.FixedCost;
            }
            return ExtraCosts;
        }

        private List<Extraa> FindExtraDB(Booking booking)
        {
            return _extraDBAccess.GetExtraDB(booking);
        }

        private Branchs FindBranchDB(int id)
        {
            return _branchesDBAccess.GetBranchById(id);
        }

        private Categories FindCategoryDB(Booking booking)
        {
            return _categoriesDBAccess.GetCategoryById(booking.Category.Id);
        }

        private User FindUserFromDB(int Id)
        {
            return _userDBAccess.GetUserByIdThenInclude(Id);
        }

        private int CalculateDaysBook(Booking booking)
        {
            if (booking.ReturnDay.Hour <= booking.StartDay.Hour)
            {
                return (booking.ReturnDay.Day - booking.StartDay.Day);
            }
            else
            {
                return (booking.ReturnDay.Day - booking.StartDay.Day) + 1;
            }
        }

        public List<Booking> ReadBookingsFromDB()
        {
            return _bookingDBAccess.GetBookingThenInclude();
        }
    }
}



