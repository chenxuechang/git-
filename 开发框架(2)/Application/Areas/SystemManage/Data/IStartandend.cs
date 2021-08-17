using Application.Areas.SystemManage.Models;
using Cathy;
using Cathy.MySql;
using Cathy.Oracle;
using Dal;
using Dal.Designer;
using Microsoft.AspNetCore.Mvc;
using Slack.Webhooks.Blocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace Application.Areas.SystemManage.Data
{
    ///<summary>
    ///账单管理
    /// </summary>
    [Area("SystemManage")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Auth(memberType: "1")]
    public class IStartandend : ControllerBase
    {
        //public  Models.DreateOrUpdateTeacherCourseInput kaishi(Q_IncomePaymentDetail IncomePaymentDetail, Q_Member Member, Q_Income Income, Q_Expenditure IExpenditure, CreateOrUpdateTeacherCourseInput ICreateOrUpdateTeacherCourseInput, GetTeacherCourseInput IGetTeacherCourseInput, DreateOrUpdateTeacherCourseInput IDreateOrUpdateTeacherCourseInput)
        //{
        //    string strWhere = " IsDel='0'";
        //    List<SqlParameter> sqlParas = new List<SqlParameter>();
        //    if (IGetTeacherCourseInput.DateTime + "" != "")
        //    {
        //        strWhere += " and UserName like @UserName";
        //        sqlParas.Add(new SqlParameter("@UserName", "%" + IGetTeacherCourseInput.Datetime + "%"));
        //    }
        //    Models.Q_IncomePaymentDetail tdc = new Models.Q_IncomePaymentDetail();
        //    //连接V_User表           //赋值                                                        //输出-1页码的所有条数                     //输出每页的条数
        //    List<Models.Q_IncomePaymentDetail> listIncomePaymentDetail = tdc.Query(strWhere, "CreateTime desc", sqlParas, (IGetTeacherCourseInput.pageIndex - 1) * IGetTeacherCourseInput.pageSize, IGetTeacherCourseInput.pageSize);
        //    /*Models.IncomePaymentDetail td = new Models.IncomePaymentDetail();*///把用户表new过来，省的封装注入

        //    Models.DreateOrUpdateTeacherCourseInput listOutput = new Models.DreateOrUpdateTeacherCourseInput();//new进来用户输出表
        //    List<Models.Output> listUserInfo = new List<Models.Output>();//new进来用户信息
        //    foreach (Models.Q_IncomePaymentDetail singleUser in listIncomePaymentDetail)//输出局部变量singleUser和listUser//foreach循环
        //    {
        //        Models.Output userListOutput = new Models.Output();
        //        userListOutput.SetPropertieValues(singleUser);
        //        listUserInfo.Add(userListOutput);//最后添加
        //    }
        //    Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//把调出的数据new给结果信息
        //    ri.IsSuccess = 1;//传输成功
        //    listOutput.resultInfo = ri;//结果信息就有值
        //    listOutput.listUserInfo = listUserInfo;//把调出的用户数据给listOutput
        //    return listOutput;//输出listOutput
        //    //没明白为什莫
        //}


        ///// <summary>
        ///// 账单保存
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public CommonDto.ResultCountOutput 





    }
    
}

//        static void Main(string[] args)
//        {
//            var IIncomePaymentDetail = new IncomePaymentDetail();
//            var IMember = new Member();
//            var IIncome = new Income();
//            var IExpenditure = new Expenditure();
//            var linst = (
//                    from q in IIncomePaymentDetail
//                    join w in IMember.GetType() on q.memberId equals w.TenantId//成
//                    select li
//        }



//        public ActionResult kaishi(IncomePaymentDetail _IncomePaymentDetail, Member _Member, Income _Income, Expenditure _Expenditure)
//        {


//            var IIncomePaymentDetail = new IncomePaymentDetail();
//            var IMember = new Member();
//            var IIncome = new Income();
//            var IExpenditure = new Expenditure();


//            {
//                var linst = (
//                    from q in IIncomePaymentDetail.GetType()
//                    join w in IMember.GetType() on q.memberId equals w.TenantId//成员的
//                    join e in IExpenditure.GetType() on q.Typeclass.Expenditure equals e.TenantId
//                    join r in IIncome.GetType() on q.Typeclass.Income equals r.TenantId
//                    select new IncomePaymentDetail
//                    {
//                        memberId = IIncomePaymentDetail.memberId,
//                        Typeclass = IIncomePaymentDetail.Typeclass,
//                        Memo = IIncomePaymentDetail.Memo,
//                        Usetype = IIncomePaymentDetail.Usetype,
//                        Money = IIncomePaymentDetail.Money,
//                        Datetime = IIncomePaymentDetail.Datetime,

//                    });
//                if (startDateTime != null && endDateTime != null)
//                {
//                    list = list.Where(s => s.storageDate >= startDateTime && s.storageDate <= endDateTime);
//                }

//                List<luggageDeposit> tbpage = list.Skip(LayuiTablePage.GetStartIndex()).Take(LayuiTablePage.limit).ToList();
//                LayuiTableData<luggageDeposit> lugg = new LayuiTableData<luggageDeposit>
//                {
//                    count = list.Count(),
//                    data = tbpage
//                };
//                return Json(lugg, JsonRequestBehavior.AllowGet);

//            }





//        }

//        public ActionResult Select(T_User User, T_RoleManage _RoleManage)
//        {
//            DateTable dt = new DateTable();
//            //获取DateTimePicker日期
//            DateTime dts = Convert.ToDateTime(dtpStart.Text);
//            DateTime dte = Convert.ToDateTime(dtpEnd.Text);
//            //根据获得的日期参数，查找出指定日期范围的数据。其中StartTime为数据库字段名称。
//            string sql = "select * from dataRecord where datediff(day,@dts,StartTime) >=0 and datediff(day, StartTime, @dte) >= 0";
//            SqlParameter[] spa = new SqlParameter[]{
//                    new SqlParameter("@dts", dts),
//                     new SqlParameter("@dte", dte)
//        };
//            conn.Open();
//            SqlDataAdapter ad = new SqlDataAdapter(sql, conn);
//            ad.SelectCommand.Parameters.AddRange(spa);
//            ad.Fill(dt);
//            dataGridView1.DataSource = dt;

//        }
//        public ActionResult lugg(LayuiTablePage LayuiTablePage, DateTime? startDateTime, DateTime? endDateTime)
//        public ActionResult lugg(IncomePaymentDetail _IncomePaymentDetail, Member _Member, Income _Income, Expenditure _Expenditure)
//        {
//            try
//            {
//                var list = (from tbluggageDeposit in myModel.B_luggageDeposit
//                            join tbpassenger in myModel.S_passenger on
//                            tbluggageDeposit.passengerID equals tbpassenger.passengerID
//                            join tbbaggageType in myModel.B_baggageType on
//                            tbluggageDeposit.baggageTypeID equals tbbaggageType.baggageTypeID
//                            join tbpersonnel in myModel.S_personnel on
//                            tbluggageDeposit.personnelID equals tbpersonnel.personnelID
//                            join tbuser in myModel.S_User on
//                            tbpersonnel.UserID equals tbuser.UserID
//                            orderby tbluggageDeposit.luggageID
//                            select new luggageDeposit
//                            {
//                                luggageID = tbluggageDeposit.luggageID,
//                                storageNumber = tbluggageDeposit.storageNumber,
//                                storageFee = tbluggageDeposit.storageFee,
//                                baggageCount = tbluggageDeposit.baggageCount,
//                                passengerName = tbpassenger.passengerName,
//                                passengerNumber = tbpassenger.passengerNumber,
//                                baggageType = tbbaggageType.baggageType,
//                                personnelNumber = tbpersonnel.personnelNumber,
//                                storageName = tbuser.UserName,
//                                storageTime1 = tbluggageDeposit.storageTime.ToString(),
//                                storageDate = tbluggageDeposit.storageDate,
//                                Remarks = tbluggageDeposit.Remarks
//                            });

//                if (startDateTime != null && endDateTime != null)
//                {
//                    list = list.Where(s => s.storageDate >= startDateTime && s.storageDate <= endDateTime);
//                }

//                List<luggageDeposit> tbpage = list.Skip(LayuiTablePage.GetStartIndex()).Take(LayuiTablePage.limit).ToList();
//                LayuiTableData<luggageDeposit> lugg = new LayuiTableData<luggageDeposit>
//                {
//                    count = list.Count(),
//                    data = tbpage
//                };
//                return Json(lugg, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception e)
//            {
//                Console.Write(e);
//                throw;
//            }
//        }


//        public Task<V_User> kaishihejieshu(GetTeacherCourseInput input)
//        {
//            throw new NotImplementedException();
//            var yinyong = new V_User();
//            var juese = new V_UserRoleInfo();
//            var chaxun =


//            throw new NotImplementedException();
//        }

//        var User = new T_User();

//        public async Task<ActionResult> kaishi(T_User User, T_RoleManage RoleManage)
//        {

//            User = new T_User();
//            RoleManage = new T_RoleManage();
//            //var chaxun = (from q in User.GetAll().Where(x => x.UserName == User.UserName).FirstOrDefaultAsync();
//            var chaxun = (from UserName in Designer.T_User



//                )
//            // from te in User
//            //             join t in RoleManage on  te.UserId equals t.UserId
//            //             //jion t in _RoleManage
//            //             where (string.IsNullOrEmpty(input.) || t.Name.Contains(Input.))
//            //             select new
//            //              {
//                              UserId = User.UserId,
//                              //UserAccount = User.UserAccount,
//                              //UserName = User.UserName,
//                              //UserSex = User.UserSex,
//                              //UserMobilePhone = User.UserMobilePhone,
//                              //UserTitles = User.UserTitles,
//                              //RoleId = RoleManage.RoleId

//                              registerTime = User.UserName,//起始时间
//                              registerTime1 = User.UserName.ToString(),
//                          };

//        DateTime qishi = Convert.ToDateTime(StartDate);//起始时间
//        DateTime endDate = Convert.ToDateTime(UserName);//结束时间
//        chaxun = chaxun.Where(m => m.UserName >= qishi&& m.UserName <= endDate);//时间范围
//            return (chaxun.JsonRequestBehavior.AllowGet);
//        }

//}

//}
