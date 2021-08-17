using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 用户列表输出之地
    /// </summary>
    public class UserListOutput
    {
        /// <summary>
        /// 结果信息
        /// </summary>
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }

        /// <summary>
        /// 用户列表信息
        /// </summary>
        public List<UserInfo> listUserInfo { get; set; }

    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo {
        /// <summary>
        /// 用户Id
        /// </summary>
        public System.Guid UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String UserAccount { get; set; }
        /// <summary>
        /// 用户账户
        /// </summary>
        public System.String UserName { get; set; }
        /// <summary>
        /// 用户性别（1收入 2支出）
        /// </summary>
        public System.String UserSex { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public System.String UserTitles { get; set; }
        /// <summary>
        /// 用户部门
        /// </summary>
        public System.Guid UserDept { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public System.String UserMobilePhone { get; set; }
        /// <summary>
        ///性别名字（收入或支出）
        /// </summary>
        public System.String SexName { get; set; }

        /// <summary>
        /// 角色名字
        /// </summary>
        public System.String RoleName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string riqi { get; set; }
       
        ///// <summary>
        ///// 输出日期
        ///// </summary>
        //public string[] riqi { get; set; }
        /// <summary>
        /// 时间名字
        /// </summary>
        public System.String riqiName { get; set; }

        /// <summary>
        /// 开始
        /// </summary>
        public System.DateTime kaishi { get; set; }
        /// <summary>
        /// 结束
        /// </summary>
        public System.DateTime jieshu { get; set; }
    }
}
