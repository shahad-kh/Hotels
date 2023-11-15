using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            { 
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                TempData["Edi"] = "Yes";

                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        public IActionResult Edit(int id)
        {
            var hotelEdi = _context.hotel.SingleOrDefault(x => x.id == id);
            return View(hotelEdi);
        }
        public IActionResult Delete(int id)
        {
            var hotelDel = _context.hotel.SingleOrDefault(x => x.id == id);
            if (hotelDel != null)
            {
                _context.hotel.Remove(hotelDel);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }
            return RedirectToAction("Index");
        }
        public IActionResult CreateNewRoomDetails(RoomDetails roomDetails)
        {
            _context.roomDetails.Add(roomDetails);
            _context.SaveChanges();
            return RedirectToAction("RoomDetails");
        }

        public IActionResult RoomDetails()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            var rooms = _context.rooms.ToList();
            ViewBag.rooms = rooms;

			ViewBag.currentuser = HttpContext.Session.GetString("UserName");

			var roomDetails = _context.roomDetails.ToList();

            return View(roomDetails);

        }
        public IActionResult Rooms()
        {
            
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;
            //ViewBag.currentuser =Request.Cookies["UserName"];
            ViewBag.currentuser = HttpContext.Session.GetString("UserName");
			var rooms = _context.rooms.ToList();

            return View(rooms);

        }
        public IActionResult CreateNewRooms(Rooms rooms)
        {
            _context.rooms.Add(rooms);
            _context.SaveChanges();
            return RedirectToAction("Rooms");
        }
        [HttpPost]
        public IActionResult Index(string city)
        {
            var findhotel = _context.hotel.Where(x => x.City.Contains(city));
            return View(findhotel);
        }
        [Authorize]
        public IActionResult Index()
        {
            var currentuser = HttpContext.User.Identity.Name;
            ViewBag.currentuser = currentuser;
            //CookieOptions options = new CookieOptions();
            //// option.Expirs = DateTime.Now.AddMinutes(20);
            //Response.Cookies.Append("UserName", currentuser, options);

            HttpContext.Session.SetString("UserName", currentuser);
            var hotel = _context.hotel.ToList();

            return View(hotel);
        }
        public IActionResult CreatNewHotel(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var hotel = _context.hotel.ToList();
            return View("Index", hotel);
        }
    }
    
}
