using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class ExcelImportExportDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
