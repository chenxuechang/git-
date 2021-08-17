using System;

namespace Dal
{ 
    /// <summary>
    /// 角色菜单之地//改为成员菜单之地
    /// </summary>
    public class V_RoleMenu
     { 
         public V_RoleMenu()
         {
             Conn = "Default";
         }

         public V_RoleMenu(string conn)
          {
              Conn =conn;
          }

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}
        /// <summary>
        /// 角色Id//改为成员Id
        /// </summary>
        public System.Guid RoleId{get;set;}
        /// <summary>
        /// 菜单Id？没看见呀
        /// </summary>

        public System.Guid MenuId{get;set;}

        /// <summary>
        /// 菜单Pid？也没看见呀
        /// </summary>

        public System.Guid MenuPid{get;set;}

     }
} 

