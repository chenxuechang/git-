using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Models
{ /// <summary>
  /// 账单输入保存
  /// </summary>
    public class shurubaocun
    {
            /// <summary>
            /// 账单Id
            /// </summary>
            public System.Guid Id { get; set; }
            /// <summary>
            /// 类型
            /// </summary>
            public char Typelass { get; set; }
            /// <summary>
            /// 成员
            /// </summary>
            public int MemberId { get; set; }
            /// <summary>
            /// 使用，用途
            /// </summary>
            public int UseType { get; set; }
            /// <summary>
            /// 金额
            /// </summary>
            public decimal Money { get; set; }
            /// <summary>
            /// 时间
            /// </summary>
            public DateTime DateTime;

            /// <summary>
            /// 备忘录
            /// </summary>
            public int Memo { get; set; }
            /// <summary>
            /// 是否删除
            /// </summary>
            public bool IsDel { get; set; }//布尔类型
        
    }
}
