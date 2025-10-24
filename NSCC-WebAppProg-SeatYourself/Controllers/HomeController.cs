using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSCC_WebAppProg_SeatYourself.Data;
using NSCC_WebAppProg_SeatYourself.Models;
using System.Diagnostics;

namespace NSCC_WebAppProg_SeatYourself.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly NSCC_WebAppProg_SeatYourselfContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(NSCC_WebAppProg_SeatYourselfContext context,ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Occasion
                .Include(o => o.Venue)
                .OrderBy(o => o.Date)
                .ToListAsync());
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
