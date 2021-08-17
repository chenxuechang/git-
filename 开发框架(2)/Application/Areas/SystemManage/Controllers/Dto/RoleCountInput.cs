using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 角色查询输入参数//通用的不用改
    /// </summary>
    public class RoleSearchInput
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }

        /// <summary>
        /// 第X页页码（从1开始）
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
    }
}
