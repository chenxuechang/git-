using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    /// <summary>
    /// 主控制器，没想好干什么用的
    /// </summary>
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
