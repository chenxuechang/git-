using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 初始化密码输入参数//这个有定位作用
    /// </summary>
    public class InitPassWordInput
    {/// <summary>
    /// 通过用户Id来定位//有用
    /// </summary>
        public Guid userId { get; set; }

        /// <summary>
        /// 时间定位
        /// </summary>
        public string  shuriqi { get; set; }
        /// <summary>
        /// 开始定位
        /// </summary>
        public Guid kaishi { get; set; }
        /// <summary>
        /// 结束定位
        /// </summary>
        public Guid jieshu { get; set; }

    }
}
