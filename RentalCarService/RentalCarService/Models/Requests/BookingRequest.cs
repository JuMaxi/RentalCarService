using System.Collections.Generic;
using System;

namespace RentalCarService.Models.Requests
{
    public class BookingRequest
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int BranchGetId { get; set; }
        public int BranchReturnId { get; set; }
        public List<int> Extras { get; set; }
        public DateTime Start { get; set; }
        public DateTime Return { get; set; }
    }
}
