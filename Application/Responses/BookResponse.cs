using Core.Entities;
using Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class BookResponse
    {
        public int RowNumber { get; set; }
        public BookTypes Type { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string NumberPages { get; set; }
        public string PublishDate { get; set; }
        public string ISBN { get; set; }
        public string Authors { get; set; }
    }
}
