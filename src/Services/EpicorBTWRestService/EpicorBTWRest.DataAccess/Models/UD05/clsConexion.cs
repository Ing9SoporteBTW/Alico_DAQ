

using System.Data;
using System.Data.SqlClient;


namespace EpicorBTWRest.DataAccess.UD05
{
    public class clsConexion
    {

        #region"Constructor"
        public clsConexion()
        {
            strNombreTabla = "Tabla";
            blnHayCnx = false;
            objConexionBD = new SqlConnection();
            objCommand = new SqlCommand();
            objAdapter = new SqlDataAdapter();
            objDataset = new DataSet();
            strConnection = string.Empty;
            strVrUnico = string.Empty;
        }
        #endregion

        #region"Atributos"
        private object _EntryForm;
        private string strCompany;
        private string strCnx;
        private string strNombreTabla;
        public string strConnection { get; set; }
        private string strVrUnico;//Almacena el valor que devuelva el exeute_Scalar().
        public string strSQL { get; set; }//Almacenara la sentencia SQL.
        private string strRutaXML;//Almacena la ruta donde se encuentra la cadena de conexion del XML.
        private string strError;
        private bool blnHayCnx;//Permitira identificar si hay o no conexion en la Bd.

        private SqlConnection objConexionBD;//Para establecer la conexion con el servidor y la Bd.
        private SqlCommand objCommand;//El comando a ejecutar Text o procedure.
        private SqlDataReader objReader;//Es el contenedor de informacion en el modo conectado.
        private SqlDataAdapter objAdapter;//El intermediario entre el Dataset y la Bd.
        private DataSet objDataset;
        private object[,] parametros; //Parametros del SP
        #endregion

        #region"Propiedades"
        public string ValorUnico
        { get { return strVrUnico; } }
        public string Error
        { get { return strError; } }
        public DataSet Midataset
        { get { return objDataset; } }
        public SqlDataReader Reader
        { get { return objReader; } }

        public object[,] ParmetrosSP
        {
            set { parametros = value; }
        }
        public string Company
        { set { strCompany = value; } }
        #endregion

        #region "Metodos Privados"
        private bool AbrirConexion()
        {
            try
            {
                objConexionBD.ConnectionString = strConnection;
                objConexionBD.Open();
                blnHayCnx = true;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                blnHayCnx = false;
                return false;
            }
        }
        #endregion

        #region "Consultas SQL"
        //Si la consulta me devuelve una tabla.
        public DataTable ConsultarDT(bool blnParametros)
        {
            DataTable dt = new DataTable();
            try //para controlar el error.
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No definio la instruccion SQL";
                    return null;
                }
                if (!blnHayCnx)
                {
                    if (!AbrirConexion())
                        return null;
                }
                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //Realizar la transaccion en la BD.
                objAdapter.SelectCommand = objCommand;
                objAdapter.Fill(dt);
                //objReader = objCommand.ExecuteReader();
                return dt;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
            finally
            {
                CerrarConexion();
            }
        }
        //Si la consulta me retorna un solo valor.
        public bool ConsultarValorUnico(bool blnParametros)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No se definio la instruccion SQL";
                    return false;
                }
                if (!blnHayCnx)
                {
                    if (!AbrirConexion())
                        return false;
                }
                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //Realizar la trasaccion en la base de datos.
                strVrUnico = objCommand.ExecuteScalar().ToString();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }
        //Si la consulta no me retorna nada.
        public bool EjecutarSentencia(bool blnParametros)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No se definio la instruccion SQL";
                    return false;
                }
                if (!blnHayCnx)
                {
                    if (!AbrirConexion())
                        return false;
                }
                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //Realizar la trasaccion en la base de datos.
                strVrUnico = objCommand.ExecuteNonQuery().ToString();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }
        #endregion

        #region"Consultas SQL SP"
        public DataTable SP_Q(bool blnParametros)
        {
            DataTable dt = new DataTable();
            try //para controlar el error.
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No definio la instruccion SQL";
                    return null;
                }
                if (!blnHayCnx)
                {
                    if (!AbrirConexion())
                        return dt;
                }
                if (blnParametros)
                {
                    //Parametros SP
                    for (int i = 0; i < parametros.GetLength(0); i++)
                    {
                        if (string.IsNullOrEmpty(parametros[i, 1].ToString()))
                            parametros[i, 1] = "";
                        SqlParameter pParametro = new SqlParameter(parametros[i, 0].ToString(), parametros[i, 1]);
                        objCommand.Parameters.Add(pParametro).Value = pParametro.Value;
                    }
                    objCommand.CommandType = CommandType.StoredProcedure;
                }
                else
                    objCommand.CommandType = CommandType.Text;
                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                //Realizar la transaccion en la BD.
                objAdapter.SelectCommand = objCommand;
                objAdapter.Fill(dt);
                //objReader = objCommand.ExecuteReader();
                return dt;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
            finally
            {
                CerrarConexion();
            }
        }
        public int SP_Scalar(bool blnParametros)
        {
            try //para controlar el error.
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No definio la instruccion SQL";
                    return -1;
                }
                if (!blnHayCnx)
                {
                    if (!AbrirConexion())
                        return -1;
                }
                if (blnParametros)
                {
                    //Parametros SP
                    for (int i = 0; i < parametros.GetLength(0); i++)
                    {
                        if (string.IsNullOrEmpty(parametros[i, 1].ToString()))
                            parametros[i, 1] = "";
                        SqlParameter pParametro = new SqlParameter(parametros[i, 0].ToString(), parametros[i, 1]);
                        objCommand.Parameters.Add(pParametro).Value = pParametro.Value;
                    }
                    objCommand.CommandType = CommandType.StoredProcedure;
                }
                else
                    objCommand.CommandType = CommandType.Text;
                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                //Realizar la transaccion en la BD.
                objAdapter.SelectCommand = objCommand;
                return Convert.ToInt32(objCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
            finally
            {
                CerrarConexion();
            }
        }
        public bool SP_NQ(bool blnParametros)
        {
            try //para controlar el error.
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No definio la instruccion SQL";
                    return false;
                }
                if (!blnHayCnx)
                {
                    if (!AbrirConexion())
                        return false;
                }
                if (blnParametros)
                {
                    //Parametros SP
                    for (int i = 0; i < parametros.GetLength(0); i++)
                    {
                        if (string.IsNullOrEmpty(parametros[i, 1].ToString()))
                            parametros[i, 1] = "";
                        SqlParameter pParametro = new SqlParameter(parametros[i, 0].ToString(), parametros[i, 1]);
                        objCommand.Parameters.Add(pParametro).Value = pParametro.Value;
                    }

                    objCommand.CommandType = CommandType.StoredProcedure;
                }
                else
                    objCommand.CommandType = CommandType.Text;
                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                //Realizar la transaccion en la BD.
                objAdapter.SelectCommand = objCommand;
                objCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }
        #endregion

        #region "Metodos Publicos"
        public void CerrarConexion()
        {
            try
            {
                objConexionBD.Close();
                objConexionBD = null;
            }
            catch (Exception ex)
            {
                strError = "No cerro o abrio la Conexion " + ex.Message;
            }
        }
        //LLenamos el data set. Medio desconectado.
        public bool LLenarDataSet(bool blnParametros)
        {
            try
            {

                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No se definio la instruccion SQL";
                    return false;
                }
                if (!blnHayCnx)
                {
                    if (!AbrirConexion())
                        return false;
                }
                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                {
                    //Parametros SP
                    for (int i = 0; i < parametros.GetLength(0); i++)
                    {
                        if (string.IsNullOrEmpty(parametros[i, 1].ToString()))
                            parametros[i, 1] = "";
                        SqlParameter pParametro = new SqlParameter(parametros[i, 0].ToString(), parametros[i, 1]);
                        objCommand.Parameters.Add(pParametro).Value = pParametro.Value;
                    }

                    objCommand.CommandType = CommandType.StoredProcedure;
                }
                else
                    objCommand.CommandType = CommandType.Text;

                //El DataAdapter utiliza el Command para la transaccion.
                objAdapter.SelectCommand = objCommand;

                //Realizar la transaccion en la BD y el llenado del Dataset/DataTable
                objAdapter.Fill(objDataset, strNombreTabla);
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion
    }
}
