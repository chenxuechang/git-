using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Designer
{
    /// <summary>
    /// 类型下拉管理
    /// </summary>
    public partial class A_leixingxialacaidan
    {
        public A_leixingxialacaidan()
        {
            Conn = "Default";
        }

        public A_leixingxialacaidan(string conn)
        {
            Conn = conn;
        }//功能不用改

        /// <summary>
        /// 数据库连接字符串KEY
        /// </summary>
        public string Conn { get; set; }

        //public System.Guid leixing { get; set; }
        
        //public System.String leixingming  { get; set; }
        
        public System.String leixing { get; set; }
        ///// <summary>
        ///// 排序改为成员等级
        ///// </summary>
        //public System.Decimal? Sort { get; set; }
        /// <summary>
        /// 是否删除（0：未删 1：已删）
        /// </summary>
        public System.String IsDel { get; set; }

    }
}
