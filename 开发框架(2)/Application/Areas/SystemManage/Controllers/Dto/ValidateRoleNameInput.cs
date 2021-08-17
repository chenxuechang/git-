using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 应该是验证输入名称的//没想好咋用
    /// </summary>
    public class ValidateRoleNameInput
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string roleId { get; set; }
    }
}
