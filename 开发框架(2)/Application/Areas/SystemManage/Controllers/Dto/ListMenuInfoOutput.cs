using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 菜单列表输出参数//不用改
    /// </summary>
    public class ListMenuInfoOutput
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<Dal.T_Menu> listMenu { get; set; }
    }
}
