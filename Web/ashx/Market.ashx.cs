using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// Market 的摘要说明
    /// </summary>
    public class Market : Base
    {
        private static readonly ILog logs = LogManager.GetLogger(typeof(Market));
        public override String get(HttpContext context)
        {
            int pageRows, page; String order = "";
            pageRows = 20;
            page = 1;
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

        public override String add(HttpContext context)
        {
            BLL.MarketBLL productsBLL = new BLL.MarketBLL();
            Model.Market market = new Model.Market();
            market.ID = System.Guid.NewGuid().ToString();
            market.IconUrl = context.Request.Params["IconUrl"].ToString();
            market.Name = context.Request.Params["Name"].ToString();
            market.SmsUrl = context.Request.Params["SmsUrl"].ToString();

            market.SortId = Convert.ToInt32(context.Request.Params["SortId"].ToString());
            market.ApprovlTime = context.Request.Params["ApprovlTime"].ToString();
            market.Disbursement = context.Request.Params["Disbursement"].ToString();

            market.MaxMoney = Convert.ToInt32(context.Request.Params["MaxMoney"].ToString());
            market.Tenure = context.Request.Params["Tenure"].ToString();
            market.Rate = context.Request.Params["Rate"].ToString();

            market.CreatePerson = userName;
            market.UpdatePerson = userName;
            market.State = Convert.ToInt32(context.Request.Params["State"].ToString());
            if (productsBLL.Insert(market))
            {
                return "0";
            }
            else
            {
                return "保存失败";
            }
        }
        public override String update(HttpContext context)
        {
            BLL.MarketBLL productsBLL = new BLL.MarketBLL();
            Model.Market market = new Model.Market();
            market.ID = context.Request.Params["ID"].ToString();
            market.IconUrl = context.Request.Params["IconUrl"].ToString();
            market.Name = context.Request.Params["Name"].ToString();
            market.SmsUrl = context.Request.Params["SmsUrl"].ToString();

            market.SortId = Convert.ToInt32(context.Request.Params["SortId"].ToString());
            market.ApprovlTime = context.Request.Params["ApprovlTime"].ToString();
            market.Disbursement = context.Request.Params["Disbursement"].ToString();

            market.MaxMoney = Convert.ToInt32(context.Request.Params["MaxMoney"].ToString());
            market.Tenure = context.Request.Params["Tenure"].ToString();
            market.Rate = context.Request.Params["Rate"].ToString();
            
            market.UpdatePerson = userName;
            market.State = Convert.ToInt32(context.Request.Params["State"].ToString());
            if (productsBLL.Update(market))
            {
                return "0";
            }
            else
            {
                return "保存失败";
            }
        }
    }
}