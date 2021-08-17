using Abp.Application.Services;
using Application.Areas.SystemManage.Models;
using Dal;
using Dal.Designer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Areas.SystemManage.Data
{
    /// <summary>
    /// 开始和结束
    /// </summary>
    public interface Startandend : IApplicationService
    {
        /// <summary>
        /// 时间范围
        /// </summary>
        /// <param name="_IncomePaymentDetail"></param>
        /// <param name="_Member"></param>
        /// <param name="_Income"></param>
        /// <param name="_Expenditure"></param>
        /// <returns></returns>
        public ActionResult kaishi(Q_IncomePaymentDetail IIncomePaymentDetail, Q_Member IMember, Q_Income IIncome, Q_Expenditure IExpenditure, CreateOrUpdateTeacherCourseInput? ICreateOrUpdateTeacherCourseInput, GetTeacherCourseInput IGetTeacherCourseInput, DreateOrUpdateTeacherCourseInput IDreateOrUpdateTeacherCourseInput);

        ///// <summary>
        ///// 根据开始和结束日期查询账单数据
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        ///// <summary>
        //Task<V_User> kaishihejieshu(GetTeacherCourseInput input); //
        ///// <summary>
        ///// 新开始结束
        ///// </summary>
        ///// <param name="_User"></param>
        ///// <param name="_UserRoleInfo"></param>
        ///// <returns></returns>
        //public ActionResult kaishi(T_User User, T_RoleManage _RoleManage);

    }
}
