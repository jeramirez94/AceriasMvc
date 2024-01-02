using HGI.Models.Modules;
using HGI.Models.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace HGI.Controllers
{
    public class HomeController : Controller
    {
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
        public string getTableHeader()
        {
            HomeModel oModel = new HomeModel();
            DataTable dtData;
            object[] Args = new object[2];
            Args[0] = DateTime.Now.ToString("yyyyMMdd");
            Args[1] = 2;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getTableHeader(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("Home-getTableHeader-" + ex.ToString());
                return "2#emt5y";
            }
        }

        [HttpPost]
        public string getDemorasAcumuladas()
        {
            HomeModel oModel = new HomeModel();
            DataTable dtData;
            object[] Args = new object[2];
            Args[0] = DateTime.Now.ToString("yyyyMMdd");
            Args[1] = 2;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getDemorasAcumuladas(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("Home-getTableHeader-" + ex.ToString());
                return "2#emt5y";
            }
        }

        [HttpPost]
        public string getDemorasTiempo()
        {
            HomeModel oModel = new HomeModel();
            DataTable dtData;
            object[] Args = new object[2];
            Args[0] = DateTime.Now.ToString("yyyyMMdd");
            Args[1] = 2;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getDemorasTiempo(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("Home-getTableHeader-" + ex.ToString());
                return "2#emt5y";
            }
        }

        [HttpPost]
        public string getDemorasFrecuencia()
        {
            HomeModel oModel = new HomeModel();
            DataTable dtData;
            object[] Args = new object[2];
            Args[0] = DateTime.Now.ToString("yyyyMMdd");
            Args[1] = 2;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getDemorasFrecuencia(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("Home-getTableHeader-" + ex.ToString());
                return "2#emt5y";
            }
        }

        [HttpPost]
        public string getDemorasAuxiliares()
        {
            HomeModel oModel = new HomeModel();
            DataTable dtData;
            object[] Args = new object[2];
            Args[0] = DateTime.Now.ToString("yyyyMMdd");
            Args[1] = 2;
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getDemorasAuxiliares(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("Home-getTableHeader-" + ex.ToString());
                return "2#emt5y";
            }
        }
    }
}
