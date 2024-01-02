using HGI.Models.General;
using System;
using System.Collections.Generic;
using System.Data;

namespace HGI.Models.Modules.Acerias
{
    public class ObjetivosUCIModel
    {

        internal string _StrConDB { get; set; }

        public ObjetivosUCIModel(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        public ObjetivosUCIModel()
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

    }
}