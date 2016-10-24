using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AccesoDatos
{
    public class Conexion
    {
        public Conexion()
		    {
			    //datos del constructor
			    objCommand = new SqlCommand();
                ObjConecction = new SqlConnection();
			    objDataSet = new DataSet();
			    objParametro = new SqlParameter();
			    objAdapter = new SqlDataAdapter();
                objParametro = new SqlParameter();
		    }

        #region"Atributos"

        public string consulta;
        private String Cadena = ConfigurationManager.ConnectionStrings["ConexionAlmacen"].ConnectionString;
        public SqlConnection ObjConecction;
        private SqlCommand objCommand;
        private SqlDataReader objReader;
        private DataSet objDataSet;
        private SqlDataAdapter objAdapter;
        private bool blnAbrio;
        private SqlParameter objParametro;
        private string SSQL;
        private string SErrorConexion;
        private DataTable dtTabla;
        private string SNombreTabla;

        #endregion

        #region "Propiedades"

        public DataTable _dtTabla
        {
            get { return dtTabla; }
            set { dtTabla = value; }
        }
        public DataSet MiDataSet
        {
            get
            {
                return objDataSet;
            }
        }
        public string _SNombreTabla
        {
            get
            {
                return SNombreTabla;
            }
            set
            {
                SNombreTabla = value;
            }
        }
        public string _SErrorConexion
        {
            get { return SErrorConexion; }
            set { SErrorConexion = value; }
        }
        public string _SSQL
        {
            get { return SSQL; }
            set { SSQL = value; }
        }
        public SqlDataReader _objReader
        {
            get { return objReader; }
            set { objReader = value; }
        }

        public SqlDataReader Reader
		    {
			    get
			    {
				    return objReader;
			    }
		    }

        #endregion

        private bool GenerarString()
        {
            //asignamos a los atributos el objeto creado parametros
            try
            {
                Cadena = ConfigurationManager.ConnectionStrings["ConexionAlmacen"].ConnectionString;
                return true;
            }
            catch (Exception exception)
            {
                SErrorConexion = exception.Message;
                return false;
            }
        }

        public void Desconectar()
        {
            try
            {
                ObjConecction.ConnectionString = Cadena;
                ObjConecction.Close();
                SErrorConexion = "Conexión terminada con éxito";
            }
            catch (Exception obj)
            {
                SErrorConexion = obj.Message;
            }
        }

        private bool AbrirConexion()
        {
            if (GenerarString())
            {
                ObjConecction.ConnectionString = Cadena;
                try //es como preparace si hay problemas si encuentran problemas me llame errores
                {
                    ObjConecction.Open();
                    blnAbrio = true;
                    return true;
                }
                catch (Exception exception)
                {
                    SErrorConexion = exception.Message;
                    blnAbrio = false;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CerrarConexion()
        {
            try
            {
                ObjConecction.Close();
            }
            catch (Exception ex) //Error #1
            {
                SErrorConexion = "No cerró la conexion" + ex.Message;
            }
            try//es como preparace si hay problemas si encuentran problemas me llame errores
            {
                ObjConecction = null;
            }
            catch (Exception ex1)
            {
                SErrorConexion = "No liberó la conexion" + ex1.Message;//error #2
            }
            return true;
        }

        public bool Consultar()
        {
            if (_SSQL == "")
            {
                SErrorConexion = "No definio la instrucción SQL";
                return false;
            }
            if (blnAbrio == false)
            {
                if (AbrirConexion() == false)
                {
                    return false;
                }
            }

            if (objCommand.Parameters.Count > 0)
            {
                objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            }
            else
            {
                objCommand.CommandType = System.Data.CommandType.Text;
            }

            objCommand.Connection = ObjConecction;
            objCommand.CommandText = SSQL;
            try//para controlar el error
            {
                objReader = objCommand.ExecuteReader();//objReader permite capturar datos en una base de datos
                return true;
            }
            catch (Exception ex)
            {
                SErrorConexion = ex.Message;
                return false;
            }
        }

        public bool EjecutarSentencia()
        {
            if (SSQL == "")
            {
                SErrorConexion = "No definio la instrucción SQL";
                return false;
            }
            if (blnAbrio == false)
            {

                if (AbrirConexion() == false)
                {
                    return false;
                }
            }

            if (objCommand.Parameters.Count > 0)
            {
                objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            }
            else
            {
                objCommand.CommandType = System.Data.CommandType.Text;
            }

            objCommand.Connection = ObjConecction;
            //Define el tipo de parámetro a ejecutar, instruccion SQL o Stored Procedure

            objCommand.CommandText = SSQL;
            try//es como preparace si hay problemas si encuentran problemas me llame errores
            {
                objCommand.ExecuteNonQuery();
                //CerrarConexion();
                return true;
            }
            catch (Exception exception)
            {
                SErrorConexion = exception.Message;
                //CerrarConexion();
                return false;
            }
        }

        public bool AgregarParametro(string sNombreParametro, SqlDbType TipoDato, Int32 Tamano, object Valor)
        {
            try
            {
                objParametro.ParameterName = sNombreParametro;
                objParametro.SqlDbType = TipoDato;
                objParametro.Value = Valor;
                objParametro.Size = Tamano;
                objCommand.Parameters.Add(objParametro);
                objParametro = new SqlParameter();
                return (true);
            }
            catch (Exception ex)
            {
                SErrorConexion = ex.Message;
                return (false);
            }
        }

        public bool LlenarDataSet()
        {
            if (_SSQL == "")
            {
                _SErrorConexion = "No definio la instrucción SQL";
                return false;
            }
            if (string.IsNullOrEmpty(_SNombreTabla))
            {
                _SErrorConexion = "No definio el nombre de la tabla";
                return false;
            }

            if (!blnAbrio)
            {
                if (AbrirConexion() == false)
                {
                    return false;
                }
            }

            objCommand.CommandType = CommandType.Text;

            objCommand.Connection = ObjConecction;
            objCommand.CommandText = _SSQL;

            try
            {
                objAdapter.SelectCommand = objCommand;
                objAdapter.Fill(objDataSet, _SNombreTabla);
                return true;
            }
            catch (Exception ex)
            {
                _SErrorConexion = ex.Message;
                return false;
            }
        }

        public bool LlenarDataTable()
        {
            if (_SSQL == "")
            {
                _SErrorConexion = "No definio la instrucción SQL";
                return false;
            }
            if (!blnAbrio)
            {
                if (AbrirConexion() == false)
                {
                    return false;
                }
            }
            objCommand.CommandType = CommandType.Text;
            objCommand.Connection = ObjConecction;
            objCommand.CommandText = _SSQL;

            try
            {
                objAdapter.SelectCommand = objCommand;
                _dtTabla = new DataTable();
                objAdapter.Fill(_dtTabla);
                return true;
            }
            catch (Exception ex)
            {
                _SErrorConexion = ex.Message;
                return false;
            }
        }

    //    public DataTable fillDataTable(string table)
    //{
    //    string query = "SELECT * FROM dstut.dbo." +table;

    //    SqlConnection sqlConn = new SqlConnection(conSTR);
    //    sqlConn.Open();
    //    SqlCommand cmd = new SqlCommand(query, sqlConn);

    //    DataTable dt = new DataTable();
    //    dt.Load(cmd.ExecuteReader());
    //    sqlConn.Close();
    //    return dt;
    









    }
}
