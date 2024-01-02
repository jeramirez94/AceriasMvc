using HGI.Models.Modules.Patios;
using HGI.Models.Modules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using HGI.Models.General;
using HGI.Models.Modules.Acerias;

namespace HGI.Controllers.Acerias
{
    public class TiempoDisponibleUCIController : Controller
    {
        //
        // GET: /TiempoDisponibleUCI/

        public ActionResult Index()
        {
            if (Session["IdUser"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logn", "Account");
            }
            return View();
        }

        [HttpPost]
        public string getTiempoDisponible()
        {
            TiempoDisponibleModel oModel = new TiempoDisponibleModel();
            DataTable dtData;
            object[] Args = new object[1];
            Args[0] = Session["ClaUbicacion"].ToString();
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getTiempoDisponible(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y Sin datos Tiempo disponible";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("TiempoDisponibleUCI-getTiempoDisponible-" + ex.ToString());
                return "2#emt5y Error al cargar Tiempo disponible";
            }
        }
    }
}
