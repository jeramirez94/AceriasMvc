using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using HGI.Models;
using System.Configuration;
using System.Data;
using HGI.Controllers.Identities;
using HGI.Models.Identities;

namespace HGI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session["ClaUbicacion"] = int.Parse(ConfigurationManager.AppSettings["ubicacion"].ToString());
            Session["ClaSistema"] = int.Parse(ConfigurationManager.AppSettings["sistema"].ToString());
            ViewBag.ReturnUrl = returnUrl;

            if (!loadConexiones())
            {
                return RedirectToAction("Error403", "Unauthorized");
            }
            if (Session["DBString"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Error500", "Unauthorized");
        }

        [AllowAnonymous]
        public ActionResult LoginByToken(string t, string returnUrl)
        {
            Session["ClaUbicacion"] = int.Parse(ConfigurationManager.AppSettings["ubicacion"].ToString());
            Session["ClaSistema"] = int.Parse(ConfigurationManager.AppSettings["sistema"].ToString());
            ViewBag.ReturnUrl = returnUrl;

            Account ac = new Account(ConfigurationManager.ConnectionStrings["DBString"].ConnectionString);
            if (!loadConexiones())
            {
                return RedirectToAction("Error403", "Unauthorized");
            }
            if (Session["DBString"] != null)
            {
                ac._StrConDB = Session["SegString"].ToString();
                Usuario usuario = ac.getLoginByToken(t);
                if (usuario == null)
                {
                    return RedirectToAction("Login", new { returnUrl = returnUrl });
                }
                else
                {
                    loadUsuario(usuario);
                    loadPermisos(usuario.LOGIN);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
                return RedirectToAction("Error500", "Unauthorized");
        }

        [HttpPost]
        [AllowAnonymous]
        public string getLogin(string user, string password)
        {
            user = user.Trim();
            password = password.Trim();

            Usuario oUsuario = new Account(Session["DBString"].ToString()).getLogin(user, password);
            if (oUsuario != null)
            {
                loadUsuario(oUsuario);
                loadPermisos(user);
                return "1";
            }
            else
            {
                return "0";
            }
        }

        internal void loadUsuario(Usuario usuario)
        {
            Session["IdUser"] = usuario.USERID;
            Session["Username"] = usuario.NOMBRE;
            Session["UserLastname"] = "";
            Session["ClaUsuario"] = usuario.USERID;
        }

        internal bool loadConexiones()
        {
            ConexionModel cm = new Account(ConfigurationManager.ConnectionStrings["DBString"].ConnectionString)
                            .getConexiones(int.Parse(ConfigurationManager.AppSettings["ubicacion"].ToString()), int.Parse(ConfigurationManager.AppSettings["sistema"].ToString()));
            if (cm != null)
            {
                Session["DBString"] = cm.operacionDefault;
                Session["ODSString"] = cm.odsDefault;
                Session["SegString"] = cm.seguridadDefault;

                return true;
            }
            else
            {
                Session["DBString"] = null;
                Session["ODSString"] = null;
                Session["SegString"] = null;

                return false;
            }
        }

        internal void loadPermisos(string user)
        {
            DataTable permission = new Account(Session["SegString"].ToString()).getPermisos(int.Parse(Session["ClaUbicacion"].ToString()), user, int.Parse(Session["ClaSistema"].ToString()));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

       

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
