using System;

namespace Dal
{ 
     /// <summary>
     /// 菜单管理//不用改
     /// </summary>
     public partial class T_Menu
     { 
         public T_Menu()
         {
             Conn = "Default";
         }

         public T_Menu(string conn)
          {
              Conn =conn;
          }

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}

         /// <summary>
         /// 菜单ID
         /// </summary>
         public System.Guid MenuId{get;set;}

         /// <summary>
         /// 菜单名称
         /// </summary>
         public System.String MenuName{get;set;}

         /// <summary>
         /// 菜单编码
         /// </summary>
         public System.String MenuCode{get;set;}

         /// <summary>
         /// URL
         /// </summary>
         public System.String MenuUrl{get;set;}

         /// <summary>
         /// 目标
         /// </summary>
         public System.String MenuTarget{get;set;}

         /// <summary>
         /// 排序
         /// </summary>
         public System.Decimal? MenuSort{get;set;}

         /// <summary>
         /// 父ID
         /// </summary>
         public System.Guid MenuPid{get;set;}

         /// <summary>
         /// 分类（目录1、功能2）
         /// </summary>
         public System.String MenuType{get;set;}

         /// <summary>
         /// 目录图标类
         /// </summary>
         public System.String IconClass{get;set;}

         /// <summary>
         /// 最后更新时间
         /// </summary>
         public System.DateTime? LastUpdateTime{get;set;}

         /// <summary>
         /// 是否已删除（0：未删除 1：已删除）
         /// </summary>
         public System.String IsDel{get;set;}

     }
} 

