using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Negocio;

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

                //if (op != 170)
                //{

                //    string contrasenaAdmin = NegocioCI.Desencriptar(contrasenaAdminEnc, ref error);

                //    negocio = new NegocioCI(servidorAdmin, usuarioBd, nombreDBConsulta, contrasenaAdmin, driverAdmin, context.Request, ref error);

                //    if (Error.Length > 0)
                //    {
                //        throw new Exception(Error);
                //    }

                //}

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
                            Proveedor Nproveedor = new Proveedor();
                            DataTable TblRes = new DataTable();

                            if(Nproveedor.CargarListaProveedores())
                            {
                                TblRes = Nproveedor._dtTabla;
                                result = Newtonsoft.Json.JsonConvert.SerializeObject(TblRes);
                                TblRes = null;
                            }
                            else
                            {
                                throw new Exception(Nproveedor._sError);
                            }

                            #endregion Cargar proveedores
                        }
                        break;
                    case 300:
                        {
                            #region Cargar orden de compra por proveedores

                            OrdenCompra NordenCompra = new OrdenCompra();
                            DataTable TblRes = new DataTable();
                            string Idproveedor = string.Empty;
                            Idproveedor = context.Request.QueryString["Idproveedor"];

                            if (string.IsNullOrEmpty(Idproveedor))
                                throw new Exception("Idproveedor esta vacio");

                            NordenCompra._iIdProveedor = int.Parse(Idproveedor);
                            if (NordenCompra.ListarOrdenCompraXproveedor())
                            {
                                TblRes = NordenCompra._dtTabla;
                                result = Newtonsoft.Json.JsonConvert.SerializeObject(TblRes);
                                TblRes = null;
                            }
                            else
                            {
                                throw new Exception(NordenCompra._sError);
                            }

                            #endregion Cargar orden de compra por proveedores
                        }
                        break;
                    case 400:
                        {
                            #region Generar consecutivo

                            Entrada Nentrada = new Entrada();
                            string NroCosecutivo = string.Empty;

                            if (Nentrada.GenerarConsecutivo())
                            {
                                NroCosecutivo = Nentrada._sConsecutivo;
                                result = Newtonsoft.Json.JsonConvert.SerializeObject(NroCosecutivo);
                            }
                            else
                            {
                                throw new Exception(Nentrada._sError);
                            }

                            #endregion Generar consecutivo

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
                    case 500:
                        {
                            #region Cargar proveedores

                            Proveedor Nproveedores = new Proveedor();
                            DataTable TblRes = new DataTable();
                            string Nombre = string.Empty;
                            Nombre = context.Request.QueryString["Nombre"];

                            if (string.IsNullOrEmpty(Nombre))
                                throw new Exception("El nombre esta vacio");

                            Nproveedores._sNombre = Nombre;
                            if (Nproveedores.CargarProveedores())
                            {
                                TblRes = Nproveedores._dtTabla;
                                result = Newtonsoft.Json.JsonConvert.SerializeObject(TblRes);
                                TblRes = null;
                            }
                            else
                            {
                                throw new Exception(Nproveedores._sError);
                            }

                            #endregion Cargar proveedores
                        }
                        break;
                    case 600:
                        {
                            #region Cargar orden compra

                            OrdenCompra NordenCompra = new OrdenCompra();
                            DataTable TblRes = new DataTable();
                            string Id = string.Empty;
                            Id = context.Request.QueryString["Id"];

                            if (string.IsNullOrEmpty(Id))
                                throw new Exception("El Id esta vacio");

                            NordenCompra._iIdOrdenCompra = int.Parse(Id);
                            if (NordenCompra.CargarOrdenCompraXid())
                            {
                                TblRes = NordenCompra._dtTabla;
                                result = Newtonsoft.Json.JsonConvert.SerializeObject(TblRes);
                                TblRes = null;
                            }
                            else
                            {
                                throw new Exception(NordenCompra._sError);
                            }

                            #endregion Cargar orden compra
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