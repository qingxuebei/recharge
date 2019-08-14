using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class OrdersApiController : ApiController
    {
        // GET: api/OrdersApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OrdersApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OrdersApi
        public MyData.ClientResult.Result Post([FromBody]string pulseCode, string OperatorName)
        {
            MyData.ClientResult.Result result = new MyData.ClientResult.Result();
            try
            {
                pulseCode = MyData.Utils.ReplaceSQLChar(pulseCode);
                BLL.PulsaProductBLL pulsaProductBLL = new BLL.PulsaProductBLL();
                Model.PulsaProduct pulsaProduct = pulsaProductBLL.getFirstById(pulseCode);

                BLL.OrdersBLL orderBLL = new BLL.OrdersBLL();
                Model.Orders orders = new Model.Orders();
                orders.orderId = MyData.Utils.GetRamCode();
                orders.pulsa_code = pulsaProduct.pulsa_code;
                orders.cn_pulsatype = pulsaProduct.pulsa_type == "data" ? "流量" : "话费";
                orders.masaaktif = pulsaProduct.masaaktif;
                orders.cn_quatity = pulsaProduct.cn_quatity;
                orders.cn_op = pulsaProduct.cn_op;
                orders.cn_price = pulsaProduct.cn_price;
                orders.cn_oldprice = pulsaProduct.cn_oldprice;
                orders.OperatorName = MyData.Utils.ReplaceSQLChar(OperatorName);
                orderBLL.Insert(orders);

                result.Code = MyData.ClientCode.Succeed;
                result.Message = MyData.Suggestion.InsertSucceed;
            }
            catch (Exception ex)
            {
                result.Code = MyData.ClientCode.Fail;
                result.Message = MyData.Suggestion.InsertException;
            }
            return result;

        }

        // PUT: api/OrdersApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OrdersApi/5
        public void Delete(int id)
        {
        }
    }
}
