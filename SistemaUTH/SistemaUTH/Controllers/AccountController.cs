using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SistemaUTH.Controllers
{
    public class AccountController : Controller
    {
        //TODO: Hacer countController para los roles.
        private readonly UserManager<user> _userManager;
        public IActionResult Index()
        {
            return View();
        }
    }
}