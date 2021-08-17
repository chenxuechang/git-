using Abp.Application.Services;
using Application.Areas.SystemManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Data
{
    /// <summary>
    /// 账单接口定义之地
    /// </summary>
    public interface BIncomePaymentDetail: IApplicationService
    {
        /// <summary>
        /// 添加或修改账单数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task tianjiaxiugai( CreateOrUpdateTeacherCourseInput input);
        /// <summary>
        /// 根据日期查询账单数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <summary>
        Task<CreateOrUpdateTeacherCourseInput> huoqvtianjiaxiugaixinxi(GetTeacherCourseInput input);
        /// <summary>
        /// 删除账单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <summary>
        Task shanchu  (string Id);
        ///// <summary>
        ///// 根据收入或支出查询账单信息
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        ///// <summary>
        
    }
}
