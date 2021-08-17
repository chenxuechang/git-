using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Application.Areas.SystemManage.Controllers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Models
{
    /// <summary>
    /// 账单表管理
    /// </summary>
    public class Q_IIncomePaymentDetail : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 账单名字
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 类型(收入1支出0）
        /// </summary>
        public Typeclass Typeclass { get; set; }
        /// <summary>
        /// 成员
        /// </summary>
        public string memberId { get; set; }
        ///<summary>
        /// 使用，用途
        /// </summary>
        public string Usetype { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 日期.时间
        /// </summary>
        public DateTime Datetime { get; set; }
        /// <summary>
        /// 备忘录
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public char IsDel { get; set; }//生成枚举

        //public class Dot
        //{
        //}
    }
    /// <summary>
    /// 类型枚举
    /// </summary>
    //public enum Typeclass
    //{
    //    /// <summary>
    //    /// 支出
    //    /// </summary>
    //    Expenditure = 1,
    //    /// <summary>
    //    /// 收入
    //    /// </summary>
    //    Income = 0
    //}
}
