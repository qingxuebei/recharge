using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Orders
    {
        public string orderId { get; set; }
        public string pulsa_code { get; set; }
        public string masaaktif { get; set; }
        public string cn_pulsatype { get; set; }
        public string cn_quatity { get; set; }
        public string cn_op { get; set; }
        public decimal cn_price { get; set; }
        public decimal cn_oldprice { get; set; }
        public string OperatorName { get; set; }
    }
}
