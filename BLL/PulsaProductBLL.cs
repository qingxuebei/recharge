using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PulsaProductBLL
    {
        public void Insert(List<Model.PulsaProduct> pulsaProductList, OleDbTransaction tr)
        {
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            pulsaProductDal.Insert(pulsaProductList, tr);
        }

        public List<Model.PulsaProduct> getNormalList()
        {
            String strWhere = " and cn_status=1 and status = 'active'";
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.getListByQuery(strWhere);
        }

        public List<Model.PulsaProductShow> getNormalShowList()
        {
            String strWhere = " and cn_status=1 and status = 'active' order by cn_price";
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.getShowListByQuery(strWhere);
        }

        public Model.PulsaProduct getFirstById(String pulsaCode)
        {
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.getFirstById(pulsaCode);
        }
    }
}
