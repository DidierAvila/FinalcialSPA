using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class OrdenCompra
    {
        private string sError;
        private int iIdOrdenCompra;
        private int iIdProveedor;
        private DataTable dtTabla;

        public DataTable _dtTabla
        {
            get { return dtTabla; }
            set { dtTabla = value; }
        }
        public int _iIdProveedor
        {
            get { return iIdProveedor; }
            set { iIdProveedor = value; }
        }
        public int _iIdOrdenCompra
        {
            get { return iIdOrdenCompra; }
            set { iIdOrdenCompra = value; }
        }
        public string _sError
        {
            get { return sError; }
            set { sError = value; }
        }



        #region Métodos

        public bool ListarOrdenCompraXproveedor()
        {
            Conexion ObjConexion = new Conexion();

            ObjConexion._SSQL = "EXECUTE PA_ListarOrdenCompraXproveedor " + _iIdProveedor;

            if (ObjConexion.LlenarDataTable())
            {
                if (ObjConexion._dtTabla.Rows.Count > 0)
                {
                    dtTabla = ObjConexion._dtTabla;
                    ObjConexion.CerrarConexion();
                    ObjConexion = null;
                    _sError = "";
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

        public bool CargarOrdenCompraXid()
        {
            Conexion ObjConexion = new Conexion();

            ObjConexion._SSQL = "EXECUTE PA_ConsultarOrdenCompraXid " + _iIdOrdenCompra;

            if (ObjConexion.LlenarDataTable())
            {
                if (ObjConexion._dtTabla.Rows.Count > 0)
                {
                    dtTabla = ObjConexion._dtTabla;
                    ObjConexion.CerrarConexion();
                    ObjConexion = null;
                    _sError = "";
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


        #endregion
    }
}
