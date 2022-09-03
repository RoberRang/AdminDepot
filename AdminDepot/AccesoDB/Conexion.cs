using System.Data;
using System.Data.SqlClient;


namespace AccesoDatos
{
    public class ConexionSQL
    {
        private readonly string Conexion;
        private readonly SqlConnection sqlConnection;
        private SqlDataReader reader;
        public SqlDataReader Reader { get { return reader; } }

        public ConexionSQL(string conexion)
        {
            Conexion = conexion;
            this.sqlConnection = new SqlConnection(Conexion);
        }

        public DataSet EjecutarConsultaDataSet(string consulta)
        {
            DataSet dsConsulta = null;
            try
            {
                sqlConnection.Open();
                dsConsulta = new DataSet("Consulta");
                 
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.Connection = sqlConnection;
                sqlDataAdapter.SelectCommand.CommandText = consulta;
                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                sqlDataAdapter.Fill(dsConsulta);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();                
            }
            return dsConsulta;
        }
        public IDataReader EjecutarConsultaDataReader(string consulta)
        {
            try
            {
                reader = null;
                if (sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.Text,
                    CommandText = consulta
                };
                reader = sqlCommand.ExecuteReader();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return Reader;
        }
        public int EjecutarQuery(string consulta)
        {
            SqlCommand conexionCommand = null;
            int numQuery = 0;
            try
            {
                sqlConnection.Open();
                conexionCommand = new SqlCommand();
                conexionCommand.Connection = sqlConnection;
                conexionCommand.CommandText = consulta;
                conexionCommand.CommandType = CommandType.Text;
                numQuery = conexionCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
            return numQuery;
        }

    }
}