using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cathy;
using Newtonsoft.Json;

namespace Application.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统管理-功能管理
    /// </summary>
    [Area("SystemManage")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Auth(memberType: "1")]
    public class FuncManageApiController : ControllerBase
    {
        ///<summary>
        /// 单个菜单信息//暂时用不上，以后看
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Dto.MenuSaveOutput MenuSingleInfo(Dto.MenuSingleInfoInput menuSingleInfoInput)
        {
            string strId = menuSingleInfoInput.menuId;
            Dal.T_Menu tm = new Dal.T_Menu();
            tm.Select("*", "MenuId={@p0} and IsDel='0'", strId);

            Dto.MenuSaveOutput menuSaveOutput = new Dto.MenuSaveOutput();
            menuSaveOutput.menuInfo = tm;
            menuSaveOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            menuSaveOutput.resultInfo.IsSuccess = 1;
            return menuSaveOutput;
        }

        /// <summary>
        /// 加载功能菜单
        /// </summary>
        [HttpPost]
        public Dto.MenuTreeOutput ListTree()
        {
            Dal.T_Menu tm = new Dal.T_Menu();
            List<Dal.T_Menu> listAllMenu = tm.Fill("*", "IsDel='0'");

            List<Dto.TreeNode> listTree = new List<Dto.TreeNode>();

            Dto.TreeNode tnRoot = new Dto.TreeNode();
            tnRoot.id = "00000000-0000-0000-0000-000000000000";
            tnRoot.label = "功能管理";
            tnRoot.children = new List<Dto.TreeNode>();
            listTree.Add(tnRoot);

            #region 取第一级目录
            var varFirstLevelMenu = listAllMenu.Where(t => t.MenuPid + "" == "00000000-0000-0000-0000-000000000000").OrderBy(t => t.MenuSort);
            foreach (var singleMenu in varFirstLevelMenu)
            {
                Dto.TreeNode treeNode = new Dto.TreeNode();
                treeNode.id = singleMenu.MenuId + "";
                treeNode.label = singleMenu.MenuName;
                treeNode.children = new List<Dto.TreeNode>();
                List<Dto.TreeNode> listChildNode = new List<Dto.TreeNode>();
                listChildNode = B_TreeAddChild.MenuAddChildrenNode(treeNode, listAllMenu);
                if (listChildNode.Count > 0)
                {
                    treeNode.children = listChildNode;
                }
                tnRoot.children.Add(treeNode);
            }
            #endregion

            Dto.MenuTreeOutput menuTreeOutput = new Dto.MenuTreeOutput();
            Application.Controllers.Dto.ResultInfo ri = new Application.Controllers.Dto.ResultInfo();
            ri.IsSuccess = 1;
            menuTreeOutput.resultInfo = ri;
            menuTreeOutput.listTree = listTree;
            return menuTreeOutput;
        }


        /// <summary>
        /// 同一父级下的菜单名称是否已存在
        /// </summary>
        /// <param name="menuNameExistInput"></param>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput MenuNameExist(Dto.MenuNameExistInput menuNameExistInput)
        {
            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfoOutput.resultInfo.IsSuccess = 1;
            Dal.T_Menu t_Menu = new Dal.T_Menu();
            bool haveExists = false;
            if (menuNameExistInput.menuId == "00000000-0000-0000-0000-000000000000")
            {
                if (t_Menu.Select("*", "MenuName={@p0} and IsDel='0' and MenuPid={@p1}", menuNameExistInput.menuName, menuNameExistInput.parentId))
                {
                    haveExists = true;
                }
            }
            else
            {
                if (t_Menu.Select("*", "MenuName={@p0} and IsDel='0' and MenuPid={@p1} and MenuId!={@p2}", menuNameExistInput.menuName, menuNameExistInput.parentId, menuNameExistInput.menuId))
                {
                    haveExists = true;
                }
            }
            if (haveExists == true)
            {
                resultInfoOutput.resultInfo.IsSuccess = 0;
                resultInfoOutput.resultInfo.ErrorInfo = "该菜单名称已存在";
            }
            return resultInfoOutput;
        }

        /// <summary>
        /// 菜单编码是否已存在
        /// </summary>
        /// <param name="menuCodeExistsInput"></param>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput MenuCodeExist(Dto.MenuCodeExistsInput menuCodeExistsInput)
        {
            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfoOutput.resultInfo.IsSuccess = 1;
            Dal.T_Menu t_Menu = new Dal.T_Menu();
            bool haveExists = false;
            if (menuCodeExistsInput.menuId == "00000000-0000-0000-0000-000000000000")
            {
                if (t_Menu.Select("*", "MenuCode={@p0} and IsDel='0'", menuCodeExistsInput.menuCode))
                {
                    haveExists = true;
                }
            }
            else
            {
                if (t_Menu.Select("*", "MenuCode={@p0} and IsDel='0' and MenuId!={@p1}", menuCodeExistsInput.menuCode, menuCodeExistsInput.menuId))
                {
                    haveExists = true;
                }
            }
            if (haveExists == true)
            {
                resultInfoOutput.resultInfo.IsSuccess = 0;
                resultInfoOutput.resultInfo.ErrorInfo = "该菜单编码已存在";
            }
            return resultInfoOutput;
        }


        /// <summary>
        /// 根据父ID查菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Dto.ListMenuInfoOutput ListMenuInfo(Dto.ListMenuInfoInput listMenuInfoInput)
        {
            Dto.ListMenuInfoOutput listMenuInfoOutput = new Dto.ListMenuInfoOutput();
            string strPid = listMenuInfoInput.parentId;
            Dal.T_Menu tm = new Dal.T_Menu();
            List<Dal.T_Menu> listMenu = tm.FillOrder("*", "MenuPid={@p0} and IsDel='0'", "MenuSort", strPid);

            Application.Controllers.Dto.ResultInfo resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfo.IsSuccess = 1;
            listMenuInfoOutput.resultInfo = resultInfo;
            listMenuInfoOutput.listMenu = listMenu;

            return listMenuInfoOutput;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput MenuDelete(Dto.MenuDeleteInput menuDeleteInput)
        {
            string strId = menuDeleteInput.menuId;
        
                Dal.T_Menu tm = new Dal.T_Menu();
                tm.Select("*", "MenuId={@p0} and IsDel='0'", strId);
                tm.LastUpdateTime = System.DateTime.Now;
                tm.IsDel = "1";
                tm.Update("MenuId ={@p0} and IsDel = '0'", strId);

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfoOutput.resultInfo.IsSuccess = 1;
            return resultInfoOutput;
           
        }

        /// <summary>
        /// 功能保存
        /// </summary>
        [HttpPost]
        public CommonDto.ResultInfoOutput MenuSave(Dto.MenuSaveInput menuSaveInput)
        {
            Dal.T_Menu tm = menuSaveInput.menuInfo;
            tm.LastUpdateTime = System.DateTime.Now;
            tm.IsDel = "0";

            if (tm.MenuId + "" == "00000000-0000-0000-0000-000000000000")
            {
                //添加
                tm.MenuId = Guid.NewGuid();
                tm.Insert();
            }
            else
            {
                //编辑 
                tm.Update("MenuId={@p0} and  IsDel='0'", menuSaveInput.menuInfo.MenuId);
            }

            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Application.Controllers.Dto.ResultInfo();
            resultInfoOutput.resultInfo.IsSuccess = 1;
            return resultInfoOutput;
        }

    }
}
