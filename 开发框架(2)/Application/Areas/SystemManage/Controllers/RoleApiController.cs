using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cathy;
using System.Data.SqlClient;

namespace Application.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 角色管理//与成员管理基本一致，可借用
    /// </summary>
    [Area("SystemManage")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Auth(memberType: "1")]
    public class RoleApiController : ControllerBase
    {
        /// <summary>
        /// 角色信息 包括角色信息及角色菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Dto.RoleInfoOutput RoleInfo(Dto.RoleInfoInput roleInfoInput)
        {
            string strRoleId = roleInfoInput.roleId;
            //角色信息
            Dal.T_RoleManage trm = new Dal.T_RoleManage();
            trm.Select("*", "RoleId={@p0} and IsDel='0'", strRoleId);

            //角色所拥有的权限
            Dal.V_RoleMenu tRoleMenu = new Dal.V_RoleMenu();
            List<Guid> listMenus = tRoleMenu.Fill("MenuId", "RoleId={@p0} and MenuPid!='00000000-0000-0000-0000-000000000000'", strRoleId).Select(t => t.MenuId).ToList();

            Dto.RoleInfoOutput roleInfoOutput = new Dto.RoleInfoOutput();
            roleInfoOutput.RoleAndMenuInfo = new Dto.RoleAndMenuInfo();
            roleInfoOutput.RoleAndMenuInfo.roleManage = trm;
            roleInfoOutput.RoleAndMenuInfo.listRoleMenu = listMenus;
            roleInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            roleInfoOutput.resultInfo.IsSuccess = 1;
            return roleInfoOutput;
        }

        /// <summary>
        /// 验证角色菜单名称
        /// </summary>
        [HttpPost]
        public CommonDto.ResultInfoOutput ValidateRoleName(Dto.ValidateRoleNameInput validateRoleNameInput)
        {
            bool haveExsits = false;
            Dal.T_RoleManage roleManage = new Dal.T_RoleManage();
            if (validateRoleNameInput.roleId == "00000000-0000-0000-0000-000000000000")
            {
                haveExsits = roleManage.Select("*", "RoleName={@p0} and IsDel='0'", validateRoleNameInput.roleName);
            }
            else
            {
                haveExsits = roleManage.Select("*", "RoleName={@p0} and IsDel='0' and RoleId!={@p1}", validateRoleNameInput.roleName, validateRoleNameInput.roleId);
            }

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            if (haveExsits)
            {
                resultInfoOutput.resultInfo.IsSuccess = 0;
                resultInfoOutput.resultInfo.ErrorInfo = "角色名称已存在";
            }
            else
            {
                resultInfoOutput.resultInfo.IsSuccess = 1;
                resultInfoOutput.resultInfo.ErrorInfo = "";
            }
            return resultInfoOutput;
        }

        /// <summary>
        /// 验证角色菜单编码
        /// </summary>
        [HttpPost]
        public CommonDto.ResultInfoOutput ValidateRoleCode(Dto.ValidateRoleNameInput validateRoleNameInput)
        {
            bool haveExsits = false;
            Dal.T_RoleManage roleManage = new Dal.T_RoleManage();
            if (validateRoleNameInput.roleId == "00000000-0000-0000-0000-000000000000")
            {
                haveExsits = roleManage.Select("*", "RoleCode={@p0} and IsDel='0'", validateRoleNameInput.roleName);
            }
            else
            {
                haveExsits = roleManage.Select("*", "RoleCode={@p0} and IsDel='0' and RoleId!={@p1}", validateRoleNameInput.roleName, validateRoleNameInput.roleId);
            }

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            if (haveExsits)
            {
                resultInfoOutput.resultInfo.IsSuccess = 0;
                resultInfoOutput.resultInfo.ErrorInfo = "角色编码已存在";
            }
            else
            {
                resultInfoOutput.resultInfo.IsSuccess = 1;
                resultInfoOutput.resultInfo.ErrorInfo = "";
            }
            return resultInfoOutput;
        }


        /// <summary>
        /// 保存角色信息
        /// </summary>
        ///<returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput SaveRoleInfo(Dto.SaveRoleInfoInput saveRoleInfoInput)
        {
            List<string> listRoleMenus = saveRoleInfoInput.listRoleMenus;
            Dal.T_RoleManage roleManage = saveRoleInfoInput.roleManage;
            string strRoleId = roleManage.RoleId + "";
            roleManage.LastUpdateTime = System.DateTime.Now;
            roleManage.IsDel = "0";
            //角色信息
            if (strRoleId == "00000000-0000-0000-0000-000000000000")
            {
                //新增
                roleManage.RoleId = Guid.NewGuid();
                roleManage.Insert();
            }
            else
            {
                //编辑
                roleManage.Update("RoleId={@p0}", roleManage.RoleId);
            }

            string strErrorInfo = "";
            //角色权限
            SqlConnection conn = Cathy.SqlHelper.CreateConn();
            Cathy.SqlHelper.Open(conn);
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                Dal.T_RoleMenu trm = new Dal.T_RoleMenu();
                trm.Delete("RoleId={@p0}", conn, tran, roleManage.RoleId);
                List<string> listMenuId = new List<string>();
                foreach (string menuId in listRoleMenus)
                {
                    Dal.T_RoleMenu trmInser = new Dal.T_RoleMenu();
                    trmInser.RoleId = roleManage.RoleId;
                    trmInser.MenuId = new Guid(menuId);
                    trmInser.Insert(conn, tran);

                    listMenuId.Add(trmInser.MenuId + "");

                    Dal.T_Menu tmenu = new Dal.T_Menu();
                    tmenu.Select("*", "MenuId={@p0} and IsDel='0'", trmInser.MenuId + "");
                    if (tmenu.MenuPid + "" != "00000000-0000-0000-0000-000000000000" && !(listMenuId.Contains(tmenu.MenuPid + "")))
                    {
                        //添加父级
                        Dal.T_RoleMenu trmInserParent = new Dal.T_RoleMenu();
                        trmInserParent.RoleId = roleManage.RoleId;
                        trmInserParent.MenuId = tmenu.MenuPid;
                        trmInserParent.Insert(conn, tran);

                        listMenuId.Add(tmenu.MenuPid + "");
                    }
                }

                SqlHelper.Commit(tran);
            }
            catch (Exception ex)
            {
                SqlHelper.RollBack(tran);
                strErrorInfo = ex.Message;
            }
            finally
            {
                SqlHelper.Close(conn);
            }

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfoOutput.resultInfo.IsSuccess = 1;
            if (strErrorInfo != "")
            {
                resultInfoOutput.resultInfo.IsSuccess = 0;
                resultInfoOutput.resultInfo.ErrorInfo = strErrorInfo;
            }
            return resultInfoOutput;
        }

        /// <summary>
        /// 角色个数 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultCountOutput RoleCount(Dto.RoleSearchInput roleSearchInput)
        {
            string strRoleName = roleSearchInput.roleName + "";
            List<SqlParameter> sqlParas = new List<SqlParameter>();
            string strWhere = " IsDel='0'";
            if (strRoleName != "")
            {
                strWhere += " and RoleName like @RoleName";
                sqlParas.Add(new SqlParameter("@RoleName", "%" + strRoleName + "%"));
            }

            Dal.T_RoleManage trm = new Dal.T_RoleManage();
            int recordNum = trm.Count(strWhere, sqlParas);

            CommonDto.ResultCountOutput resultCountOutput = new CommonDto.ResultCountOutput();
            resultCountOutput.recordCount = recordNum;
            resultCountOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultCountOutput.resultInfo.IsSuccess = 1;
            return resultCountOutput;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Dto.RoleListOutput RoleList(Dto.RoleSearchInput roleSearchInput)
        {
            string strRoleName = roleSearchInput.roleName + "";
            List<SqlParameter> sqlParas = new List<SqlParameter>();
            string strWhere = " IsDel='0'";
            if (strRoleName != "")
            {
                strWhere += " and RoleName like @RoleName";
                sqlParas.Add(new SqlParameter("@RoleName", "%" + strRoleName + "%"));
            }

            Dal.T_RoleManage trm = new Dal.T_RoleManage();
            List<Dal.T_RoleManage> listRole = trm.Query(strWhere, "sort ,LastUpdateTime desc", sqlParas, (roleSearchInput.pageIndex - 1) * roleSearchInput.pageSize, roleSearchInput.pageSize);

            Dto.RoleListOutput roleListOutput = new Dto.RoleListOutput();
            roleListOutput.listRoleManage = listRole;
            roleListOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            roleListOutput.resultInfo.IsSuccess = 1;
            return roleListOutput;
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput RoleDelete(Dto.RoleInfoInput roleInfoInput)
        {
            string strId = roleInfoInput.roleId;
            Dal.T_RoleManage trm = new Dal.T_RoleManage();
            trm.Select("*", "RoleId={@p0}", strId);
            trm.IsDel = "1";
            trm.LastUpdateTime = System.DateTime.Now;
            trm.Update("RoleId={@p0}", strId);

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfoOutput.resultInfo.IsSuccess = 1;
            return resultInfoOutput;
        }
    }
}
