using HGI.Controllers.Identities;
using HGI.Models.General;
using HGI.Models.Identities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace HGI.Models
{
    public class Account
    {
        internal string _StrConDB { get; set; }

        public Account(string StrConDB)
        {
            _StrConDB = StrConDB;
        }

        internal Usuario getLogin(string user, string password)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            Usuario oUsuario;

            string url = @"https://apprest.deacero.com/APPCONTAINER_WS/WebService.asmx/ValidatePassword?Usuario=[user]&Password=[pass]";

            url = url.Replace("[user]",user).Replace("[pass]", password);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            oUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(json);

            oUsuario.LOGIN = oUsuario.USERNAME;

            return oUsuario;
        }

        internal Usuario getLoginByToken(string token)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC [DTSch].[LoginValidaToken] @psToken = " + token.Replace("t=","");
            Usuario oUsuario;
            try
            {
                dt = conn.getDataTable(sql);
                if (dt.Rows.Count > 0) // existe el token
                {
                    if (dt.Rows[0][0].ToString().Trim() != "") // devuelve un login válido
                    {
                        oUsuario = new Usuario();
                        oUsuario.LOGIN = dt.Rows[0][0].ToString().Trim();
                        sql = "EXEC [dbo].[TiObtenerUsuariosPorLoginSel] @login = " + dt.Rows[0][0].ToString().Trim();
                        dt = conn.getDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            oUsuario.USERID = int.Parse(dt.Rows[0]["IdUsuario"].ToString());
                            oUsuario.USERNAME = dt.Rows[0]["ClaEmpleado"].ToString().Trim();
                            oUsuario.EMAIL = dt.Rows[0]["Email"].ToString().Trim();
                            oUsuario.CLAEMPLEADO = 0;
                            oUsuario.NOMBRE = dt.Rows[0]["NombreUsuario"].ToString().Trim() + " " + dt.Rows[0]["ApellidoPaterno"].ToString().Trim() + " " + dt.Rows[0]["ApellidoMaterno"].ToString().Trim();
                            return oUsuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                Models.General.LogWritter.WriteErrorLog("getConexiones-Account: " + conn.error + "; SP: " + sql);
                return null;
            }
        }

        internal ConexionModel getConexiones(int ClaUbicacion, int ClaSistema)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC [HGISch].[HGICadenasDeConexion_Sel] @PnClaUbicacion = " + ClaUbicacion.ToString() + ", @PnClaSistema = " + ClaSistema.ToString();
            ConexionModel cm = new ConexionModel();
            try
            {
                dt = conn.getDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    cm.operacionDefault = dt.Select("ClaTipo = 1")[0]["Cadena"].ToString();
                    cm.odsDefault = dt.Select("ClaTipo = 2")[0]["Cadena"].ToString();
                    cm.seguridadDefault = dt.Select("ClaTipo = 3")[0]["Cadena"].ToString();
                    return cm;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                Models.General.LogWritter.WriteErrorLog("getConexiones-Account: " + conn.error + "; SP: " + sql);
                return null;
            }
        }

        internal DataTable getPermisos(int ClaUbicacion, string User, int ClaSistema)
        {
            DataTable dt = new DataTable();
            Connection conn = new Connection();
            conn._connectionString = this._StrConDB;
            string sql = "EXEC dbo.HGIUsuarioPermisos_Sel @PnClaUbicacion = " + ClaUbicacion
                                                     + ", @PsLoginUsuario = '" + User + "'"
                                                     + ", @PnClaSistema = " + ClaSistema;
            try
            {
                dt = conn.getDataTable(sql);
                return dt;
            }
            catch (Exception)
            {
                Models.General.LogWritter.WriteErrorLog("getPermisos-Account: " + conn.error + "; SP: " + sql);
                return dt;
            }
        }

        internal bool checkPermiso(int IdObjeto, DataTable Permisos)
        {
            DataRow[] result;
            string qrySelect = "idObjeto = " + IdObjeto + " and Consulta = 1";
            result = Permisos.Select(qrySelect);
            if (result.Length > 0)
                return true;
            else
                return false;
        }
    }
}
