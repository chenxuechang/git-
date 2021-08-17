using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 用户查询输入
    /// </summary>
    public class UserCountInput
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 输入时间
        /// </summary>
        //public DateTime riqi { get; set; }
        /// <summary>
        /// 输入日期
        /// </summary>
        public string[] shuriqi { get; set; }
        /// <summary>
        /// 数入开始
        /// </summary>
        public DateTime kaishi { get; set; }
        /// <summary>
        /// 数入结束
        /// </summary>
        public DateTime jieshu { get; set; }

        /// <summary>
        /// 第X页页码（从1开始）
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }

        //public static implicit operator List<object>(UserCountInput v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
