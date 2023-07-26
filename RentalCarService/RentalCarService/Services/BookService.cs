using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RentalCarService.Services
{
    public class BookService : IBookService
    {
        private readonly RentalCarsDBContext _dbContext;

        public BookService(RentalCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertNewBook(Book book)
        {
            book.Code = GenerateBookNumber();

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        private string GenerateBookNumber()
        {
            int size = 6;
            Random random = new Random();
            string AlphabetNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


            char[] chars = new char[size];
            for(int Position = 0; Position < size; Position++)
            {
                chars[Position] = AlphabetNumbers[random.Next(AlphabetNumbers.Length)];
                   
            }
            
            CheckBookNumberCodeDB(chars.ToString());
            return chars.ToString();
        }

        private void CheckBookNumberCodeDB(string Code)
        {
            var Book = _dbContext.Books.Find(Code);

            if(Code == Book.Code)
            {
                GenerateBookNumber();
            }
        }

        

       
    
    }
} 
    


