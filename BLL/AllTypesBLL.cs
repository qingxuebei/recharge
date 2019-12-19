using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AllTypesBLL
    {
        public List<Model.AllTypes> getListByQuery(string strWhere)
        {
            DAL.AllTypesDal allTypesDal = new DAL.AllTypesDal();
            return allTypesDal.getListByQuery(strWhere);
        }
    }
}
