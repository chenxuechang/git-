using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.CommonDto
{
    /// <summary>
    /// 带返回记录总数的输出参数
    /// </summary>
    public class ResultCountOutput
    {
        /// <summary>
        /// 结果信息
        /// </summary>
        public Controllers.Dto.ResultInfo resultInfo { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int recordCount { get; set; }
    }
}
