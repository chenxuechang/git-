using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers.Dto
{
    /// <summary>
    /// 应该是excel导出出口//也不用管
    /// </summary>
    public class ExportExcelOutput
    {
        public ResultInfo resultInfo { get; set; }
        /// <summary>
        /// 下载文件路径
        /// </summary>
        public string downLoadPath { get; set; }
    }
}
