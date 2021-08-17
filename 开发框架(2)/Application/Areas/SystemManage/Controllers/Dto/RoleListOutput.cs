using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 角色列表输出
    /// </summary>
    public class RoleListOutput
    {
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<Dal.T_RoleManage> listRoleManage { get; set; }
    }
}
