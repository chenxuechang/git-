using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 菜单保存输入参数//不用改
    /// </summary>
    public class MenuSaveInput
    {
        /// <summary>
        /// 菜单信息
        /// </summary>
       public  Dal.T_Menu menuInfo { get; set; }
    }
}
