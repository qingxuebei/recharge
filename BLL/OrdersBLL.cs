using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrdersBLL
    {
        public bool Insert(Model.Orders orders)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            return ordersDal.Insert(orders);
        }
    }
}
