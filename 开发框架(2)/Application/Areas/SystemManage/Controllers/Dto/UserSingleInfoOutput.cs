using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 用户信息输出//不用改
    /// </summary>
    public class UserSingleInfoOutput
    {
        /// <summary>
        /// 结果信息
        /// </summary>
       public  Application.Controllers.Dto.ResultInfo resultInfo { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public Dto.UserSaveInput userSaveInput { get; set; }
        /// <summary>
        /// 时间信息输出
        /// </summary>
        public Dto.UserSaveInput riqi { get; set; }
        public string riqiNe { get; set; }
    }
}
