using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;

namespace HGI.Models.Modules
{
    public class CaracterizacionDeChatarraModel
    {

        internal string _StrConDB { get; set; }

        public CaracterizacionDeChatarraModel(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public CaracterizacionDeChatarraModel()
        {

        }
        
        internal DataTable getColadasPorRango(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.MVCColadasPorRangoFecha_Sel @pnClaUbicacion = '" + Args[0].ToString() + "'" +
                                                      ", @ptFechaInicial = '" + Args[1].ToString() + "'" +
                                                      ", @ptFechaFinal = '" + Args[2].ToString() + "'" +
                                                      ", @pnHornoFusion = " + Args[3].ToString() +
                                                      ", @pnTurno39 = " + Args[4].ToString() +
                                                      ", @pnrdFechas = " + Args[5].ToString() +
                                                      ", @pnColadaInicial = " + Args[6].ToString() +
                                                      ", @pnColadaFinal = " + Args[7].ToString();
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("CaracterizacionDeChatarraModel-getColadasPorRango: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getCaracterizacion(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC ACESch.ACECaracterizaChatarra_U @pnClaUbicacion = '" + Args[0].ToString() + "'" +
                                                      ", @PsColadas = '" + Args[1].ToString() + "'" +
                                                      ", @PiHorno = " + Args[2].ToString() +
                                                      ", @PiMetodo = " + Args[3].ToString() +
                                                      ", @PsNombrePCMod = '" + Args[4].ToString() + "'" +
                                                      ", @PnClaUsuarioMod = " + Args[5].ToString() +
                                                      ", @PnConsultar = " + Args[6].ToString() ;
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("CaracterizacionDeChatarraModel-getCaracterizacion: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getCatalogoChatarra(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC [ACESch].[Ace_CU770_Pag1_Grid_GridCatChat_Sel]";
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("CaracterizacionDeChatarraModel-getCatalogoChatarra: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

    }
}