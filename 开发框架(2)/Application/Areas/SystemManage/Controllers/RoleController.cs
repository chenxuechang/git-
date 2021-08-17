using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 角色控制，也不用管
    /// </summary>
    [Area("SystemManage")]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
