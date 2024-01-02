using HGI.Models.General;
using HGI.Models.Identities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HGI.Controllers.Identities
{
    public class LogEventosController : Controller
    {
        //
        // GET: /LogEventos/

        [HttpPost]
        public string getLogEventos(int IdModulo, string ClaveInt, string ClaveString)
        {
            LogEventosModel oModel = new LogEventosModel();
            DataTable dtData;
            object[] Args = new object[4];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = IdModulo;
            Args[2] = ClaveInt;
            Args[3] = ClaveString == "null" || ClaveString.Trim() == "" ? "null" : "'" + ClaveString + "'";

            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getLogEventos(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("LogEventosController-getLogEventos-" + ex.ToString());
                return "2#emt5y";
            }
        }

    }
}
