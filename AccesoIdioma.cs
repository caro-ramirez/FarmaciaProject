using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AccesoIdioma
    {

        private SqlConnection cn;
        private SqlTransaction tx;
        public void Abrir()
        {
            cn = new SqlConnection("Data Source=DESKTOP-GP7K86O\\SQLEXPRESS;Initial Catalog=Farmacia;Integrated Security=True");
            //cn = new SqlConnection("initial catalog =TDCampo; data source=DESKTOP-GP7K86O\\SQLEXPRESS; integrated security = sspi");
            cn.Open();

            //DESKTOP-GP7K86O\SQLEXPRESS

        }
        public void Cerrar()
        {
            cn.Close();
            cn = null;
            GC.Collect();
        }

        public void IniciarTX()
        {
            tx = cn.BeginTransaction();

        }


        public void ConfirmarTx()
        {
            tx.Commit();
            tx = null;

        }
        public void RollbackTx()
        {
            tx.Rollback();
            tx = null;
        }


        private SqlCommand CrearComando(string sp, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand(sp, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parametros != null)
            {
                cmd.Parameters.AddRange(parametros.ToArray());
            }
            if (tx != null)
            {
                cmd.Transaction = tx;

            }
            return cmd;

        }

        public DataTable Leer(string sp, List<SqlParameter> parametros = null)
        {
            Abrir();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = CrearComando(sp, parametros);
            DataTable tabla = new DataTable();
            da.Fill(tabla);
            da = null;
            Cerrar();
            return tabla;

        }

        public int Escribir(string sp, List<SqlParameter> parametros = null)
        {
            Abrir();
            SqlCommand cmd = CrearComando(sp, parametros);
            int res;
            try
            {
                res = cmd.ExecuteNonQuery();
            }
            catch
            {
                res = -1;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            Cerrar();
            return res;

        }

        public int LeerEscalar(string sp, List<SqlParameter> parametros = null)
        {
            int resultado;
            SqlCommand cmd = CrearComando(sp, parametros);
            try
            {
                resultado = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch
            { resultado = -1; }
            return resultado;

        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.String;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.Int32;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, float valor)
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.Single;
            return parametro;
        }


        private string conexionstring;
        public AccesoIdioma()
        {
            conexionstring = "Data Source=DESKTOP-GP7K86O\\SQLEXPRESS;Initial Catalog=Farmacia;Integrated Security=True";
        }
        public SqlConnection Conectarse()
        {
            return new SqlConnection(conexionstring);
        }

    }
}
