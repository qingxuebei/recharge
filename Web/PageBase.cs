using System;
using System.Web;

namespace Web
{
    public class PageBase:System.Web.UI.Page
    {
        int _UID; string _U_NAME; int _ROLE; int _GROUP;

        public int UID { get { return _UID; } }
        public string U_NAME { get { return _U_NAME; } }
        public int ROLE { get { return _ROLE; } }
        public int GROUP { get { return _GROUP; } }

        public PageBase()
        {
            LoginCheck();
        }
        public void LoginCheck()
        {
            HttpCookie Cookies = HttpContext.Current.Request.Cookies["LOGIN"];
            bool isLogin = false;
            if (Cookies != null)
            {
                try
                {
                    this._UID = Convert.ToInt32(System.Web.HttpUtility.UrlDecode(Cookies["_UID"], System.Text.Encoding.UTF8));
                    this._U_NAME = System.Web.HttpUtility.UrlDecode(Cookies["_U_NAME"], System.Text.Encoding.UTF8);
                    this._ROLE = Convert.ToInt32(System.Web.HttpUtility.UrlDecode(Cookies["_ROLE"], System.Text.Encoding.UTF8));
                    this._GROUP = Convert.ToInt32(System.Web.HttpUtility.UrlDecode(Cookies["_GROUP"], System.Text.Encoding.UTF8));
                    isLogin = true;
                }
                catch (Exception ex)
                {
                    isLogin = false;
                }
            }
            if (!isLogin)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/login.aspx");
            }

            //this._UID = 1;
            //this._U_NAME = "张三";
            //this._ROLE = 1;
            //this._GROUP = 1;
        }
    }
}