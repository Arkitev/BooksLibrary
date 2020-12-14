using BooksLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Data.Repos
{
    public class ReservationsRepo : Repository<Reservation, ApplicationDbContext>
    {
        private readonly ApplicationDbContext _context;

        public ReservationsRepo(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetUserReservations(string ownerId)
        {
            return await _context.Set<Reservation>().Where(reservation =>
            reservation.Owner == ownerId)
                .ToListAsync();
        }
    }
}
