using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 应该是管理职称的或者是管理权限的，暂时用不上
    /// </summary>
    [Area("SystemManage")]//系统管理
    public class FuncManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FuncList() {
            return View();
        }
    }
}
