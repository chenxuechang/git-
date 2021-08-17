using System;

namespace Application.Areas.SystemManage.Models
{
    /// <summary>
    /// 输入数据之地
    /// </summary>    
    /// <returns></returns>
    public class CreateOrUpdateTeacherCourseInput
    {
        ///// <summary>
        ///// 账单名字
        ///// </summary>
        //public int id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public char typelass { get; set; }
        /// <summary>
        /// 成员
        /// </summary>
        public int memberId { get; set; }
        /// <summary>
        /// 使用，用途
        /// </summary>
        public int useType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal money { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime;

        /// <summary>
        /// 备忘录
        /// </summary>
        public int memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool isDel { get; set; }//布尔类型
    }
}