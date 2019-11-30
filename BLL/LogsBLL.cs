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
    public class LogsBLL
    {
        public bool Insert(Model.Logs logs)
        {
            DAL.LogsDal logsDal = new LogsDal();
            return logsDal.Insert(logs);
        }
       
    }
}
