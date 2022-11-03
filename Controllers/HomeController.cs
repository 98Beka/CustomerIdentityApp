using CustomIdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace CustomIdentityApp.Controllers {
    [Authorize(Roles = "user")]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager) {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index() {
            if(!string.IsNullOrEmpty(User.Identity?.Name)) {
                var user = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (user != null)
                    return View(await _userManager.GetRolesAsync(user));
            } 
            return View();
        }


        public IActionResult Privacy() {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult About() {
            return Content("you are admin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}