using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{   /// <summary>
    /// 应该是角色菜单输入的地
    /// </summary>
    public class SaveRoleInfoInput
    {
        /// <summary>
        /// 角色管理
        /// </summary>
        public Dal.T_RoleManage roleManage { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<string> listRoleMenus { get; set; }
    }
}
