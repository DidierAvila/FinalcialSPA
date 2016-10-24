using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class Entrada
    {
        private string sConsecutivo;
        private string sError;
        private DataTable dtTabla;

        public string _sConsecutivo
        {
            get { return sConsecutivo; }
            set { sConsecutivo = value; }
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
        


        public bool GenerarConsecutivo()
        {
            Conexion ObjConexion = new Conexion();

            ObjConexion._SSQL = "PA_GenerarConsecutivo";

            if (ObjConexion.LlenarDataTable())
            {
                if (ObjConexion._dtTabla.Rows.Count > 0)
                {
                    sConsecutivo = ObjConexion._dtTabla.Rows[0][0].ToString();
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



    }
}
