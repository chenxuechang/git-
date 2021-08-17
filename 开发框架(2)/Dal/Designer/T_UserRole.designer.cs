using System;

namespace Dal
{ 
     /// <summary>
     /// 平台_用户角色表改为成员表
     /// </summary>
     public partial class T_UserRole
     { 
         public T_UserRole()
         {
             Conn = "Default";
         }

         public T_UserRole(string conn)
          {
              Conn =conn;
          }

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}

         /// <summary>
         /// 用户ID//改为，不用改通用的
         /// </summary>
         public System.Guid UserId{get;set;}

         /// <summary>
         /// 角色ID//改为成员Id，其实没变化
         /// </summary>
         public System.Guid RoleId{get;set;}

     }
} 

