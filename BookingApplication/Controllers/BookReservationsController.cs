using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApplication.Data;
using BookingApplication.Models;

namespace BookingApplication.Controllers
{
    public class BookReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookReservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookReservations.Include(b => b.BookingList).Include(b => b.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookReservations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservations
                .Include(b => b.BookingList)
                .Include(b => b.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReservation == null)
            {
                return NotFound();
            }

            return View(bookReservation);
        }

        // GET: BookReservations/Create
        public IActionResult Create()
        {
            ViewData["BookingListId"] = new SelectList(_context.BookingLists, "Id", "Id");
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id");
            return View();
        }

        // POST: BookReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberOfNights,ReservationId,BookingListId")] BookReservation bookReservation)
        {
            if (ModelState.IsValid)
            {
                bookReservation.Id = Guid.NewGuid();
                _context.Add(bookReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingListId"] = new SelectList(_context.BookingLists, "Id", "Id", bookReservation.BookingListId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", bookReservation.ReservationId);
            return View(bookReservation);
        }

        // GET: BookReservations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservations.FindAsync(id);
            if (bookReservation == null)
            {
                return NotFound();
            }
            ViewData["BookingListId"] = new SelectList(_context.BookingLists, "Id", "Id", bookReservation.BookingListId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", bookReservation.ReservationId);
            return View(bookReservation);
        }

        // POST: BookReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NumberOfNights,ReservationId,BookingListId")] BookReservation bookReservation)
        {
            if (id != bookReservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookReservationExists(bookReservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingListId"] = new SelectList(_context.BookingLists, "Id", "Id", bookReservation.BookingListId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", bookReservation.ReservationId);
            return View(bookReservation);
        }

        // GET: BookReservations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservations
                .Include(b => b.BookingList)
                .Include(b => b.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReservation == null)
            {
                return NotFound();
            }

            return View(bookReservation);
        }

        // POST: BookReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bookReservation = await _context.BookReservations.FindAsync(id);
            if (bookReservation != null)
            {
                _context.BookReservations.Remove(bookReservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookReservationExists(Guid id)
        {
            return _context.BookReservations.Any(e => e.Id == id);
        }
    }
}
