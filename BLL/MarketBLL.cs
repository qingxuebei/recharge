using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class MarketBLL
    {
        public bool Insert(Model.Market market)
        {
            DAL.MarketDal marketDal = new MarketDal();
            return marketDal.Insert(market);
        }
        public bool Update(Model.Market market)
        {
            DAL.MarketDal marketDal = new MarketDal();
            return marketDal.Update(market);
        }

        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.MarketDal marketDal = new DAL.MarketDal();
            return marketDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.MarketDal marketDal = new DAL.MarketDal();
            return marketDal.GetRecordCount(strWhere);
        }
        public List<Model.Market> GetList()
        {
            DAL.MarketDal marketDal = new DAL.MarketDal();
            return marketDal.GetList();
        }
    }
}
