using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers.Dto
{
    /// <summary>
    /// 用户登录输出//得改，但用户不是管理员
    /// </summary>
    public class UserLoginOutput
    {
        /// <summary>
        /// 结果信息
        /// </summary>
        public ResultInfo ResultInfo { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }


        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 用户拥有的角色，以"$"连接
        /// </summary>
        public string UserRoleIds { get; set; }

        /// <summary>
        /// 用户授权token
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// 用户授权日期，哈哈
        /// </summary>
        public string riqi { get; set; }
    }
}
