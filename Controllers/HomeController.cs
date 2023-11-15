﻿using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
           _context = context;
        }
        public IActionResult CreateNewRecord(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
			var hotel = _context.hotel.ToList();

            return View("Index",hotel);
        }
        public IActionResult Update(Hotel hotel) 
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit");
        }
        public IActionResult Edit(int id) 
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.id == id);
            return View(hoteledit);
        }
        public IActionResult Delete(int id)
        {
            var hoteldelete = _context.hotel.SingleOrDefault(x => x.id== id);
            _context.hotel.Remove(hoteldelete);
            _context.SaveChanges(); 
            return RedirectToAction("Index");
        
        }

		public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();

            return View(hotel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}