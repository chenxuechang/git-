using System;

namespace Dal
{ 
     /// <summary>
     /// 部门//没用
     /// </summary>
     public partial class T_Dept
     { 
         public T_Dept()
         {
             Conn = "Default";
         }

         public T_Dept(string conn)
          {
              Conn =conn;
          }

         /// <summary>
         /// 数据库连接字符串KEY
         /// </summary>
         public string Conn{get;set;}

         /// <summary>
         /// ID
         /// </summary>
         public System.Guid DeptId{get;set;}

         /// <summary>
         /// 部门名称
         /// </summary>
         public System.String DeptName{get;set;}

         /// <summary>
         /// 部门编码
         /// </summary>
         public System.String DeptCode{get;set;}

         /// <summary>
         /// 排序
         /// </summary>
         public System.Decimal? DeptSort{get;set;}

         /// <summary>
         /// 上级部门(一级部门父ID为0)
         /// </summary>
         public System.Guid DeptPid{get;set;}

         /// <summary>
         /// 最后更新时间
         /// </summary>
         public System.DateTime? LastUpdateTime{get;set;}

         /// <summary>
         /// 是否已删除（0：未删  1：已删）
         /// </summary>
         public System.String IsDel{get;set;}

     }
} 

