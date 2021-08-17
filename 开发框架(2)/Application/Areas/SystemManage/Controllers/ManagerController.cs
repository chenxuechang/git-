using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 管理控制器，关于权限的暂时都不用管，用不上，技术也不够
    /// </summary>
    [Area("SystemManage")]
    
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
