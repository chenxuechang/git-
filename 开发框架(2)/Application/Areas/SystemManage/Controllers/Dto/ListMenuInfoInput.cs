using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 菜单列表输入参数//不用改
    /// </summary>
    public class ListMenuInfoInput
    {
        /// <summary>
        ///父菜单ID
        /// </summary>
        public string parentId { get; set; }
    }
}
