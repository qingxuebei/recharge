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

            BLL.PulsaProductBLL pulsaProductBLL = new BLL.PulsaProductBLL();
            String strWhere = " 1=1";
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and Name='" + st[0] + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[1]) && st[1] != "-1")
            {
                strWhere += " and pulsa_type='" + st[1].ToString() + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[2]))
            {
                strWhere += " and cn_status=" + Convert.ToInt32(st[2]);
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
            DataTable dt = pulsaProductBLL.GetListByPage(strWhere.ToString(), order, (page - 1) * pageRows + 1, page * pageRows);
            int count = pulsaProductBLL.GetRecordCount(strWhere.ToString());//获取条数  
            return MyData.Utils.EasyuiDataGridJson(dt, count);
        }

        public override String update(HttpContext context)
        {
            BLL.PulsaProductBLL pulsaProductBLL = new BLL.PulsaProductBLL();
            Model.PulsaProduct pulsaProduct = new Model.PulsaProduct();

            String pulsa_code = context.Request.Params["pulsa_code"].ToString();
            if (String.IsNullOrWhiteSpace(pulsa_code))
            {
                return "ID为空";
            }
            pulsaProduct = pulsaProductBLL.getFirstById(pulsa_code);
            if (pulsa_code == null) { return "该记录不存在"; }
            pulsaProduct.cn_op = context.Request.Params["cn_op"].ToString();
            pulsaProduct.cn_price = Convert.ToDecimal(context.Request.Params["cn_price"].ToString());
            pulsaProduct.cn_quatity = context.Request.Params["cn_quatity"].ToString();
            pulsaProduct.cn_status = Convert.ToInt32(context.Request.Params["cn_status"].ToString());

            if (pulsaProductBLL.Update(pulsaProduct))
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