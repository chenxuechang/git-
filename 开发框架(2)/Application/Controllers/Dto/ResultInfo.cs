using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers.Dto
{
    /// <summary>
    /// 结果信息，不用管1
    /// </summary>
    public class ResultInfo
    {
        /// <summary>
        /// 1:成功 0：失败
        /// </summary>
        public int IsSuccess { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorInfo { get; set; }
    }
}
