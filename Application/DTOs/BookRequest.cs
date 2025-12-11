using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
