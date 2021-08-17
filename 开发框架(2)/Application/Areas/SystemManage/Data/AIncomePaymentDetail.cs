using Abp.Authorization;
using Abp.Domain.Repositories;
using Application.Areas.SystemManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Data
{
    /// <summary>
    /// 账单接口实现之地
    /// </summary>
    //[AbpAuthorize()]//仅供有权限的人修改，我感觉不用，暂时用不上
    public class AIncomePaymentDetail //: BIncomePaymentDetail
    {
        //private readonly IRepository<Application.EntityDesign.Expenditure, Guid> _Expenditure;
        /// <summary>
        /// 删除账单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <summary>
        /// 
        public Task shanchu(string Id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 添加或修改账单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task tianjiaxiugai(CreateOrUpdateTeacherCourseInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据日期查询账单数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <summary>
        public Task<CreateOrUpdateTeacherCourseInput> huoqvtianjiaxiugaixinxi(GetTeacherCourseInput input)
        {
            var riqi = new GetTeacherCourseInput();
            throw new NotImplementedException();
        }


    }

}
