using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public static class unit
    {
       static private HREntities ob {get;set;}
       static public HREntities getEntity()
        {
            if (ob==null)
            {
                ob = new HREntities();
                ob.Configuration.ProxyCreationEnabled = false;
            }
            return ob;
        }

    }
}