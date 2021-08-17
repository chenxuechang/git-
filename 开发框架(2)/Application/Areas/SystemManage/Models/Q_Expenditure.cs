using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Designer
{
    /// <summary>
    ///支出
    /// </summary>
    public class Q_Expenditure : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        ///支出
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        ///用途
        /// </summary>
        public string Usetype { get; set; }
        /// <summary>
        ///时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        ///备忘录
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public char IsDel { get; set; }//应该没用

    }
}
