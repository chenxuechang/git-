using System;

namespace Dal
{
    /// <summary>
    ///用户实体之处，按理说是最重要的，但用户部分没啥用
    /// </summary>
    public class V_User
    {
        public V_User()
        {
            Conn = "Default";
        }

        public V_User(string conn)
        {
            Conn = conn;
        }//功能通用的

        /// <summary>
        /// 数据库连接字符串KEY
        /// </summary>
        public string Conn { get; set; }
        /// <summary>
        ///用户Id//通用的
        /// </summary>
        public System.Guid UserId { get; set; }
        /// <summary>
        /// 用户账户改为用途
        /// </summary>
        public System.String UserAccount { get; set; }
        /// <summary>
        /// 用户密码改为好像还没用到
        /// </summary>
        public System.String UserPassword { get; set; }

        /// <summary>
        /// 用户名字改为日期
        /// </summary>
        public System.String UserName { get; set; }
        /// <summary>
        /// 用户性别//改为类型
        /// </summary>
        public System.String UserSex { get; set; }
        
        /// <summary>
        /// 用户名称//还没用到
        /// </summary>
        public System.String UserTitles { get; set; }
        /// <summary>
        /// 用户部门没用
        /// </summary>
        public System.Guid UserDept { get; set; }
        /// <summary>
        /// 用户电话这个不要了
        /// </summary>
        public System.String UserTelPhone { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        public DateTime riqi { get; set; }


        public System.String[]shuriqi { get; set; } 


        //public object GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 用户手机改为金额
        /// </summary>
        public System.String UserMobilePhone { get; set; }
        /// <summary>
        /// 用户邮箱没用
        /// </summary>
        public System.String UserEmail { get; set; }
        /// <summary>
        /// 创建时间可在成员表用
        /// </summary>
        public System.DateTime? CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间可在成员表使用
        /// </summary>
        public System.DateTime? LastUpdateTime { get; set; }
        /// <summary>
        /// 操作通用的
        /// </summary>

        public System.String IsDel { get; set; }
        /// <summary>
        /// 性别名称改为类型
        /// </summary>
        public System.String SexName { get; set; }

        
        /// <summary>
        /// 角色名称改为成员名称
        /// </summary>
        public System.String RoleName { get; set; }
        /// <summary>
        /// 用户名字改为日期
        /// </summary>
       // public System.DateTime shuriqi { get; set; }
    }
}

