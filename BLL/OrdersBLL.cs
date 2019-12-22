using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrdersBLL
    {
        public bool Insert(Model.Orders orders)
        {
            //创建订单，
            //1.Order表里添加一条记录，WechatpayState和PulsaState状态为待支付
            //2.首次充值：积分表添加一条推荐人的积分记录，积分1分
            //  非首次充值：若使用了积分，则UserPoints表里添加一条积分消费记录，状态为积分锁定中；
            //      同时若扣完积分，还需要支付费用，则积分表添加一条推荐人的积分记录(商品价格-积分)*1%
            //3.
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            return ordersDal.Insert(orders);
        }

        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            return ordersDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            return ordersDal.GetRecordCount(strWhere);
        }
    }
}
