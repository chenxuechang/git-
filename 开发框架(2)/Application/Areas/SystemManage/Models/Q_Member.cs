using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Designer
{
    /// <summary>
    ///成员
    /// </summary>
    public class Q_Member : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        ///成员
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        ///成员名字
        /// </summary>
        public string Name { get; set; }
    }
}
