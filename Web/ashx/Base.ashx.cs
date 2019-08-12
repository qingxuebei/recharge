using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Web.ashx
{
    /// <summary>
    /// Base 的摘要说明
    /// </summary>
    public class Base : IHttpHandler, IRequiresSessionState
    {
        public String userId = "", userName = "";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (context.Session["Username"] != null && context.Session["Username"].ToString() != "")
                {
                    userName = context.Session["Username"].ToString();
                }
                else { return; }

                switch (context.Request.Params["type"].ToString())
                {
                    case "get":
                        context.Response.Write(get(context));
                        break;
                    case "add":
                        context.Response.Write(add(context));
                        break;
                    case "update":
                        context.Response.Write(update(context));
                        break;
                    case "delete":
                        context.Response.Write(del(context));
                        break;
                    case "export":
                        context.Response.Write(export(context));
                        break;
                    case "loginout":
                        context.Response.Write(loginout(context));
                        break;
                    case "editpassword":
                        context.Response.Write(editpassword(context));
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("系统错误！");
            }
        }

        public virtual String get(HttpContext context)
        {
            return null;
        }
        public virtual String add(HttpContext context)
        {
            return null;
        }
        public virtual String update(HttpContext context)
        {
            return null;
        }
        public virtual String del(HttpContext context)
        {
            return null;
        }
        public virtual String export(HttpContext context)
        {
            return null;
        }

        public String loginout(HttpContext context)
        {
            context.Session["Username"] = null;
            return "1";
        }
        public String editpassword(HttpContext context)
        {
            String oldpwd = context.Request.Params["oldpwd"].ToString();
            String newpwd = context.Request.Params["newpwd"].ToString();
            String username = context.Session["Username"].ToString();
            if (new BLL.SysUserBLL().getCount(username, oldpwd) > 0)
            {
                if (new BLL.SysUserBLL().editPwd(username, newpwd))
                {
                    return "1";
                }
                else { return "修改失败！"; }
            }
            return "原密码错误！";
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}