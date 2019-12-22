using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// Orders 的摘要说明
    /// </summary>
    public class Orders : Base
    {
        public override String get(HttpContext context)
        {
            int pageRows = 20, page = 1; String order = "";

            BLL.OrdersBLL orderBLL = new BLL.OrdersBLL();
            String strWhere = " 1=1";
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and OrderId='" + st[0].ToString() + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[1]))
            {
                strWhere += " and OpenId='" + st[1].ToString() + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[2]))
            {
                strWhere += " and OperatorId=" + Convert.ToInt32(st[2]);
            }
            if (!String.IsNullOrWhiteSpace(st[3]))
            {
                strWhere += " and CnPulsatype='" + st[3].ToString() + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[4]))
            {
                strWhere += " and WechatpayState=" + Convert.ToInt32(st[4]);
            }
            if (!String.IsNullOrWhiteSpace(st[5]))
            {
                strWhere += " and PulsaState=" + Convert.ToInt32(st[5]);
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
            DataTable dt = orderBLL.GetListByPage(strWhere.ToString(), order, (page - 1) * pageRows + 1, page * pageRows);
            int count = orderBLL.GetRecordCount(strWhere.ToString());//获取条数  
            return MyData.Utils.EasyuiDataGridJson(dt, count);
        }
    }
}