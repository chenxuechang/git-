using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 数量结果
    /// </summary>
    public class UserCountOutput
    {
        /// <summary>
        /// 结果信息
        /// </summary>
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }
        /// <summary>
        ///用户数量
        /// </summary>
        public int userCount { get; set; }
    }
}
