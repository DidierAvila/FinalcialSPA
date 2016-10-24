using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace Negocio
{
    public class Proveedor
    {
        DataTable dtResponse = new DataTable();
        private string sError;
        private DataTable dtTabla;
        private string sNombre;

        public string _sNombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }
        public DataTable _dtTabla
        {
            get { return dtTabla; }
            set { dtTabla = value; }
        }
        public string _sError
        {
            get { return sError; }
            set { sError = value; }
        }



        #region Métodos 

        public bool CargarListaProveedores()
        {
            Conexion ObjConexion = new Conexion();

            ObjConexion._SSQL = "PA_ListarProveedores";

            if (ObjConexion.LlenarDataTable())
            {
                if (ObjConexion._dtTabla.Rows.Count > 1)
                {
                    this._dtTabla = ObjConexion._dtTabla;

                    ObjConexion.CerrarConexion();
                    ObjConexion = null;
                    _sError = "Correcto";
                    return true;
                }
                else
                {
                    _sError = ObjConexion._SErrorConexion;
                    ObjConexion.CerrarConexion();
                    ObjConexion = null;
                    return false;
                }
            }
            else
            {
                _sError = ObjConexion._SErrorConexion;
                ObjConexion.CerrarConexion();
                ObjConexion = null;
                return false;
            }
        }

        public bool CargarProveedores()
        {
            Conexion ObjConexion = new Conexion();

            ObjConexion._SSQL = "PA_CargarProveedores";

            if (ObjConexion.LlenarDataTable())
            {
                if (ObjConexion._dtTabla.Rows.Count > 1)
                {
                    this._dtTabla = ObjConexion._dtTabla;
                    ObjConexion.CerrarConexion();
                    ObjConexion = null;
                    _sError = "Correcto";
                    return true;
                }
                else
                {
                    _sError = ObjConexion._SErrorConexion;
                    ObjConexion.CerrarConexion();
                    ObjConexion = null;
                    return false;
                }
            }
            else
            {
                _sError = ObjConexion._SErrorConexion;
                ObjConexion.CerrarConexion();
                ObjConexion = null;
                return false;
            }
        }

        #endregion Métodos



    }
}
