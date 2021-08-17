using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public class C_Tree
    {
        public string name { get; set; }
        /// <summary>
        /// 到JS端需要转化为checked
        /// </summary>
        public bool Checked { get; set; }//选中检查布尔类型
        public string checkboxValue { get; set; }//复选框，公共的
        public bool spread { get; set; }//传播？
        public TreeData data { get; set; }//公共，枚举，数据？//树状型枚举结构，在js上有用
        public List<C_Tree> children { get; set; }//索引//有索引对象的强类型列表
    }

    public class TreeData
    {
        public string id { get; set; }
        public string type { get; set; }
    }
}
