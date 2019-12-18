using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// PulsaProduct 的摘要说明
    /// </summary>
    public class PulsaProduct : Base
    {
        public override String get(HttpContext context)
        {
            int pageRows = 20, page = 1; String order = "";

            BLL.MarketBLL marketBLL = new BLL.MarketBLL();
            String strWhere = " 1=1";
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and Name='" + st[0] + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[1]) && st[1] != "-1")
            {
                strWhere += " and State=" + Convert.ToInt32(st[1]);
            }
            if (null != context.Request["rows"])
            {
                pageRows = int.Parse(context.Request["rows"].ToString().Trim());
            }
            if (null != context.Request["page"])
            {
                page = int.Parse(context.Request["page"].ToString().Trim());
            }
            if (null != context.Request["sort"])
            {
                order = context.Request["sort"].ToString().Trim();
            }

            //调用分页的GetList方法  
            DataTable dt = marketBLL.GetListByPage(strWhere.ToString(), order, (page - 1) * pageRows + 1, page * pageRows);
            int count = marketBLL.GetRecordCount(strWhere.ToString());//获取条数  
            return MyData.Utils.EasyuiDataGridJson(dt, count);
        }
    }
}