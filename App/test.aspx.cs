using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request["code"].ToString();
            string state = Request["state"].ToString();

            Model.Logs logs = new Model.Logs();
            BLL.LogsBLL logBLL = new BLL.LogsBLL();
            logs.LogText = "code=" + code + "; state=" + state;
            logBLL.Insert(logs);
        }
    }
}