using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xyan.Model;
using Xyan.IBLL;
using Xyan.BLL;
using Xyan.Common;

namespace Weixin.Controllers
{
    public class LoginController : Controller
    {
        #region Fields
        private IBaseUserService _userInfoService;

        #endregion

        #region Constructors
        public LoginController(IBaseUserService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        #endregion

        /// <summary>
        /// 实现用户的登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 判断用户输入的信息是否正确，[HttpPost]
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="userInfo">用户的实体类</param>
        /// <param name="Code">验证码</param>
        /// <returns>返回是否执行成功的标志</returns>
        public ActionResult CheckUserInfo(BaseUser userInfo)
        {
            //首先我们拿到系统的验证码
            string sessionCode = this.TempData["ValidateCode"] == null
                                     ? new Guid().ToString()
                                     : this.TempData["ValidateCode"].ToString();
            ////然后我们就将验证码去掉，避免了暴力破解
            //this.TempData["ValidateCode"] = new Guid();
            ////判断用户输入的验证码是否正确
            //if (sessionCode != Code)
            //{
            //    return Content("验证码输入不正确");
            //}

            //调用业务逻辑层（BLL）去校验用户是否正确,,,定义变量存取获取到的用户的错误信息
            string UserInfoError = "";
            var loginUserInfo = _userInfoService.CheckUserInfo(userInfo);
            switch (loginUserInfo)
            {
                case LoginResult.PwdError:
                    UserInfoError = "密码输入错误";
                    break;
                case LoginResult.UserNotExist:
                    UserInfoError = "用户名输入错误或者您已经被禁用";
                    break;
                case LoginResult.UserIsNull:
                    UserInfoError = "用户名不能为空";
                    break;
                case LoginResult.PwdIsNUll:
                    UserInfoError = "密码不能为空";
                    break;
                case LoginResult.OK:
                    UserInfoError = "OK";
                    //首先根据用户名的信息获取到用户详细的信息
                    Session["UserInfo"] = _userInfoService.LoadEntities(c => c.UserName == userInfo.UserName).FirstOrDefault();
                    break;
                default:
                    UserInfoError = "未知错误，请您检查您的数据库";
                    break;
            }

            return Content(UserInfoError);
        }


        /// <summary>
        /// 验证码的实现
        /// </summary>
        /// <returns>返回验证码</returns>
        public ActionResult CheckCode()
        {
            //首先实例化验证码的类
            var validateCode = new KenceryValidateCode();
            //生成验证码指定的长度
            string code = validateCode.CreateValidateCode(5);
            this.TempData["ValidateCode"] = code;
            //创建验证码的图片
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            //最后将验证码返回
            return File(bytes, @"image/jpeg");
        }

    }
}
