using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;

namespace HGI.Models.Modules.Acerias
{
    public class TiempoDisponibleModel
    {

        internal string _StrConDB { get; set; }

        public TiempoDisponibleModel(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public TiempoDisponibleModel()
        {

        }
        
        internal DataTable getTiempoDisponible(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.AceHGITiempoDisponibleUCI_Sel @PnClaUbicacion = '" + Args[0].ToString() + "'";
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("TiempoDisponible-getTiempoDisponible: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }
    }
}