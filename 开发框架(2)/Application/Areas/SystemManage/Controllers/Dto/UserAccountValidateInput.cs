using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 用户帐号验证输入
    /// </summary>
    public class UserAccountValidateInput
    {
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string userAccount { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }
    }
}
