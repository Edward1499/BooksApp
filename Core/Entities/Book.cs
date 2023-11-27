using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Book
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Number_of_pages { get; set; }
        public string Publish_date { get; set; }
        public string ISBN { get; set; }
        public List<Author> Authors { get; set; }
    }
}
