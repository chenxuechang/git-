using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 类型下拉输出参数//
    /// </summary>
    public class leixingxialacanshu
    {
        /// <summary>
        ///结果信息//不用改
        /// </summary>
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }

        /// <summary>
        /// 类型下拉菜单
        /// </summary>
        public List<Dal.Designer.A_leixingxialacaidan> leixingxialacaidan { get; set; }
    }
}
