using Application.Areas.SystemManage.Models;
using Dal.Designer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Areas.SystemManage.Data
{
    //public Data.



}
//    /// <summary>
//    /// 控制器
//    /// </summary>
//    public ActionResult kaishi(IncomePaymentDetail _IncomePaymentDetail, Member _Member, Income _Income, Expenditure _Expenditure)
//    {
//        var IIncomePaymentDetail = new IncomePaymentDetail();
//        var IMember = new Member();
//        var IIncome = new Income();
//        var IExpenditure = new Expenditure();
//        var IGetTeacherCourseInput = new GetTeacherCourseInput();
//        try
//        {
//            var linst = (
//                from q in IIncomePaymentDetail.GetAll()
//                join w in IMember.GetType() on q.memberId equals w.TenantId//成员的
//                join e in IExpenditure.GetType() on q.Typeclass.Expenditure equals e.TenantId
//                join r in IIncome.GetType() on q.Typeclass.Income equals r.TenantId
//                join t in IGetTeacherCourseInput.GetType() on q.Datetime equals t.Datetime
//                select new IncomePaymentDetail
//                {
//                    memberId = IIncomePaymentDetail.memberId,
//                    Typeclass = IIncomePaymentDetail.Typeclass,
//                    Memo = IIncomePaymentDetail.Memo,
//                    Usetype = IIncomePaymentDetail.Usetype,
//                    Money = IIncomePaymentDetail.Money,
//                    Datetime = IIncomePaymentDetail.Datetime,

//                });
//            if (kaishiDateTime != null && jieshuDateTime != null)
//            {
//                linst = linst.Where(s => s.Datetime >= kaishiDateTime && s.Datetime <= jieshuDateTime);
//            }

//            List<luggageDeposit> tbpage = list.Skip(LayuiTablePage.GetStartIndex()).Take(LayuiTablePage.limit).ToList();
//            LayuiTableData<luggageDeposit> lugg = new LayuiTableData<luggageDeposit>
//            {
//                count = list.Count(),
//                data = tbpage
//            };
//            return Json(lugg, JsonRequestBehavior.AllowGet);

//        }
//}
