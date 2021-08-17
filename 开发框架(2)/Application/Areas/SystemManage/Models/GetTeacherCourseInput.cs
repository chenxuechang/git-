using Application.Areas.SystemManage.Models;
using System;

namespace Application.Areas.SystemManage.Models
{
    /// <summary>
    ///时间获取
    /// </summary>
    public class GetTeacherCourseInput:Q_IncomePaymentDetail//.Dot
    {
        /// <summary>
        /// 通过时间获取
        /// </summary>
        public Guid? DateTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public Guid? kaishiDateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public Guid? jieshuDateTime { get; set; }
        /// <summary>
        /// 第X页页码（从1开始）
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; }
    }
}