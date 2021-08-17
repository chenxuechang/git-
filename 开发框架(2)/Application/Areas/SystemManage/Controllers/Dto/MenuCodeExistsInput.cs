using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 菜单编码已存在输入参数//不用改
    /// </summary>
    public class MenuCodeExistsInput
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string menuCode { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string menuId { get; set; }
    }
}
