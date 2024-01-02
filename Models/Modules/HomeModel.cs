using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HGI.Models.Modules
{
    public class HomeModel
    {
        internal string _StrConDB { get; set; }

        public HomeModel(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public HomeModel()
        {

        }

        internal DataTable getTableHeader(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.ProduccionAceriaDemorasMesProc @dtFecha = '" + Args[0].ToString() + "'" +
                                                      ", @nHorno = " + Args[1].ToString();
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("getTableHeader-HomeModel: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getDemorasAcumuladas(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.AceDemorasAcumMesMod @dtFecha = '" + Args[0].ToString() + "'" +
                                                      ", @nHorno = " + Args[1].ToString();
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("getTableHeader-HomeModel: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getDemorasTiempo(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.AceDemorasPorTiempoMod @dtFecha = '" + Args[0].ToString() + "'" +
                                                      ", @nHorno = " + Args[1].ToString();
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("getTableHeader-HomeModel: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getDemorasFrecuencia(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.AceDemoraMayorFrecuenciaMod @dtFecha = '" + Args[0].ToString() + "'" +
                                                      ", @nHorno = " + Args[1].ToString();
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("getTableHeader-HomeModel: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getDemorasAuxiliares(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.AceDemorasMayoresAuxMod @dtFecha = '" + Args[0].ToString() + "'" +
                                                      ", @nHorno = " + Args[1].ToString();
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("getTableHeader-HomeModel: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }
    }
}