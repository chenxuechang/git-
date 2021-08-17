using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers.Dto
{
    /// <summary>
    /// 用户登录输入
    /// </summary>
    public class UserLoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

    }
}
