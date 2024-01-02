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
    public class HornoController : Controller
    {

        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public string getHornoFusion(string Valor, int Tipo, int IncluirTodosSN)
        {
            HornoModel oModel = new HornoModel();
            DataTable dtData;
            object[] Args = new object[4];
            Args[0] = Session["ClaUbicacion"].ToString();
            Args[1] = Valor == null ? "" : Valor ;
            Args[2] = Tipo;
            Args[3] = IncluirTodosSN;
            
            oModel._StrConDB = Session["DBString"].ToString();
            try
            {
                dtData = oModel.getHornoFusion(Args);
                if (dtData.Rows.Count > 0)
                    return JsonConvert.SerializeObject(dtData);
                else
                    return "2#emt5y";
            }
            catch (Exception ex)
            {
                LogWritter.WriteErrorLog("HornoController-getHornoFusion-" + ex.ToString());
                return "2#emt5y";
            }
        }

    }
}
