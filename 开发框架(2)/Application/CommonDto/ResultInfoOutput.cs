using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.CommonDto
{
    /// <summary>
    /// 通用输出，只有结果信息1
    /// </summary>
    public class ResultInfoOutput
    {
        /// <summary>
        /// 结果信息
        /// </summary>
        public Controllers.Dto.ResultInfo resultInfo { get; set; }

    }
}
