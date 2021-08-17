using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 菜单保存输出参数//不用改
    /// </summary>
    public class MenuSaveOutput
    {
       public  Application.Controllers.Dto.ResultInfo resultInfo { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        public Dal.T_Menu menuInfo { get; set; }
    }
}
