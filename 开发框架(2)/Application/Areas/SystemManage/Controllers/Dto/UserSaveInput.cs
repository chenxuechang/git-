using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 用户保存输入//很重要
    /// </summary>
    public class UserSaveInput
    {
        /// <summary>
        /// 用户ID通用的
        /// </summary>
        public System.Guid UserId { get; set; }

        /// <summary>
        /// 帐号改为用途
        /// </summary>
        public System.String UserAccount { get; set; }

        /// <summary>
        /// 姓名//日期
        /// </summary>
        public System.String UserName { get; set; }

        /// <summary>
        /// 保存输入时间
        /// </summary>
        public DateTime riqi { get; set; }
        /// <summary>
        /// 保存输入日期
        /// </summary>
        public string[] shuriqi { get; set; }

        /// <summary>
        /// 性别（1收入2支出）
        /// </summary>
        public System.String UserSex { get; set; }//应该给他来个枚举的




        /// <summary>
        /// 职称改为备注
        /// </summary>
        public System.String UserTitles { get; set; }

        ///// <summary>
        ///// 所属部门
        ///// </summary>
        //public System.Guid UserDept { get; set; }

        /// <summary>
        /// 办公电话，没用
        /// </summary>
        public System.String UserTelPhone { get; set; }

        /// <summary>
        /// 手机改为金额
        /// </summary>
        public System.String UserMobilePhone { get; set; }

        /// <summary>
        /// E-mail//没用
        /// </summary>
        public System.String UserEmail { get; set; }

        /// <summary>
        /// 用户角色//没想好咋用
        /// </summary>
        public List<Guid> UserRoles { get; set; }
       
        /// <summary>
        /// 保存数入开始
        /// </summary>
        public System.DateTime kaishi { get; set; }
        /// <summary>
        /// 保存数入结束
        /// </summary>
        public System.DateTime jieshu { get; set; }

        //public static implicit operator string(UserSaveInput v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
