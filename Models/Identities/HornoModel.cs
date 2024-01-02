using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HGI.Models.Identities
{
    public class HornoModel
    {
        internal string _StrConDB { get; set; }

        public HornoModel(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public HornoModel()
        {

        }
        
        internal DataTable getHornoFusion(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC AceSch.AceHornoFusionCmb @pnClaUbicacion = '" + Args[0].ToString() + "'" +
                                                      ", @psValor = '" + Args[1].ToString() +"'"+
                                                      ", @pnTipo = " + Args[2].ToString() +
                                                      ", @ModoSel = default " +
                                                      ", @pnModoSel = default" +
                                                      ", @pnBajasSn = default" +
                                                      ", @pnIncluirTodosSN = " + Args[3].ToString() +
                                                      ", @pnIncluirSinHornoSN = default" +
                                                      ", @pnDeaceroSN = default" +
                                                      ", @pnMostrarTodosSN = default" +
                                                      ", @pnFiltro = default" +
                                                      ", @pnClaSubAlmacen = default";
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("HornoModel-getHornoFusion: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }
    }
}