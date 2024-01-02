using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HGI.Models.Identities
{
    public class TurnoModel
    {
        internal string _StrConDB { get; set; }

        public TurnoModel(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public TurnoModel()
        {

        }
        
        internal DataTable getTurno(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC AceSch.AceTurnoCmb @pnClaUbicacion = '" + Args[0].ToString() + "'" +
                                                      ", @pnIncluirTodosSN = " + Args[1].ToString() +
                                                      ", @pnwtk0 = '" + Args[2].ToString()+"'";
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("TurnoModel-getTurno: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }
    }
}