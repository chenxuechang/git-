using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cathy;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace Application.Controllers
{
    /// <summary>
    /// 后台主框架
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Auth(memberType:"1")]
    public class MainApiController : ControllerBase
    {
        /// <summary>
        /// 修改密码不用管
        /// </summary>
        [HttpPost]
        public Dto.EditPassWordOutput EditPassword(Dto.EditPassWordInput epwinput)
        {
            Dto.EditPassWordOutput epwOutput = new Dto.EditPassWordOutput();
            Dto.ResultInfo ri = new Dto.ResultInfo();
            string userId = epwinput.userId;
            string oldpas = epwinput.oldPass;
            string newpas = epwinput.newPass;
            Dal.T_User oldUser = new Dal.T_User();
            oldUser.Select("*", "UserId={@p0} and IsDel='0'", userId);
            if (oldUser != null && oldUser.UserPassword != oldpas)
            {
                ri.IsSuccess = 0;
                ri.ErrorInfo = "原密码输入错误";
            }
            else
            {
                oldUser.UserPassword = newpas;

                oldUser.Update("UserId={@p0} and IsDel='0'", userId);
                ri.IsSuccess = 1;
                ri.ErrorInfo = "";                
            }
            epwOutput.ResultInfo = ri;
            return epwOutput;
        }

        /// <summary>
        /// 主页面加载菜单
        /// </summary>
        [HttpPost]
        public Dto.MainPageLoadMenuOutput MainPageLoadMenu(Dto.MainPageLoadMenuInput mainPageLoadMenuInput)
        {
            string strResult = "";
            string strUserId = mainPageLoadMenuInput.userId;

            string strSql = @"select *
from T_Menu m
where exists(select *
from V_UserRoleMenu vurm
where vurm.UserId='"+strUserId+"' and m.MenuId=vurm.MenuId)";
            SqlConnection conn = SqlHelper.CreateConn();
            DataTable dt = SqlHelper.DataSet(strSql, conn);

            List<Dal.T_Menu> listMenu = DbOperExtend.DataTableToList<Dal.T_Menu>(dt);

            //#####为替换符
            strResult = "<ul>#####</ul>";

            string strUlAll = "";
            #region 一级目录
            var firstMenus = listMenu.Where(t => t.MenuPid+"" == "00000000-0000-0000-0000-000000000000").OrderBy(t => t.MenuSort);
            int i = 0;
            foreach (var singleMenu in firstMenus)
            {
                string strMenu = "<li>#####</li>";

                string strFirstTitle = @"<div def-list='" + i + @"' onclick='showHideFuncMenuSingle(this)' class='divTitle'>
                        <span class='" + singleMenu.IconClass + @"' aria-hidden='true'></span>&nbsp;&nbsp;" + singleMenu.MenuName + @"<div class='leftMenuRightIcon'><span class='glyphicon  glyphicon-menu-down' def-righticon='" + i + @"'></span></div>
                    </div>";
                string strUlFunc = " <ul def-menu='" + i + "' style='display: block;'>#####</ul>";
                var funcMenus = listMenu.Where(t => t.MenuPid == singleMenu.MenuId).OrderBy(t => t.MenuSort);
                string strLiFuncs = "";
                foreach (var singleFunc in funcMenus)
                {
                    strLiFuncs += "<li><div class='sub1' def-sub='" + i + @"' def-target='" + singleFunc.MenuTarget + @"' def-url='" + singleFunc.MenuUrl + "' onclick='openUrl(this);'>" + singleFunc.MenuName + "</div></li>";
                }
                strUlFunc = strUlFunc.Replace("#####", strLiFuncs);

                string strMenuInfo = strFirstTitle + strUlFunc;
                strMenu = strMenu.Replace("#####", strMenuInfo);

                strUlAll += strMenu;
                i++;
            }
            strResult = strResult.Replace("#####", strUlAll);
            #endregion
            Dto.MainPageLoadMenuOutput mainPageLoadMenuOutput = new Dto.MainPageLoadMenuOutput();
            mainPageLoadMenuOutput.resultInfo = new Dto.ResultInfo();
            mainPageLoadMenuOutput.resultInfo.IsSuccess = 1;
            mainPageLoadMenuOutput.menuInfo = strResult;
            return mainPageLoadMenuOutput;
        }
    }
}
