using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 菜单名称是否已存在输入参数//不用改
    /// </summary>
    public class MenuNameExistInput
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuName { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 当前菜单Id
        /// </summary>
        public string menuId { get; set; }
    }
}
