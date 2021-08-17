using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers.Dto
{
    /// <summary>
    /// 加密输入，密码输入，不用管
    /// </summary>
    public class UerLoginEncryptInput
    {
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string userPass { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string validateCode { get; set; }
    }
}
