using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL   
{
    public class DaoIdioma : AccesoIdioma
    {
        public bool CargarIdioma(Idioma idioma)
        {
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@idioma", idioma.NombreIdioma);
                    comando.CommandText = "select * from Idioma where Idioma=@idioma COLLATE SQL_Latin1_General_CP1_CS_AS";
                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return false;
                    }
                    else
                    {
                        reader.Close();
                        comando.Parameters.AddWithValue("@Nombre", idioma.NombreIdioma);
                        comando.Parameters.AddWithValue("@Desc", idioma.Descripcion);
                        comando.CommandText = "insert into Idioma values(@Nombre,@Desc)";
                        comando.ExecuteNonQuery();

                        var comando2 = new SqlCommand("InsertarIdiomaControl", conexion);
                        comando2.CommandType = CommandType.StoredProcedure;
                        comando2.Parameters.AddWithValue("@idioma", idioma.NombreIdioma);
                        comando2.ExecuteNonQuery();
                        return true;
                    }
                }
            }

        }

        public bool CargarIdiomaControl(IdiomaControl idiomaControl)
        {
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@idioma", idiomaControl.Idioma);
                    comando.Parameters.AddWithValue("@CodControl", idiomaControl.CodControl);
                    comando.Parameters.AddWithValue("@traduccion", idiomaControl.Traduccion);
                    comando.CommandText = "insert into IdiomaControl values (@idioma,@CodControl,@traduccion)";
                    comando.CommandType = CommandType.Text;
                    comando.ExecuteNonQuery();
                    return true;
                }
            }

        }

        public List<string> DevolverIdiomas()
        {
            List<string> nombreidiomas = new List<string>();
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "select Idioma from Idioma";
                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        nombreidiomas.Add(reader.GetString(0));
                    }
                    return nombreidiomas;

                }
            }
        }

        public List<IdiomaControl> RetornarTraducciones(string idioma)
        {
            List<IdiomaControl> traducciones = new List<IdiomaControl>();
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@idioma", idioma);
                    comando.CommandText = $"select * from IdiomaControl where CodIdioma =@idioma";
                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        IdiomaControl idiomaControl = new IdiomaControl();
                        idiomaControl.Idioma = reader.GetString(0);
                        idiomaControl.CodControl = reader.GetString(1);
                        idiomaControl.Traduccion = reader.GetString(2);
                        traducciones.Add(idiomaControl);
                    }
                    return traducciones;
                }
            }
        }

        public List<ControlUsuario> RetornarControles(string formulario)
        {
            List<ControlUsuario> tagControles = new List<ControlUsuario>();
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@formulario", formulario);
                    comando.CommandText = $"select * from Control where Descripcion like '{formulario}%'";
                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ControlUsuario aux = new ControlUsuario();
                        aux.CodControl = reader.GetString(0);
                        aux.Descripcion = reader.GetString(1);
                        tagControles.Add(aux);
                    }
                    return tagControles;
                }
            }
        }

        public DataTable RetornarTablaIdioma(string idioma)
        {
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@idioma", idioma);
                    comando.CommandText = $"select Control.CodControl,Descripcion,CodIdioma,Traduccion from Control,IdiomaControl where Control.CodControl= IdiomaControl.CodControl and IdiomaControl.CodIdioma=@idioma";
                    SqlDataAdapter sqlDA = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    sqlDA.Fill(tabla);
                    return tabla;
                }
            }
        }

        public void EditarTraduccion(string codControl, string traduccion, string idioma)
        {
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@CodControl", codControl);
                    comando.Parameters.AddWithValue("@Traduccion", traduccion);
                    comando.Parameters.AddWithValue("@Idioma", idioma);
                    comando.CommandText = $"update IdiomaControl set Traduccion=@Traduccion where CodControl=@CodControl and CodIdioma=@idioma ";
                    SqlDataAdapter sqlDA = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    sqlDA.Fill(tabla);
                }
            }
        }

        public DataTable RetornarIdiomas()
        {
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = $"select * from Idioma";
                    SqlDataAdapter sqlDA = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    sqlDA.Fill(tabla);
                    return tabla;
                }
            }
        }

        public void EditarIdioma(string idioma, string nuevonombre)
        {
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    string descripcion = "El idioma cargado es: " + nuevonombre;
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@idioma", idioma);
                    comando.Parameters.AddWithValue("@nuevonombre", nuevonombre);
                    comando.CommandText = $"update Idioma set Idioma=@nuevonombre where Idioma=@idioma";
                    comando.CommandType = CommandType.Text;
                    comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@descripcion", descripcion);
                    comando.CommandText = $"update Idioma set Descripcion=@descripcion where Idioma=@nuevonombre";
                    comando.CommandType = CommandType.Text;
                    comando.ExecuteNonQuery();

                }
            }
        }

        public void BorrarIdioma(string idioma)
        {
            using (var conexion = Conectarse())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.Parameters.AddWithValue("@idioma", idioma);
                    comando.CommandText = $"delete from Idioma where Idioma.Idioma=@idioma";
                    comando.CommandType = CommandType.Text;
                    comando.ExecuteNonQuery();
                    comando.ExecuteNonQuery();

                }
            }
        }



    }
}
