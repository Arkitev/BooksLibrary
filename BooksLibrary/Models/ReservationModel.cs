using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Models
{
    public class ReservationModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string OwnerID { get; set; }
        public Guid BookID { get; set; }
    }
}
