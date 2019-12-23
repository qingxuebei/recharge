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
            //2.若使用了积分，则UserPoints表里添加一条积分消费记录，状态为积分锁定中；
            //3.用户点击支付：
            //      a.等待用户充值成功，若成功，则修改Order表的WechatpayState状态为支付成功，接着调起三方支付接口进行充值，等待回调
            //      b.得到回调通知，若成功，则修改Order表PulsaState状态为充值成功；若失败，则进入故障订单
            //4. 3.a第三方充值成功：消费积分记录状态变成已消费，同时推荐人积分按以下情况记录
            //      首次充值：积分表添加一条推荐人的积分记录，积分1分，状态为成功
            //      非首次充值：若扣完积分，还需要支付费用，则积分表添加一条推荐人的积分记录(商品价格-积分)*1%，状态为成功
            //5. 3.b第三方充值失败（重试三次）：进入故障订单列表，后台手动处理，若成功，则走第4步流程；
            //      若失败：发起退款流程，WechatpayState变为已失败；UserPoints状态变为已取消
            //

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
