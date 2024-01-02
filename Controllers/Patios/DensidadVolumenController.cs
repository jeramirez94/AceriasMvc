using HGI.Models.Modules.Patios;
using HGI.Models.Modules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using HGI.Models.General;

namespace HGI.Controllers.Patios
{
    public class DensidadVolumenController : Controller
    {
        //
        // GET: /DensidadVolumen/

        public ActionResult Index()
        {
            if (Session["IdUser"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Account");
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

        [AllowAnonymous]
        public string getAgrupadoCargas(string FechaInicial, string FechaFinal, int ClaHorno, string search = "")
        {
            DensidadVolumen oModel = new DensidadVolumen();
            DataTable dtData;
            object[] Args = new object[5];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = ClaHorno;
            Args[2] = FechaInicial;
            Args[3] = FechaFinal;
            Args[4] = "CH-1,CH01,CH02,CH03,CH05,CH06,CH07,CH08,CH09,CH10,CH34,CH50,CH60";
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getAgrupadoCargas(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y Sin datos agrupado cargas";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("DensidadVolumen-getAgrupadoCargas-" + ex.ToString());
                return "2#emt5y Error al cargar agrupado por cargas";
            }
        }

        [AllowAnonymous]
        public string getAgrupadoSemanas(string FechaInicial, string FechaFinal, int ClaHorno, string search = "")
        {
            DensidadVolumen oModel = new DensidadVolumen();
            DataTable dtData;
            object[] Args = new object[6];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = ClaHorno;
            Args[2] = FechaInicial;
            Args[3] = FechaFinal;
            Args[4] = "CH-1,CH01,CH02,CH03,CH05,CH06,CH07,CH08,CH09,CH10,CH34,CH50,CH60";
            Args[5] = 0;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getAgrupadoSemanas(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y Sin datos agrupado semana";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("DensidadVolumen-getAgrupadoSemanas-" + ex.ToString());
                return "2#emt5y Error al cargar agrupado por semanas";
            }
        }
    }
}
