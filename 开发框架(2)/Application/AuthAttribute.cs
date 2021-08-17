using Cathy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application
{
    public class AuthAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 1：管理用户    2：企业用户   3：个人用户
        /// </summary>
        private string _memberType;
        public AuthAttribute(string memberType)
        {
            this._memberType = memberType;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //是否允许匿名访问   默认不允许
            var isDefined = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                //匿名访问
                isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                   .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
            }
            if (isDefined)
            {
                //匿名访问
                return;
            }
            else
            {
                #region 授权验证
                string strAuth = context.HttpContext.Request.Headers["Authorization"] + "";

                if (strAuth != "")
                {
                    string strAuthDecrypt= ExtendMethod.DESDecrypt(strAuth, ConfigHelper.GetSection("DesKey").Value);
                    string[] strArr = strAuthDecrypt.Split("$");
                    string strUserName  = strArr[0];
                    string strUserPass = strArr[1];
                    string strMemberType = strArr[2];


                    if (strMemberType == "1")
                    {
                        //管理员验证
                        Dal.T_User user = new Dal.T_User();
                        if (user.Select("*", "UserAccount={@p0}  and IsDel='0'", strUserName))
                        {
                            if (user.UserPassword == strUserPass)
                            {
                                base.OnActionExecuting(context);
                            }
                            else
                            {
                                CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
                                resultInfoOutput.resultInfo = new Controllers.Dto.ResultInfo();
                                resultInfoOutput.resultInfo.IsSuccess = 0;
                                resultInfoOutput.resultInfo.ErrorInfo = "您没有登录授权";
                                JsonResult jsonResult = new JsonResult(resultInfoOutput);
                                context.Result = jsonResult;
                            }
                        }

                    }
                    else
                    {
                        CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
                        resultInfoOutput.resultInfo = new Controllers.Dto.ResultInfo();
                        resultInfoOutput.resultInfo.IsSuccess = 0;
                        resultInfoOutput.resultInfo.ErrorInfo = "您没有登录授权";
                        JsonResult jsonResult = new JsonResult(resultInfoOutput);
                        context.Result = jsonResult;
                        return;
                    }
                    #endregion
                    return;
                }
            }
        }
    }
}
