using HGI.Models.General;
using HGI.Models.Modules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace HGI.Controllers
{
    public class CaracterizacionDeChatarraController : Controller
    {
        //
        // GET: /CaracterizacionDeChatarra/

        public ActionResult Index()
        {

            if (Session["IdUser"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public string getColadasPorRango(string FechaInicial, string FechaFinal, int ClaHorno, int ClaTurno, int TipoFiltro, int ColadaInicial, int ColadaFinal)
        {
            CaracterizacionDeChatarraModel oModel = new CaracterizacionDeChatarraModel();
            DataTable dtData;
            object[] Args = new object[8];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = FechaInicial;
            Args[2] = FechaFinal;
            Args[3] = ClaHorno;
            Args[4] = ClaTurno;
            Args[5] = TipoFiltro;
            Args[6] = ColadaInicial;
            Args[7] = ColadaFinal;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getColadasPorRango(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("getColadasPorRango-getColadasPorRango-" + ex.ToString());
                return "2#emt5y";
            }
        }

        [HttpPost]
        public string getCaracterizacion(int ClaHorno, string Coladas, int SoloConsultar)
        {
            CaracterizacionDeChatarraModel oModel = new CaracterizacionDeChatarraModel();
            DataTable dtData;
            object[] Args = new object[7];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = Coladas;
            Args[2] = 2; //ClaHorno;
            Args[3] = 1; //PiMetodo
            Args[4] = HttpContext.Request.UserHostAddress.ToString(); //PNombrePCMod
            Args[5] = int.Parse(Session["ClaUsuario"].ToString()); //PClaUsuarioMod
            Args[6] = SoloConsultar; //PConsultar
            
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getCaracterizacion(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                {
                    if (SoloConsultar != 0)
                        return "2#emt5y Sin datos encontrados para los filtros seleccionados";
                    else
                        return "ok";
                }
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("getColadasPorRango-getCaracterizacion-" + ex.ToString());
                return "2#emt5y " +ex.Message;
            }
        }

        [HttpPost]
        public string getCatalogoChatarra()
        {
            CaracterizacionDeChatarraModel oModel = new CaracterizacionDeChatarraModel();
            DataTable dtData;
            object[] Args = new object[1];
            Args[0] = Session["ClaUbicacion"].ToString();
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getCatalogoChatarra(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("getColadasPorRango-getColadasPorRango-" + ex.ToString());
                return "2#emt5y";
            }
        }
    }
}
