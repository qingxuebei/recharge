using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_cs_Click(object sender, EventArgs e)
        {
            Session["Username"] = "test";
            Response.Redirect("index.aspx", false);
            //if (new BLL.SysUserBLL().getCount(username.Value.Trim(), password.Value.Trim()) > 0)
            //{
            //    Session["Username"] = username.Value.Trim();
            //    Response.Redirect("index.aspx", false);
            //}
            //else
            //{
            //    Response.Write("<script language='javascript'>alert('Account or password is wrong，please try again！')</script>");
            //    return;
            //}
        }
    }
}