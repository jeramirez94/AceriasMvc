using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;

namespace HGI.Models.Modules.Patios
{
    public class DensidadVolumen
    {
        internal string _StrConDB { get; set; }

        public DensidadVolumen(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public DensidadVolumen()
        {

        }
        
        internal DataTable getA1Diario(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC [ACESch].[HgiChatarraDensidadVolumenDBGrupo_Rpt] @pnPlanta = '" + Args[0].ToString() + "'" +
                                                      ", @pnHorno = '" + Args[1].ToString() + "'" ;
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("DensidadVolumen-getA1Diario: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getAgrupadoCargas(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC [ACESch].[ACEHgiChatarraDensidadVolumenGrupo_Rpt] @pnPlanta = '" + Args[0].ToString() + "'" +
                                                      ", @pnHorno = '" + Args[1].ToString() + "'" +
                                                      ", @pdFechaInicio = '" + Args[2].ToString() + "'" +
                                                      ", @pdFechaFin = '" + Args[3].ToString() + "'" +
                                                      ", @psProducto = '" + Args[4].ToString() + "'";
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("DensidadVolumen-getAgrupadoCargas: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal DataTable getAgrupadoSemanas(object[] Args)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC [ACESch].[HgiChatarraDyVRptDia] @pnPlanta = '" + Args[0].ToString() + "'" +
                                                      ", @pnHorno = '" + Args[1].ToString() + "'" +
                                                      ", @pdFechaInicio = '" + Args[2].ToString() + "'" +
                                                      ", @pdFechaFin = '" + Args[3].ToString() + "'" +
                                                      ", @psProducto = '" + Args[4].ToString() + "'" +
                                                      ", @PnEsReporting = '" + Args[5].ToString() + "'";
            try
            {
                dt = conn.getDataTable(sql);

                return dt;
            }
            catch (Exception)
            {
                LogWritter.WriteErrorLog("DensidadVolumen-getAgrupadoSemanas: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }
        
    }
}