using System;

namespace Dal
{
    /// <summary>
    /// 用户角色信息之处提取部分信息改为成员
    /// </summary>
    public class V_UserRoleInfo
     { 
       
         public V_UserRoleInfo()
         {
             Conn = "Default";
         }

         public V_UserRoleInfo(string conn)
          {
              Conn =conn;
          }//通用的

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}
        /// <summary>
        /// 用户Id通用的
        /// </summary>
        public System.Guid UserId{get;set;}
        /// <summary>
        /// 角色Id改为成员Id
        /// </summary>
         public System.Guid RoleId{get;set;}
        /// <summary>
        /// 角色名字改为成员名字
        /// </summary>
         public System.String RoleName{get;set;}
        /// <summary>
        /// 成员编码改为成员地位//地位是独一无二的和编号一般
        /// </summary>
         public System.String RoleCode{get;set;}
        /// <summary>
        /// 排序改为成员等级
        /// </summary>
         public System.Decimal? Sort{get;set;}
        /// <summary>
        /// 最后更新时间，通用的
        /// </summary>
         public System.DateTime? LastUpdateTime{get;set;}
        /// <summary>
        /// 操作，通用的
        /// </summary>
         public System.String IsDel{get;set;}

     }
} 

