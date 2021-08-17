using System;

namespace Dal
{ 
     /// <summary>
     /// 角色管理改为成员管理
     /// </summary>
     public partial class T_RoleManage
     { 
         public T_RoleManage()
         {
             Conn = "Default";
         }

         public T_RoleManage(string conn)
          {
              Conn =conn;
          }//功能不用改

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}

         /// <summary>
         /// 角色ID改为成员Id
         /// </summary>
         public System.Guid RoleId{get;set;}

         /// <summary>
         /// 名称改为成员名称
         /// </summary>
         public System.String RoleName{get;set;}

         /// <summary>
         /// 编码改为角色地位
         /// </summary>
         public System.String RoleCode{get;set;}

         /// <summary>
         /// 排序改为成员等级
         /// </summary>
         public System.Decimal? Sort{get;set;}

         /// <summary>
         /// 最后更新时间
         /// </summary>
         public System.DateTime? LastUpdateTime{get;set;}

         /// <summary>
         /// 是否删除（0：未删 1：已删）
         /// </summary>
         public System.String IsDel{get;set;}

     }
} 

