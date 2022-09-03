using System.Data;
using AdminDepot.Modelos;
using AccesoDatos;
using System.Collections.Generic;
using System;
using System.Data.Common;

namespace AdminDepot.ModelosDTOs
{
    internal class ServicioProvincia : Provicia
    {
        private const string SQL_SEL_PROVINCIAS = "SELECT id, Nombre, CAST(Superficie AS DECIMAL(18,2)) Superficie, Capital FROM Provincias";
        private const string SQL_INS_PROVINCIAS = "INSERT INTO Provincias (Nombre, Superficie, Capital) VALUES (@Nombre, @Superficie, @Capital)";
        private const string SQL_UP_PROVINCIAS = "UPDATE Provincias SET Nombre = @Nombre, Superficie = @Superficie, Capital = @Capital" +
            " WHERE Id = @Id ";
        private const string SQL_DEL_PROVINCIAS = "DELETE FROM Provincias WHERE Id = @Id";
        private readonly string conexion;

        public ServicioProvincia(string conexion)
        {
            this.conexion = conexion;
        }

        public DataSet DarProvincias()
        {
            DataSet ds = new DataSet();
            ConexionSQL conexionSQL = new ConexionSQL(conexion);
            ds = conexionSQL.EjecutarConsultaDataSet(SQL_SEL_PROVINCIAS);
            return ds;
        }
        public List<Provicia> ObtenerProvincias()
        {
            ConexionSQL conexionSQL = new ConexionSQL(conexion);
            IDataReader dataReader =  conexionSQL.EjecutarConsultaDataReader(SQL_SEL_PROVINCIAS);
            List<Provicia> provincias = new List<Provicia>();
            while (dataReader.Read())
            {
                Provicia provicia = new Provicia
                {
                    Id = int.Parse(dataReader["id"].ToString()),
                    Nombre = dataReader["Nombre"].ToString(),
                    Superficie = decimal.Parse(dataReader["Superficie"].ToString()),
                    Capital = dataReader["Capital"].ToString()
                };
                provincias.Add(provicia);
            }
            return provincias;
        }
        public int GrabarProvincia()
        {
            ConexionSQL conexionSQL = new ConexionSQL(conexion);
            string sql = SQL_INS_PROVINCIAS;
            int numQuery = conexionSQL.EjecutarQuery(RemplazarValoresSql(sql));
            return numQuery;
        }
        public int ActualizarProvincia()
        {
            ConexionSQL conexionSQL = new ConexionSQL(conexion);
            string sql = SQL_UP_PROVINCIAS;
            int numQuery = 0;
            var sqlCom = conexionSQL.NewSqlCommand;
            sqlCom.CommandText = sql;
            sqlCom.CommandType = CommandType.Text;
            if (sql.Contains("@Id"))
            {
                var sqlParam = conexionSQL.NewSqlParameter;
                sqlParam.ParameterName = "@Id";
                sqlParam.Value = Id;
                sqlCom.Parameters.Add(sqlParam);
            }
            if (sql.Contains("@Nombre"))
            {
                var sqlParam = conexionSQL.NewSqlParameter;
                sqlParam.ParameterName = "@Nombre";
                sqlParam.Value = Nombre;
                sqlCom.Parameters.Add(sqlParam);
            }
            if (sql.Contains("@Superficie"))
            {
                var sqlParam = conexionSQL.NewSqlParameter;
                sqlParam.ParameterName = "@Superficie";
                sqlParam.Value = Superficie;
                sqlCom.Parameters.Add(sqlParam);
            }
            if (sql.Contains("@Capital"))
            {
                var sqlParam = conexionSQL.NewSqlParameter;
                sqlParam.ParameterName = "@Capital";
                sqlParam.Value = Capital;
                sqlCom.Parameters.Add(sqlParam);
            }
            numQuery = conexionSQL.EjecutarQueryCommand(sqlCom);
            return numQuery;
        }

        private string RemplazarValoresSql(string sql)
        {
            if (!this.Id.ToString().Trim().Equals(""))
                sql = sql.Replace("@Id", this.Id.ToString());
            if (!this.Nombre.ToString().Trim().Equals(""))
                sql = sql.Replace("@Nombre", "'" + this.Nombre + "'");
            if (!this.Superficie.ToString().Trim().Equals(""))
                sql = sql.Replace("@Superficie", DecimalConPunto(this.Superficie.ToString()));
            if (!this.Capital.ToString().Trim().Equals(""))
                sql = sql.Replace("@Capital", "'" + this.Capital + "'");
            return sql;
        }
        private string DecimalConPunto(string valor)
        {
            string datoSql = valor;
            datoSql = datoSql.Replace(",", ".");
            return datoSql;
        }

        internal int EliminarProvincia()
        {
            ConexionSQL conexionSQL = new ConexionSQL(conexion);
            string sql = SQL_DEL_PROVINCIAS;
            int numQuery = conexionSQL.EjecutarQuery(RemplazarValoresSql(sql));
            return numQuery;
        }
    }
}
