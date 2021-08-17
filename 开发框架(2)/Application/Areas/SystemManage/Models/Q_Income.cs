using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Designer
{
    /// <summary>
    ///收入
    /// </summary>
    public class Q_Income : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        ///支出Id
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        ///来源
        /// </summary>
        public string Source { get; set; }
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
        public char IsDel { get; set; }
    }
}
