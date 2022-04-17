using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;

namespace ReporteDBModel
{    
   public class Controlador
    {
        static public ReporteDBEntities Context = null;
        
        public Controlador()
        {
            if(Context == null)
                Context = new ReporteDBEntities();
        }
         public ReporteDBEntities GetCnx()
         {
             return Context ?? (Context = new ReporteDBEntities());
         }
        public ReporteDBEntities Cnx
        {
            get { return GetCnx(); }
            set { Context = value; }
        }
    }
}
