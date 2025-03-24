using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.Entities;

namespace BookingPayments.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Instructor))]
    public class RoomsController : Controller
    {
        private readonly IRoomAppService _roomAppService;
        private readonly IRoomStatusAppService _roomStatusAppService;

        public RoomsController(IRoomAppService roomAppService, IRoomStatusAppService roomStatusAppService)
        {
            _roomAppService = roomAppService;
            _roomStatusAppService = roomStatusAppService;
        }

        public IActionResult Select()
        {
            var rooms = _roomAppService.Select();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Statuses = _roomStatusAppService.Select();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Rooms room)
        {
            if (ModelState.IsValid)
            {
                _roomAppService.Create(room);
                return RedirectToAction(nameof(Select));
            }
            ViewBag.Statuses = _roomStatusAppService.Select();
            return View(room);
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _roomAppService.Delete(id);
            if (deleted)
                return RedirectToAction(nameof(Select));
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var room = _roomAppService.GetById(id);
            if (room == null)
                return NotFound();

            ViewBag.Statuses = _roomStatusAppService.Select();
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Rooms room)
        {
            if (ModelState.IsValid)
            {
                _roomAppService.Edit(room);
                return RedirectToAction(nameof(Select));
            }

            ViewBag.Statuses = _roomStatusAppService.Select();
            return View(room);
        }
    }
}
