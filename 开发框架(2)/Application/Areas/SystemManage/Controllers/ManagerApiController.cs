using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cathy;
using System.Data.SqlClient;
using Newtonsoft.Json.Converters;
using NPOI.SS.Formula.Functions;
using Dal;

namespace Application.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 管理员//应该是管理员管理，通用的
    /// </summary>
    [Area("SystemManage")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Auth(memberType:"1")]
    public class ManagerApiController : ControllerBase
    {
        /// <summary>
        /// 人员个数//有时间了把他去了
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Dto.UserCountOutput UserCount(Dto.UserCountInput userCountInput)//数量结果//用户查询输入
        {
            string strWhere = " IsDel='0'";
            
            List<SqlParameter> sqlParas = new List<SqlParameter>();//new进来SqlParameter
            //if (userCountInput.userName + "" != "")//判断用户查询输入的用户名字的输入的名字不相等
            //{
            //    strWhere += " and UserName like @UserName";
            //    sqlParas.Add(new SqlParameter("@UserName", "%" + userCountInput.userName + "%"));
            //}//这一步没有意义


            if (userCountInput.shuriqi + "" != "")//判断用户查询输入的用户名字的输入的名字不相等
            {
                strWhere += " and riqi like @riqi";
                sqlParas.Add(new SqlParameter("@riqi", "%" + userCountInput.shuriqi + "%"));
            }
            Dal.V_User vu = new Dal.V_User();//new进来用户实体表
            int recordNum = vu.Count(strWhere, sqlParas);//把查询出来的记录总数赋值给recordNum

            Dto.UserCountOutput uco = new Dto.UserCountOutput();//new出数量结果过类
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//new出结果信息
            ri.IsSuccess = 1;//结果信息传输失败获成功//成功
            uco.userCount = recordNum;//赋值数量结果过的用户数量
            uco.resultInfo = ri;//赋值数量结果过
            return uco;//将数量结果返回
        }


        //[HttpPost]
        //public Dto.UserCountOutput UserCount1(Dto.UserCountInput userCountInput)//数量结果//用户查询输入
        //{
        //}



        /// <summary>
        /// 通过日期范围查
        /// </summary>
        /// <param name="userCountInput"></param>
        /// <returns></returns>
        [HttpPost]
        public Dto.UserListOutput UserList(/*DateTime ks, DateTime js,*/ Dto.UserCountInput userCountInput)//输出//输入
        {
            string strWhere = " IsDel='0'";
            List<SqlParameter> sqlParas = new List<SqlParameter>();

            //if (!string.IsNullOrEmpty(strWhere))//strwhere为null时用的，但好像没啥用
            //for (int i = 0; i <0; i++)//瞎搞的
            


                if (userCountInput.shuriqi!=null&&userCountInput.shuriqi.Length == 2 )//日期的个数长度//也就是说有俩
                {
                    strWhere += " and riqi >='" + userCountInput.shuriqi[0] + "' and riqi <='" + userCountInput.shuriqi[1] + "'";
                    sqlParas.Add(new SqlParameter("@riqi", "%" + userCountInput.shuriqi + "%"));
                }
            //    else
            //{
            //T s = null;

            //    //strWhere += " and riqi >='" + userCountInput.shuriqi[0] + "' and riqi <='" + userCountInput.shuriqi[1] + "'";
            //    //sqlParas.Add(new SqlParameter("@riqi", "%" + userCountInput.shuriqi + "%"));
            //}
            //DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "1"; //第一天
            //DateTime.Parse(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "1").AddMonths(1).AddDays(-1).ToShortDateString();


           
        
           
            //}
            //new出用户实体，给用户实体赋方法（先排序在分页）
            Dal.V_User tdc = new Dal.V_User();
    
            List<Dal.V_User> listUser = tdc.Query(strWhere, "CreateTime desc", sqlParas, (userCountInput.pageIndex - 1) * userCountInput.pageSize, userCountInput.pageSize);
            
            //new出来用户输出表和用户输出表的用户信息
            Dto.UserListOutput listOutput = new Dto.UserListOutput();//new进来用户输出表
            List<Dto.UserInfo> listUserInfo = new List<Dto.UserInfo>();//new进来用户信息

            //把new来的信息
            foreach (Dal.V_User singleUser in listUser)//输出局部变量singleUser和listUser//foreach循环
            {
                Dto.UserInfo userListOutput = new Dto.UserInfo();
                userListOutput.riqi = singleUser.riqi.ToString("yyyy-MM-dd");
                userListOutput.SetPropertieValues(singleUser);
                //listUserInfo.Sort();
                listUserInfo.Add(userListOutput);//最后添加
         

            }
            //最后输出的地方
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//把调出的数据new给结果信息
            ri.IsSuccess = 1;//传输成功
            listOutput.resultInfo = ri;//结果信息就有值
            listOutput.listUserInfo = listUserInfo;//把调出的用户数据给listOutput
            return listOutput;//输出listOutput
        }
        ///// <summary>
        ///// 管理员信息//yinhghaishizhe 
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public Dto.UserListOutput UserList(Dto.UserCountInput userCountInput)
        //{
        //    string strWhere = " IsDel='0'";
        //    List<SqlParameter> sqlParas = new List<SqlParameter>();
        //    if (userCountInput.userName + "" != "")
        //    {
        //        strWhere += " and UserName like @UserName";
        //        sqlParas.Add(new SqlParameter("@UserName", "%" + userCountInput.userName + "%"));//不太懂应该跟用户名字有关，输入用户名字
        //    }
        //
        //    Dal.V_User tdc = new Dal.V_User();//把用户表new过来，省的封装注入
        //    //连接V_User表           //赋值                                                        //输出-1页码的所有条数                     //输出每页的条数

        //    List<Dal.V_User> listUser = tdc.Query(strWhere, "CreateTime desc", sqlParas, (userCountInput.pageIndex-1)*userCountInput.pageSize, userCountInput.pageSize);
            
        //    Dto.UserListOutput listOutput = new Dto.UserListOutput();//new进来用户输出表
        //    List<Dto.UserInfo> listUserInfo = new List<Dto.UserInfo>();//new进来用户信息
        //    foreach (Dal.V_User singleUser in listUser)//输出局部变量singleUser和listUser//foreach循环
        //    {
        //        Dto.UserInfo userListOutput = new Dto.UserInfo();
        //        userListOutput.riqi = singleUser.riqi.ToString("yyyy-MM-dd");


        //        userListOutput.SetPropertieValues(singleUser);

        //        listUserInfo.Add(userListOutput);//最后添加
        //    }
        //    Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//把调出的数据new给结果信息
        //    ri.IsSuccess = 1;//传输成功
        //    listOutput.resultInfo = ri;//结果信息就有值
        //    listOutput.listUserInfo = listUserInfo;//把调出的用户数据给listOutput
        //    return listOutput;//输出listOutput
        //}

        






        /// <summary>
        ///角色下拉菜单//不用改//后期给类型搞一个
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Dto.DrpListRoleOutput DrpListRole()
        {
            Dal.T_RoleManage trm = new Dal.T_RoleManage();
            List<Dal.T_RoleManage> listRole = trm.FillOrder("*", "IsDel='0'", "SORT");

            Dto.DrpListRoleOutput drpListRoleOutput = new Dto.DrpListRoleOutput();
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();
            ri.IsSuccess = 1;
            drpListRoleOutput.resultInfo = ri;
            drpListRoleOutput.listRoleManage = listRole;

            return drpListRoleOutput;
        }


        ///// <summary>
        /////类型下拉菜单
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public  Dto.leixingxialacanshu leixingxiala()
        //{
        //    Dal.Designer.A_leixingxialacaidan trm = new Dal.Designer.A_leixingxialacaidan();
        //    List<Dal.Designer.A_leixingxialacaidan> ww = trm.FillOrder("*", "UserId=''", "UserId");

        //    Dto.leixingxialacanshu drpListRoleOutput = new Dto.leixingxialacanshu();
        //    Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();
        //    ri.IsSuccess = 1;
        //    drpListRoleOutput.resultInfo = ri;
        //    drpListRoleOutput.leixingxialacaidan = ww;

        //    return drpListRoleOutput;
        //}


        /// <summary>
        /// 用户帐号验证//已改为可以重复
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput UserAccountValidate(Dto.UserAccountValidateInput userAccountValidateInput)
        {
            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            Dal.T_User t_User = new Dal.T_User();
            Application.Controllers.Dto.ResultInfo resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfo.IsSuccess = 1;
            //if (userAccountValidateInput.userId != "00000000-0000-0000-0000-000000000000")
            //{
            //    //编辑 
            //    if (t_User.Select("*", "UserAccount={@p0} and IsDel='0' and UserId!={@p1}", userAccountValidateInput.userAccount, userAccountValidateInput.userId))
            //    {
            //        resultInfo.IsSuccess = 0;
            //        resultInfo.ErrorInfo = "用户帐号已存在";
            //    }
            //}
            //else
            //{
            //    //新增
            //    if (t_User.Select("*", "UserAccount={@p0} and IsDel='0'", userAccountValidateInput.userAccount))
            //    {
            //        resultInfo.IsSuccess = 0;
            //        resultInfo.ErrorInfo = "用户帐号已存在";
            //    }
            //}
            resultInfoOutput.resultInfo = resultInfo;
            return resultInfoOutput;
        }


        ///// <summary>
        ///// 通过日期范围掉数据
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public Dto.UserSingleInfoOutput diaoshujv (Dal.T_User t_User, DateTime ks, DateTime js, Dto.InitPassWordInput initPassWordInput)
        //{
        //    string riqi = initPassWordInput.riqi + "";//通过日期定位
        //    if (riqi == null)
        //    {
        //        DateTime now = DateTime.Now;//当前时间
        //        DateTime d1 = now.AddDays(1 - now.Day);//本月月初
        //    }
        //    //string kaishi = initPassWordInput.kaishi + "";
        //    //string jieshu = initPassWordInput.jieshu + "";
        //    var q = new Dal.T_User();//new进平台用户表格
        //    var list = q.Fill("*", "riqi={@p0}", initPassWordInput.riqi);//通过日期定位
        //    var w = from e in list
        //            select new
        //            {
        //                UserId = e.UserId,
        //                riqi = e.riqi,//开始日期
        //                riqi1 = e.riqi.ToString(),//结束日期

        //            };//引出全部数据
        //    DateTime Kaishi = Convert.ToDateTime(ks);//给开始日期赋值
        //    DateTime Jieshu = Convert.ToDateTime(js);//给结束日期赋值
        //    w = w.Where(m => m.riqi >= ks && m.riqi <= js);//赋值日期范围
        //    //return JsonOptions(w, JsonResultBehavior.AllowGet);            
        //    //return JsonResult(w, JsonResultBehavior.AllowGet);

        //    Dto.UserSaveInput userSaveInput = new Dto.UserSaveInput();//new出来用户保存输入表
        //    Dto.UserSingleInfoOutput userSingleInfoOutput = new Dto.UserSingleInfoOutput();//new出用户信息输出
        //    userSingleInfoOutput.userSaveInput = userSaveInput;//把用户保存信息传给用户信息输出

        //    Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//new出结果信息
        //    ri.IsSuccess = 1;//是否传出//成功
        //    userSingleInfoOutput.resultInfo = ri;//传到结果信息
        //    return userSingleInfoOutput;//返回用户信息输出
        //}


        /// <summary>
        /// 通过日期范围查
        /// </summary>
        /// <param name="initPassWordInput"></param>
        /// <returns></returns>
        [HttpPost]
        public Dto.UserListOutput chafanwei( DateTime ks, DateTime js, Dto.UserCountInput userCountInput)//输出//输入
        {
            string strWhere = " IsDel='0'";
            List<SqlParameter> sqlParas = new List<SqlParameter>();
            if (userCountInput.shuriqi + "" != "")
            {
                strWhere += " and riqi like @riqi";
                sqlParas.Add(new SqlParameter("@riqi", "%" + userCountInput.shuriqi + "%"));
            }       
            //Dal.V_User tdc = new Dal.V_User();//把用户表new过来，省的封装注入
            //if (tdc != null)
            //{
            //    var now = DateTime.Now;//当前时间
            //    DateTime d1 = now.AddDays(1 - now.Day);//本月月初
            //}
            var q = new Dal.T_User();//new进平台用户表格
            var initPassWordInput = new Dto.InitPassWordInput();
            string surriqi = initPassWordInput.shuriqi + "";//通过日期定位
            var list = q.Fill("*", "riqi={@p0}", surriqi);//通过日期定位
            //var qqq = userCountInput;
            var w = from e in list
                    select new
                    {
                        UserId = e.UserId,
                        riqi = e.riqi,
                       // riqi1 = e.riqi,//开始日期
                        riqi2 = e.riqi.ToString(),//结束日期

                    };//引出全部数据
            DateTime Kaishi = Convert.ToDateTime(ks);//给开始日期赋值
            DateTime Jieshu = Convert.ToDateTime(js);//给结束日期赋值
            w = w.Where(m => m.riqi >= ks && m.riqi <= js);//赋值日期范围
            var wq = userCountInput.shuriqi;
            // w = wq;
            Dto.UserCountInput chachu = (Dto.UserCountInput)w;
            //var qwe = w;

            //new出用户实体，给用户实体赋方法
            Dal.V_User tdc = new Dal.V_User();
            List<Dal.V_User> listUser = tdc.Query(strWhere, "CreateTime desc", sqlParas, (userCountInput.pageIndex - 1) * userCountInput.pageSize, userCountInput.pageSize);
           
            Dto.UserListOutput listOutput = new Dto.UserListOutput();//new进来用户输出表
            List<Dto.UserInfo> listUserInfo = new List<Dto.UserInfo>();//new进来用户信息
            foreach (Dal.V_User singleUser in listUser)//输出局部变量singleUser和listUser//foreach循环
            {
                Dto.UserInfo userListOutput = new Dto.UserInfo();
                userListOutput.riqi = singleUser.riqi.ToString("yyyy-MM-dd");
                userListOutput.SetPropertieValues(singleUser);
                listUserInfo.Add(userListOutput);//最后添加
            }
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//把调出的数据new给结果信息
            ri.IsSuccess = 1;//传输成功
                listOutput.resultInfo = ri;//结果信息就有值
                listOutput.listUserInfo = listUserInfo;//把调出的用户数据给listOutput
                return listOutput;//输出listOutput
        }





        //        string surriqi = initPassWordInput.dingriqi + "";//通过日期定位
        //    if (tdc != null)
        //    {
        //        var now = DateTime.Now;//当前时间
        //        DateTime d1 = now.AddDays(1 - now.Day);//本月月初
        //    }
        //    var q = new Dal.T_User();//new进平台用户表格
        //    var list = q.Fill("*", "riqi={@p0}",surriqi);//通过日期定位
        //    var w = from e in list
        //            select new
        //            {
        //                UserId = e.UserId,
        //                riqi =e.riqi,
        //                riqi1 = e.riqi,//开始日期
        //                riqi2 = e.riqi.ToString(),//结束日期

        //            };//引出全部数据
        //    DateTime Kaishi = Convert.ToDateTime(ks);//给开始日期赋值
        //    DateTime Jieshu = Convert.ToDateTime(js);//给结束日期赋值
        //    w = w.Where(m => m.riqi >= ks && m.riqi <= js);//赋值日期范围
        //    //return JsonOptions(w, JsonResultBehavior.AllowGet);            
        //    //return JsonResult(w, JsonResultBehavior.AllowGet);

        //    //Dto.UserListOutput userListOutput = new Dto.UserListOutput();








        //    Dto.UserSaveInput userSaveInput = new Dto.UserSaveInput();//new出来用户保存输入表
        //    userSaveInput.SetPropertieValues(q);//应该是为对象平台用户表的变量tuser提供某种功能
        //    Dto.UserSingleInfoOutput userSingleInfoOutput = new Dto.UserSingleInfoOutput();//new出用户信息输出
        //    userSingleInfoOutput.riqi = userSaveInput;//把用户保存信息传给用户信息输出
        //    //userSingleInfoOutput.riqiNe = userSaveInput;//把用户保存信息传给用户信息输出
        //    Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//new出结果信息
        //    ri.IsSuccess = 1;//是否传出//成功
        //    userSingleInfoOutput.resultInfo = ri;//传到结果信息
        //    userSingleInfoOutput.riqiNe = userSaveInput.riqi.ToString("yyyy-MM-dd HH:mm:ss");
        //    userSingleInfoOutput.riqi = new Dto.UserSaveInput();
        //    userSingleInfoOutput.riqi.riqi = userSaveInput.riqi;
        //   return userSingleInfoOutput;//返回用户信息输出

        

        //}

        /// <summary>
        /// 用户保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput UserSave(Dto.UserSaveInput userSaveInput)
        {
            string strId = userSaveInput.UserId + "";//赋值用户保存输入的用户Id和输入值
            List<Guid> listRole = userSaveInput.UserRoles;//赋值用户保存输入的用户角色
            Dal.T_User user = new Dal.T_User();//给平台用户表的局部变量new出来
            user.Select("*", "UserId={@p0}", strId);//调出平台用户表这个格式的所有的数据 ，以用户Id为主键       //*应该代表范围      //UserId={@p0}应该是正则的语句格式限制     //
            user.SetPropertieValues(userSaveInput);//设置输入保存的 属性
            //按理说上面的就够用了

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();//new出输出结果
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//new出结果信息
            ri.IsSuccess = 1;//是否传出结果信息//成功
            user.LastUpdateTime = System.DateTime.Now;//给平台表的最后添加时间创造功能
            user.riqi = System.DateTime.Today;
            user.IsDel = "0";//是否删除//未删除
            if (strId == "00000000-0000-0000-0000-000000000000")
            {
                //用户添加
                user.CreateTime = System.DateTime.Now;//创建时间
                user.riqi = userSaveInput.riqi;
                if (userSaveInput.riqi == null)
                {
                    userSaveInput.riqi = System.DateTime.Today;
                }
                user.UserPassword = Cathy.ConfigHelper.GetSection("InitPass").Value;//添加密码功能，没用
                user.UserId = Guid.NewGuid();//初始化用户表的用户Id，用于给新添加的数据递增Id
                user.Insert();//添加实体//Insert添加
            }
            else//如果想添加的数据已存在就修改数据
            {
                user.riqi = userSaveInput.riqi;
                
                //用户编辑 
                user.Update("UserId={@p0}", user.UserId);//Update更新
            }

            //用户角色
            Dal.T_UserRole turDel = new Dal.T_UserRole();//new出平台用户角色表
            turDel.Delete("UserId={@p0}", user.UserId);//给用户角色表删除用户Id的功能
            foreach (Guid strRoleId in listRole)//循环用户角色
            {
                Dal.T_UserRole tur = new Dal.T_UserRole();//new出用户角色表
                tur.UserId = user.UserId;//赋值用户表中的用户Id
                tur.RoleId = strRoleId;//赋值角色表Id
                tur.Insert();//添加
            }
            resultInfoOutput.resultInfo = ri;//传给结果信息//成功
            return resultInfoOutput;//返回值，结果信息
        }

        /// <summary>
        /// 用户单条信息//应该跟查询有关
        /// </summary>
        /// <returns></returns>
        [HttpPost]//不用管
        public Dto.UserSingleInfoOutput UserSingleInfo(Dto.InitPassWordInput initPassWordInput)
        {
            string strUserId = initPassWordInput.userId+"";//赋值通过用户Id定位   //很重要
            //用户信息
            Dal.T_User tuser = new Dal.T_User();//new出用户平台表
            tuser.Select("*", "UserId={@p0}", strUserId);//返回符合这个格式的这个类型的所有数据，定位的

            //角色信息
            Dal.T_UserRole tur = new Dal.T_UserRole();//new出角色表
            List<Dal.T_UserRole> listUserRole = tur.Fill("*", "UserId={@p0}", strUserId);//连接角色表，把调出的数据赋值给他
            List<Guid> listRoleIds = new List<Guid>();// new一个局部变量           //List初始化  //Guid全局唯一标识符  //listRoleIds变量标识符
            foreach (Dal.T_UserRole singleUserRole in listUserRole)//listUserRole里面的局部变量singleUserRole//循环
            {
                listRoleIds.Add(singleUserRole.RoleId);//添加角色Id//应该是自动添加角色Id，自动递增
            }

            Dto.UserSaveInput userSaveInput = new Dto.UserSaveInput();//new出来用户输入表
            userSaveInput.SetPropertieValues(tuser);//应该是为对象平台用户表的变量tuser提供某种功能
            userSaveInput.UserRoles = listRoleIds;//把角色Id赋值给用户角色

            Dto.UserSingleInfoOutput userSingleInfoOutput = new Dto.UserSingleInfoOutput();//new出用户角色输出
            userSingleInfoOutput.userSaveInput = userSaveInput;//把用户保存信息传给用户信息输出
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//new出结果信息
            ri.IsSuccess = 1;//是否传出//成功
            userSingleInfoOutput.resultInfo = ri;//传到结果信息
            userSingleInfoOutput.riqiNe = userSaveInput.riqi.ToString("yyyy-MM-dd");
            userSingleInfoOutput.riqi = new Dto.UserSaveInput();
            userSingleInfoOutput.riqi.riqi= userSaveInput.riqi; 
            //userSingleInfoOutput.riqi = userSaveInput.riqi.ToString("yyyy-MM-dd hh:mm:ss");
            return userSingleInfoOutput;//返回用户信息输出
        }






    




        /// <summary>
        /// 用户删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput UserDelete(Dto.InitPassWordInput initPassWordInput)
        {
            string strUserId = initPassWordInput.userId + "";//建立定位信息的能力        //定位信息
            Dal.T_User tuer = new Dal.T_User();//new进来平台信息表
            tuer.Select("*", "UserId={@p0}", strUserId);//返回所有符合格式要求的数据，定位的
            tuer.LastUpdateTime = System.DateTime.Now;//最后时间更新功能，删除的
            tuer.IsDel = "1";//是否删除//删除
            tuer.Update("UserId={@p0}", strUserId);//更新，影响行数方法，通过复合的定位

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();//new出通用输出结果信息
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();//new出结果信息
            ri.IsSuccess = 1;//结果信息里的成功或失败//成功
            resultInfoOutput.resultInfo = ri;//将结果信息输出
            return resultInfoOutput;//反回结果信息
        }

        /// <summary>
        /// 密码初始化//这个没用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput InitPassWord(Dto.InitPassWordInput initPassWordInput)//通用输出和密码初始化
        {
            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();//new出通用输出
            Application.Controllers.Dto.ResultInfo resultInfo = new Application.Controllers.Dto.ResultInfo();//new出结果信息
            resultInfo.IsSuccess = 1;//结果信息，成功或失败//成功
            string strUserId = initPassWordInput.userId + "";//赋值定位//通过输入的用户Id来定位
            Dal.T_User tuser = new Dal.T_User();//new出平台成员表
            tuser.Select("*", "UserId={@p0}", strUserId);//返回符合定位的数据
            tuser.UserPassword = Cathy.ConfigHelper.GetSection("InitPass").Value;//应该是给密码附上加密功能
            tuser.Update("UserId={@p0}", strUserId);//通过用户Id定位更新所有符合的数据

            resultInfoOutput.resultInfo = resultInfo;//传给结果信息
            return resultInfoOutput;//将数据返回给通用输出
        }




    }
}
