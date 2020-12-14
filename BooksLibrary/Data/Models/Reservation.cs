using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Data.Models
{
    public class Reservation : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        
        [ForeignKey("OwnerFK")]
        public string Owner { get; set; }
        public IdentityUser OwnerFK { get; set; }
        

        [ForeignKey("BookFK")]
        public Guid Book { get; set; }
        public Book BookFK { get; set; }

    }
}
