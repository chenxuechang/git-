using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 角色下拉输出参数
    /// </summary>
    public class DrpListRoleOutput
    {
        /// <summary>
        ///结果信息//不用改
        /// </summary>
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }

        /// <summary>
        /// 角色列表改为成员列表
        /// </summary>
        public List<Dal.T_RoleManage> listRoleManage { get; set; }

    }
}
