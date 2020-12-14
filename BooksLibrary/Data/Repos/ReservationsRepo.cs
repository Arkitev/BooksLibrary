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

        public async Task<List<Reservation>> GetBookReservations(Guid bookId)
        {
            return await _context.Set<Reservation>().Where(reservation =>
            reservation.Book == bookId)
                .ToListAsync();
        }

        public async Task<Reservation> DeleteUserReservation(Guid bookId, string ownerId)
        {
            var userReservations = await GetUserReservations(ownerId);
            Reservation userReservation = userReservations.FirstOrDefault(userReservation => userReservation.Book == bookId);

            _context.Set<Reservation>().Remove(userReservation);
            await _context.SaveChangesAsync();

            return userReservation;
        }
    }
}
