using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FinancialSPA.Controllers
{
    /// <summary>
    /// Summary description for MainAplication
    /// </summary>
    public class MainAplication : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        


        public void ProcessRequest(HttpContext context)
        {
            string result = "";
            string tipoContenido = "text/json";
            context.Response.ContentType = tipoContenido;

            try
            {
                string Error = string.Empty;
                bool Res = false; 
                int op = int.Parse(context.Request.QueryString["op"].ToString());

                if (Error.Length > 0)
                {
                    throw new Exception(Error);
                }

                switch (op)
                {

                    case 100:
                        {
                            #region Autenticar

                            string usuario = context.Request.QueryString["Username"];
                            string contrasena = context.Request.QueryString["Password"];

                            Res = true;//negocio.Autenticar(usuario, contrasena, ref error);

                            if (Error.Length > 0)
                            {
                                throw new Exception(Error);
                            }
                            else
                            {
                                tipoContenido = "text/json";
                                result = JsonConvert.SerializeObject(Res);
                            }

                            #endregion Autenticar
                        }
                        break;
                    case 200:
                        {
                            #region Cargar proveedores

                            DataTable tblRes;// = negocio.CamposTablaArchivador(NOMBREARCHIVADOR, ref error);

                            if (Error.Length > 0)
                            {
                                throw new Exception(Error);
                            }
                            else
                            {
                                result = Newtonsoft.Json.JsonConvert.SerializeObject(tblRes);
                            }

                            #endregion Cargar proveedores
                        }
                        break;
                    case 115:
                        {
                            #region Autenticar Dw Final

                            //string usuario = context.Request.QueryString["usuario"];

                            //string contrasena = context.Request.QueryString["contrasena"];


                            //bool res = negocio.AutenticarDW(usuario, contrasena, ref error);


                            //if (error.Length > 0)
                            //{
                            //    throw new Exception(error);
                            //}
                            //else
                            //{

                            //    tipoContenido = "text/json";
                            //    result = JsonConvert.SerializeObject(res);

                            //}

                            #endregion Autenticar Dw
                        }
                        break;
                    case 200:
                        {
                            #region Autenticar admin

                            //string lUsuarioDB = context.Request.QueryString["usuario"];

                            //string lContrasenaDB = context.Request.QueryString["contrasena"];

                            ////string lDriverAdmin = context.Request.QueryString["driverAdmin"];

                            ////string lServidorAdmin = context.Request.QueryString["servidorAdmin"];

                            ////negocio = new NegocioCI(lServidorAdmin, lUsuarioDB, nombreDBConsulta, lContrasenaDB, lDriverAdmin, context.Request, ref error);

                            //negocio = new NegocioCI(servidorAdmin, lUsuarioDB, nombreDBConsulta, lContrasenaDB, driverAdmin, context.Request, ref error);

                            //if (error.Length > 0)
                            //{
                            //    throw new Exception(error);
                            //}

                            ////bool res = negocio.AutenticarAdmin(lUsuarioDB, lContrasenaDB, lDriverAdmin, lServidorAdmin, ref error);

                            //bool res = negocio.AutenticarAdmin(lUsuarioDB, lContrasenaDB, driverAdmin, servidorAdmin, ref error);

                            //if (error.Length > 0)
                            //{
                            //    throw new Exception(error);
                            //}
                            //else
                            //{
                            //    /*
                            //    servidorAdmin = lServidorAdmin;
                            //    driverAdmin = lDriverAdmin;
                            //    usuarioBd = lUsuarioDB;
                            //    */
                            //    contrasenaAdminEnc = NegocioCI.Encriptar(lContrasenaDB, ref error);

                            //    bool modificarCfg = false;

                            //    if (lUsuarioDB.Equals(usuarioBd))
                            //    {
                            //        if (lContrasenaDB.Equals(lContrasenaDB))
                            //        {
                            //            /*
                            //            if (lServidorAdmin.Equals(servidorAdmin))
                            //            {
                            //                modificarCfg = false;
                            //            }
                            //            else
                            //            {
                            //                modificarCfg = true;
                            //            }
                            //            */
                            //        }
                            //        else
                            //        {
                            //            modificarCfg = true;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        modificarCfg = true;
                            //    }

                            //    if (modificarCfg)
                            //    {

                            //        //Configuration cfg = ConfigurationManager.(ConfigurationUserLevel.None);

                            //        Configuration cfg = WebConfigurationManager.OpenWebConfiguration(context.Request.ApplicationPath);

                            //        //cfg.AppSettings.Settings["servidorAdmin"].Value = lServidorAdmin;

                            //        //cfg.AppSettings.Settings["driverAdmin"].Value = lDriverAdmin;
                            //        cfg.AppSettings.Settings["usuarioBd"].Value = lUsuarioDB;
                            //        cfg.AppSettings.Settings["contrasena"].Value = contrasenaAdminEnc;

                            //        cfg.Save();

                            //    }

                                //tipoContenido = "text/json";
                                //result = JsonConvert.SerializeObject(Res);

                            //}

                            #endregion Autenticar admin
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                tipoContenido = "text";
                result = "ERROR: " + ex.Message;
            }



            context.Response.ContentType = tipoContenido;
            context.Response.Write(result);

        }





    }
}