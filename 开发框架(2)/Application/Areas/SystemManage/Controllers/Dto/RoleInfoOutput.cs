using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 角色信息输出参数//通用的不用改
    /// </summary>
    public class RoleInfoOutput
    {
        /// <summary>
        /// 访问结果
        /// </summary>
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }
        /// <summary>
        /// 角色和菜单信息
        /// </summary>
        public RoleAndMenuInfo RoleAndMenuInfo { get; set; }
    }

    /// <summary>
    /// 角色和菜单信息
    /// </summary>
    public class RoleAndMenuInfo
    {
        /// <summary>
        /// 角色信息//通用的不用改
        /// </summary>
        public Dal.T_RoleManage roleManage { get; set; }
        /// <summary>
        /// 角色所拥有的菜单权限//权限区域勾选就行，不用管
        /// </summary>
        public List<Guid> listRoleMenu { get; set; }
    }
}
