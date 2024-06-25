using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_CONFIG
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_Config getItem(string name)
        {
            return db.tb_Config.FirstOrDefault(x => x.Name == name);
        }
    }
}
