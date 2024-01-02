using HGI.Models.Modules.Patios;
using HGI.Models.Modules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using HGI.Models.General;

namespace HGI.Controllers.Acerias
{
    public class ObjetivosUCIController : Controller
    {
        //
        // GET: /ObjetivosUCI/

        public ActionResult Index()
        {
            if (Session["IdUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public string getA1Diario(string FechaInicial, string FechaFinal, int ClaHorno)
        {
            DensidadVolumen oModel = new DensidadVolumen();
            DataTable dtData;
            object[] Args = new object[2];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = ClaHorno;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getA1Diario(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y Sin datos A1";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("DensidadVolumen-getA1Diario-" + ex.ToString());
                return "2#emt5y Error al cargar A1 diario";
            }
        }

    }
}
