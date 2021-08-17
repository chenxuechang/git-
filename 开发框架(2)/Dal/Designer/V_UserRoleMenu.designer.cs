using System;

namespace Dal
{ 
    /// <summary>
    /// 用户角色//改为用户成员信息，感觉没啥用
    /// </summary>
     public class V_UserRoleMenu
     { 
         public V_UserRoleMenu()
         {
             Conn = "Default";
         }

         public V_UserRoleMenu(string conn)
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
        /// 菜单Id通用的
        /// </summary>
         public System.Guid MenuId{get;set;}
        /// <summary>
        /// 菜单名字可改，暂时不用改
        /// </summary>
         public System.String MenuName{get;set;}
        /// <summary>
        /// 菜单编码可改，暂时不用改
        /// </summary>
        public System.String MenuCode{get;set;}
        /// <summary>
        /// 菜单网址，不能乱改
        /// </summary>
        public System.String MenuUrl{get;set;}
        /// <summary>
        /// 菜单目标，没感觉有啥用
        /// </summary>
         public System.String MenuTarget{get;set;}
        /// <summary>
        /// 菜单分类
        /// </summary>
         public System.Decimal? MenuSort{get;set;}
        /// <summary>
        /// 菜单PId？Pid是个啥
        /// </summary>
         public System.Guid MenuPid{get;set;}
        /// <summary>
        /// 菜单类型
        /// </summary>
         public System.String MenuType{get;set;}
        /// <summary>
        /// 偶像等级？这又是个啥
        /// </summary>
         public System.String IconClass{get;set;}
        /// <summary>
        /// 最后更新时间通用的
        /// </summary>
         public System.DateTime? LastUpdateTime{get;set;}
        /// <summary>
        /// 操作通用的
        /// </summary>
         public System.String IsDel{get;set;}

     }
} 

