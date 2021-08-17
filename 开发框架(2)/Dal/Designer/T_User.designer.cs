using System;

namespace Dal
{ 
     /// <summary>
     /// 平台_用户表改为账单表
     /// </summary>
     public partial class T_User
     { 
         public T_User()
         {
             Conn = "Default";
         }

         public T_User(string conn)
          {
              Conn =conn;
          }//功能不用改

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}

         /// <summary>
         /// 用户ID
         /// </summary>
         public System.Guid UserId{get;set;}

         /// <summary>
         /// 帐号改为用途
         /// </summary>
         public System.String UserAccount{get;set;}

         /// <summary>
         /// 密码(md5加密)没用
         /// </summary>
         public System.String UserPassword{get;set;}

         /// <summary>
         /// 姓名改为日期
         /// </summary>
         public System.String UserName{get;set;}

        /// <summary>
        /// 类型（1收入2支出）
        /// </summary>
        public System.String UserSex{get;set;}

         /// <summary>
         /// 职称改为备注
         /// </summary>
         public System.String UserTitles{get;set;}

         /// <summary>
         /// 所属部门没用
         /// </summary>
         public System.Guid UserDept{get;set;}

         /// <summary>
         /// 办公电话没用
         /// </summary>
         public System.String UserTelPhone{get;set;}

         /// <summary>
         /// 手机改为金额
         /// </summary>
         public System.String UserMobilePhone{get;set;}

         /// <summary>
         /// E-mail没用
         /// </summary>
         public System.String UserEmail{get;set;}

        /// <summary>
        /// 用户创建时间--待加的功能
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后更新时间--待加的功能
        /// </summary>
        public System.DateTime? LastUpdateTime{get;set;}

         /// <summary>
         /// 是否已删除（0：未删  1：已删）
         /// </summary>
         public System.String IsDel{get;set;}


        /// <summary>
        /// 时间
        /// </summary>
        public System.DateTime riqi { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
         //public System.DateTime shuriqi { get; set; }
        ///// <summary>
        ///// 开始 
        ///// </summary>
        //public System.DateTime kaishi { get; set; }
        ///// <summary>
        ///// 结束
        ///// </summary>
        //public System.DateTime jieshu { get; set; }

    }
} 

