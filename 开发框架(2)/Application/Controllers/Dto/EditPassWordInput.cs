using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers.Dto
{
    /// <summary>
    /// 修改密码输入//不用管
    /// </summary>
    public class EditPassWordInput
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 老密码
        /// </summary>
        public string oldPass { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string newPass { get; set; }
    }
}
