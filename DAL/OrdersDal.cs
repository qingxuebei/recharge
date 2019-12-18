using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrdersDal
    {
        public bool Insert(Model.Orders orders)
        {
            String sql = @"INSERT INTO [dbo].[Orders]
                           ([orderId]
                           ,[pulsa_code]
                           ,[cn_pulsatype]
                           ,[masaaktif]
                           ,[cn_quatity]
                           ,[cn_op]
                           ,[cn_price]
                           ,[cn_oldprice]
                           ,[OperatorName])
                     VALUES ('{0}','{1}','{2}','{3}',{4},'{5}',{6},{7},'{8}');";
            sql = String.Format(sql, orders.OrderId, orders.PulsaCode, orders.CnPulsatype, orders.Masaaktif, orders.CnQuatity,
                orders.CnOp, orders.CnPrice, orders.CnOldprice, orders.OperatorName);

            return MyData.DataBase.Base_cmd(sql);

        }
    }
}
