using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public class B_TreeAddChild
    {
        /// <summary>
        /// 添加孩子节点
        /// </summary>
        /// <param name="ctPid">父节点</param>
        /// <param name="listAllMenu">所有可用的菜单功能信息</param>
        public static List<Dal.C_Tree> AddChildrenNode(Dal.C_Tree ctPid, List<Dal.T_Menu> listAllMenu)
        {
            List<Dal.C_Tree> listChildrenNode = new List<Dal.C_Tree>();
            var varChildLevelMenu = listAllMenu.Where(t => t.MenuPid+"" == ctPid.data.id).OrderBy(t => t.MenuSort);
            foreach (var singleMenu in varChildLevelMenu)
            {
                Dal.C_Tree ct = new Dal.C_Tree();
                ct.name = singleMenu.MenuName;
                ct.checkboxValue = singleMenu.MenuId+"";
                ct.spread = true;
                Dal.TreeData treedata = new Dal.TreeData();
                treedata.id = singleMenu.MenuId+"";
                treedata.type = singleMenu.MenuType;
                ct.data = treedata;
                ct.children = AddChildrenNode(ct, listAllMenu);
                listChildrenNode.Add(ct);
            }
            return listChildrenNode;
        }


        /// <summary>
        /// 添加功能菜单孩子节点
        /// </summary>
        /// <param name="ctPid">父节点</param>
        /// <param name="listAllMenu">所有可用的菜单功能信息</param>
        public static List<Areas.SystemManage.Controllers.Dto.TreeNode> MenuAddChildrenNode(Areas.SystemManage.Controllers.Dto.TreeNode ctPid, List<Dal.T_Menu> listAllMenu)
        {
            List<Areas.SystemManage.Controllers.Dto.TreeNode> listChildrenNode = new List<Areas.SystemManage.Controllers.Dto.TreeNode>();
            var varChildLevelMenu = listAllMenu.Where(t => t.MenuPid+"" == ctPid.id).OrderBy(t => t.MenuSort);
            foreach (var singleMenu in varChildLevelMenu)
            {
                Areas.SystemManage.Controllers.Dto.TreeNode treeNode = new Areas.SystemManage.Controllers.Dto.TreeNode();
                treeNode.id = singleMenu.MenuId + "";
                treeNode.label = singleMenu.MenuName;
                treeNode.children = new List<Areas.SystemManage.Controllers.Dto.TreeNode>();
                List<Application.Areas.SystemManage.Controllers.Dto.TreeNode> listTreeNode = new List<Areas.SystemManage.Controllers.Dto.TreeNode>();
                listTreeNode = B_TreeAddChild.MenuAddChildrenNode(treeNode, listAllMenu);
                if (listTreeNode.Count > 0)
                {
                    treeNode.children = listTreeNode;
                }
                ctPid.children.Add(treeNode);
            }
            return listChildrenNode;
        }
    }
}
