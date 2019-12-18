using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>
        /// 获取显示在后台的列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public List<Model.PulsaProduct> getNormalList(string strWhere)
        {
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.getListByQuery(strWhere);
        }

        /// <summary>
        /// 获取可显示在前台的列表
        /// </summary>
        /// <returns></returns>
        public List<Model.PulsaProductShow> getNormalShowList()
        {
            
            String strWhere = " and cn_status=1 and status = 'active' order by cn_price";
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.getShowListByQuery(strWhere);
        }

        /// <summary>
        /// 通过Id查询获取第一个
        /// </summary>
        /// <param name="pulsaCode"></param>
        /// <returns></returns>
        public Model.PulsaProduct getFirstById(String pulsaCode)
        {
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.getFirstById(pulsaCode);
        }

        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.PulsaProductDal pulsaProductDal = new DAL.PulsaProductDal();
            return pulsaProductDal.GetRecordCount(strWhere);
        }
    }
}
