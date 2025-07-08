using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    internal class Acceso
    {
            SqlConnection conexion;

            public void Abrir()
           {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    conexion = new SqlConnection("Data Source=DESKTOP-GH3C8I6\\SQLEXPRESS;Initial Catalog=Farmacia321;Integrated Security=True");
                   
                    //conexion.ConnectionString = @"Initial Catalog= Farmacia; Data source=.;  Integrated Security = SSPI";
                    //conexion.ConnectionString = conexion;

                    conexion.Open();

                //DESKTOP-GP7K86O\SQLEXPRESS
            }
        }

            public void Cerrar()
            {
                conexion.Close();
                conexion = null;
                GC.Collect();
            }

            private SqlCommand CrearComando(string sql, List<SqlParameter> parametros)
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;

                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;

                if (parametros != null && parametros.Count > 0)
                {
                    comando.Parameters.AddRange(parametros.ToArray());
                }
                return comando;
            }

            public BE.Farmaceutico Login(string sql, List<SqlParameter> parametros)
            {
            BE.Farmaceutico usuario = null;
                //sql = "select * from usuario where username=@usu and contraseña=@pass ";
                Abrir();

                SqlCommand comando = CrearComando(sql, parametros);
                SqlDataReader lector = comando.ExecuteReader();


                while (lector.Read())
                {
                    usuario = new BE.Farmaceutico();
                    usuario.dni = lector.GetInt32(0);
                    usuario.nombre = lector.GetString(1);
                    usuario.apellido = lector.GetString(2);
                    
                }

                lector.Close();
                comando.Parameters.Clear();
                comando.Dispose();
                Cerrar();

                GC.Collect();
                return usuario;
            }
            private SqlCommand EscribirComando(string sql, List<SqlParameter> parametros)
            {

                SqlCommand comando = new SqlCommand();

                comando.CommandText = sql;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Connection = conexion;


                if (parametros != null && parametros.Count > 0)
                {
                    comando.Parameters.AddRange(parametros.ToArray());
                }

                return comando;
            }
            public int Escribir(string nombre, List<SqlParameter> parametros = null)
            {
                Abrir();
                int filas = 0;

                SqlCommand comando = EscribirComando(nombre, parametros);

                try
                {
                    filas = comando.ExecuteNonQuery();
                }
                catch
                {
                    filas = -1;
                }

                comando.Parameters.Clear();
                comando.Dispose();
                comando = null;
                Cerrar();
                return filas;
            }

            public int EscribirRegistrar(string sql, List<SqlParameter> parametros)
            {
                int filasAfectadas = 0;

                Abrir();

                SqlCommand comando = CrearComando(sql, parametros);

                try
                {
                    filasAfectadas = comando.ExecuteNonQuery();
                }
                catch
                {
                    filasAfectadas = -1;
                }

                comando.Parameters.Clear();
                comando.Dispose();
                Cerrar();
                return filasAfectadas;
            }


            public int LeerEscalar(string sql)
            {
                int resultado;

                Abrir();

                SqlCommand comando = new SqlCommand();

                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                comando.CommandText = sql;
                //el scalar devuelve la primer columna del primer registro//
                resultado = int.Parse(comando.ExecuteScalar().ToString());

                Cerrar();

                return resultado;
            }

            public int LeerEscalarRegistro(string sql, List<SqlParameter> parametros = null)
            {
                Abrir();

                SqlCommand comando = CrearComando(sql, parametros);

                int res = int.Parse(comando.ExecuteScalar().ToString());

                comando.Parameters.Clear();
                comando.Dispose();

                Cerrar();

                return res;
            }
            public DataTable Leer(string nombre, List<SqlParameter> parametros = null)
            {
                DataTable tabla = new DataTable();
                Abrir();
                SqlDataAdapter adaptador = new SqlDataAdapter();

                adaptador.SelectCommand = EscribirComando(nombre, parametros);
                adaptador.Fill(tabla);
                adaptador.Dispose();
                Cerrar();
                return tabla;
            }

            public SqlParameter CrearParametro(string nombre, int valor)
            {
                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = nombre;
                parametro.Value = valor;
                parametro.DbType = DbType.Int32;

                return parametro;

            }

            public SqlParameter CrearParametro(string nombre, string valor)
            {

                SqlParameter parametro = new SqlParameter();
                parametro.DbType = DbType.String;
                parametro.ParameterName = nombre;
                parametro.Value = valor;


                return parametro;

            }

        }

    }

