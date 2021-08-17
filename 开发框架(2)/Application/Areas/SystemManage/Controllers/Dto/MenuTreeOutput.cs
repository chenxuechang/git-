using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Controllers.Dto
{
    /// <summary>
    /// 菜单树输出参数//不用改
    /// </summary>
    public class MenuTreeOutput
    {
        public Application.Controllers.Dto.ResultInfo resultInfo { get; set; }
        public List<TreeNode> listTree { get; set; }
    }

    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode
    { 
        /// <summary>
        /// Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 显示值
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 孩子节点
        /// </summary>
        public List<TreeNode> children { get; set; }
    }
}
