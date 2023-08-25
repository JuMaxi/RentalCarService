using System.Collections.Generic;
using System;

namespace RentalCarService.Models.Responses
{
    public class BookingResponse
    {
        public string Code { get; set; }
        public string User { get; set; }
        public string Category { get; set; }
        public string BranchGet { get; set; }
        public string BranchReturn { get; set; }
        public List<ExtraResponse> Extras { get; set; }
        public double Total { get; set; }
        public DateTime Start { get; set; }
        public DateTime Return { get; set; }
    }
}
