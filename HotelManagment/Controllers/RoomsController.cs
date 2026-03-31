using Microsoft.AspNetCore.Mvc;
using HotelManagment.Services;
using HotelManagment.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
public class ReservationsController : Controller
{
    private readonly IReservationService _resService;
    private readonly IRoomService _roomService;

    public ReservationsController(IReservationService resService, IRoomService roomService)
    {
        _resService = resService;
        _roomService = roomService;
    }

    public IActionResult Index()
    {
        var reservations = _resService.GetAll();
        return View(reservations);
    }

    public IActionResult Create()
    {
        ViewBag.Rooms = _roomService.GetFilteredRooms(null, null, true);
        return View();
    }

    [HttpPost]
    public IActionResult Create(Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            reservation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _resService.CreateReservation(reservation);

            return RedirectToAction(nameof(Index));
        }
        return View(reservation);
    }

    public IActionResult Details(int id)
    {
        var res = _resService.GetById(id);
        if (res == null) return NotFound();
        return View(res);
    }
}