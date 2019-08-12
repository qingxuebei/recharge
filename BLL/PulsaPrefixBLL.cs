using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PulsaPrefixBLL
    {
        public List<Model.PulsaPrefix> getList()
        {
            DAL.PulsaPrefixDal pulsaPrefixDal = new DAL.PulsaPrefixDal();
            return pulsaPrefixDal.getList();
        }
    }
}
