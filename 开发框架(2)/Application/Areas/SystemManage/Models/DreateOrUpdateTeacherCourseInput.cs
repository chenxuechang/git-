using System;
using System.Collections.Generic;

namespace Application.Areas.SystemManage.Models
{
    /// <summary>
    /// 输出数据之地
    /// </summary>    
    /// <returns></returns>
    public class DreateOrUpdateTeacherCourseInput
    {
        /// <summary>
        /// 结果信息
        /// </summary>
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }

        /// <summary>
        /// 帐单列表信息
        /// </summary>
        public List<Output> listUserInfo { get; set; }


    }
        /// <summary>
        /// 账单名字
        /// </summary>
        public class Output
        {

        
        /// <summary>
        /// 账单Id
        /// </summary>
        public System.Guid  Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public char Typelass { get; set; }
        /// <summary>
        /// 成员
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 使用，用途
        /// </summary>
        public int UseType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime qiro { get; set; }

        /// <summary>
        /// 备忘录
        /// </summary>
        public int Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }//布尔类型
        }
}