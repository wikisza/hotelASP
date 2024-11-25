using hotelASP.Data;
using hotelASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotelASP.Controllers
{
	public class ReservationsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ReservationsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("/get_current_reservations")]
		public JsonResult GetReservations()
		{
			var today = DateTime.Today;
			var reservations = _context.Reservations
				.Where(r => r.Date_to >= today)
				.Select(r => new
				{
					start = r.Date_from.Date.AddHours(14).ToString("yyyy-MM-ddTHH:mm:ss"),
					end = r.Date_to.Date.AddHours(10).ToString("yyyy-MM-ddTHH:mm:ss"),
					title = r.First_name + ' ' + r.Last_name,
					description = $"Pokój: {r.Id_room}",
					id_room = r.Id_room
				})
				.ToList();

			return Json(reservations);
		}

		[HttpGet("/get_old_reservations")]
		public JsonResult GetOldReservations()
		{
			var yesterday = DateTime.Today.AddDays(-1);
			var oldReservations = _context.Reservations
				.Where(r => r.Date_to <= yesterday)
				.Select(r => new
				{
					start = r.Date_from.Date.AddHours(14).ToString("yyyy-MM-ddTHH:mm:ss"),
					end = r.Date_to.Date.AddHours(10).ToString("yyyy-MM-ddTHH:mm:ss"),
					title = r.First_name + ' ' + r.Last_name,
					description = $"Pokój: {r.Id_room}",
					id_room = r.Id_room
				})
				.ToList();

			return Json(oldReservations);
		}

		public IActionResult Calendar()
		{
			return View();
		}

		// GET: ReservationsController
		[Authorize]
		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async Task<IActionResult> CurrentReservations()
		{
			var today = DateTime.Today;

			var reservations = await _context.Reservations
				.Where(r => r.Date_to >= today)
				.ToListAsync();

			return View(reservations);
		}

		public async Task<IActionResult> HistoryReservations()
		{
			var yesterday = DateTime.Today.AddDays(-1);

			var reservations = await _context.Reservations
				.Where(r => r.Date_to <= yesterday)
				.ToListAsync();
			return View(reservations);
		}

		// GET: Reservations/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var reservation = await _context.Reservations
		.Select(r => new
		{
			r.Id_reservation,
			r.Date_from,
			r.Date_to,
			CheckIn = r.Date_from.Date.AddHours(14),
			CheckOut = r.Date_to.Date.AddHours(10),
			r.First_name,
			r.Last_name,
			r.Id_room
		})
		.FirstOrDefaultAsync(m => m.Id_reservation == id);

			if (reservation == null)
			{
				return NotFound();
			}

			return View(reservation);
		}

		// GET: Rooms/Create
		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id_reservation,Date_from, Date_to, First_name, Last_name, Id_room")] Reservation reservation)
		{
			if (ModelState.IsValid)
			{
				_context.Add(reservation);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(reservation);
		}

		// GET: Rooms/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var reservation = await _context.Reservations.FindAsync(id);
			if (reservation == null)
			{
				return NotFound();
			}
			return View(reservation);
		}

		// POST: Rooms/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id_reservation,Date_from, Date_to, First_name, Last_name, Id_room")] Reservation reservation)
		{
			if (id != reservation.Id_reservation)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(reservation);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ReservationExists(reservation.Id_reservation))
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
			return View(reservation);
		}

		// GET: Rooms/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var reservation = await _context.Reservations
				.FirstOrDefaultAsync(m => m.Id_reservation == id);
			if (reservation == null)
			{
				return NotFound();
			}

			return View(reservation);
		}

		// POST: Rooms/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var reservation = await _context.Reservations.FindAsync(id);
			if (reservation != null)
			{
				_context.Reservations.Remove(reservation);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ReservationExists(int id)
		{
			return _context.Reservations.Any(e => e.Id_reservation == id);
		}
	}
}

