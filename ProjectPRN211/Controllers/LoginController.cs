using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Models;
namespace ProjectPRN211.Controllers
{
    public class LoginController : Controller
    {
        MyOrderContext context = new MyOrderContext();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(TblUser user)
        {
            if (ModelState.IsValid)
            {
                TblUser result = context.TblUsers.FirstOrDefault(x => x.Username == user.Username && x.Pass == user.Pass);
                if (result != null)
                {
                    return RedirectToAction("Tasks", "Home");
                }
            }
            return View();
        }
    }
}
