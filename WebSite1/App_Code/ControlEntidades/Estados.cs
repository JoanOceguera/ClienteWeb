using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReporteDBModel
{
    public static class Estados
    {
        static private String _pendienteADefectar = "p";
        static private String _siendoDefectado    = "d";
        static private String _solucionado = "s";

        static public String PendienteADefectar { get { return _pendienteADefectar; } }
        static public String SiendoDefectado { get { return _siendoDefectado; } }
        static public String Solucionado { get { return _solucionado; } }
        
    }
}
