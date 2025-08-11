using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.MyApp.Domain.DTO.Request
{
    public class CreateSeatRequest
    {
        public string RowChar { get; set; }
        public int ColNumber { get; set; }
        public int SeatColorId { get; set; }
        public string? Floor { get; set; }
    }
}
