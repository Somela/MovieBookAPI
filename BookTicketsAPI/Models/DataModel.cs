using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookTicketsAPI.Models
{
    public class DataModel
    {
        public class GetCreditCardInfoResponse
        {
            public List<string> MovieName { get; set; }          
        }
        public class GetMovieDetailsResponse
        {
            public List<string> MovieDetails { get; set; }
            public ResponseMessage Response { get; set; }
        }
        public class ResponseMessage
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public string Severity { get; set; }
        }
        public class GetMovieInfoRequest
        {
            public string PageName { get; set; }
            public string Type { get; set; }
            public List<string> Selected { get; set; }
        }
    }
}