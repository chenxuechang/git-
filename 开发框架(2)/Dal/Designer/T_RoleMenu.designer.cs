using System;

namespace Dal
{ 
     /// <summary>
     /// 角色菜单改为成员菜单
     /// </summary>
     public partial class T_RoleMenu
     { 
         public T_RoleMenu()
         {
             Conn = "Default";
         }

         public T_RoleMenu(string conn)
          {
              Conn =conn;
          }//功能不用改

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}

         /// <summary>
         /// 角色ID改为成员Id，其实没变化
         /// </summary>
         public System.Guid RoleId{get;set;}

         /// <summary>
         /// 菜单ID//通用的
         /// </summary>
         public System.Guid MenuId{get;set;}

     }
} 

