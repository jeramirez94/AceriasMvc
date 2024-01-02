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
    public class TurnoController : Controller
    {
       
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public string getTurno(int IncluirTodosSN, string wtk0)
        {
            TurnoModel oModel = new TurnoModel();
            DataTable dtData;
            object[] Args = new object[3];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = IncluirTodosSN;
            Args[2] = wtk0 == null ? "" : wtk0;

            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getTurno(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("TurnoController-getTurno-" + ex.ToString());
                return "2#emt5y";
            }
        }

    }
}
