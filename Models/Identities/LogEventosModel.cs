using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace HGI.Models.Identities
{
    public class LogEventosModel
    {
        internal string _StrConDB { get; set; }

        public LogEventosModel(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public LogEventosModel()
        {

        }
        
        internal DataTable getLogEventos(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.ACELogEventos_Sel @pnClaUbicacion = '" + Args[0].ToString() + "'" +
                                                      ", @PnIdModulo = " + Args[1].ToString() +
                                                      ", @PnClaveInt = " + Args[2].ToString() + 
                                                      ", @PsClaveString = " + Args[3].ToString();
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("LogEventosModel-getLogEventos: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }
    }
}