using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    /// <summary>
    /// 登陆控制器暂时用不上
    /// </summary>
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
