using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class MP_PERMISO : MAPPER<BE.Menu>
    {
        Acceso acceso = new Acceso();

        public Menu ObtenerPermisoPorId(int id)
        {
            Menu permiso = null;

            string sql = "OBTENER_PERMISO_ID";
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Id", id));

            
                DataTable resultado = acceso.Leer(sql, parametros);

                if (resultado.Rows.Count > 0)
                {
                    DataRow fila = resultado.Rows[0];
                    permiso = new Menu
                    {
                        Id = Convert.ToInt32(fila["id"]),
                        Nombre = fila["nombre"].ToString(),
                        // Agrega más propiedades según la estructura de tu tabla Permiso en la base de datos
                    };
                }

            return permiso;
        }

        public List<Rol> ObtenerPermisos()
        {
            List<Rol> permisos = new List<Rol>();

            string sql = "OBTENER_PERMISOS";

            
                DataTable resultado = acceso.Leer(sql);

                foreach (DataRow fila in resultado.Rows)
                {
                Rol permiso = new Rol
                {
                        Id = Convert.ToInt32(fila["id"]),
                        Nombre = fila["nombre"].ToString(),
                        // Agrega más propiedades según la estructura de tu tabla Permiso en la base de datos
                    };

                    permisos.Add(permiso);
                }
            

            return permisos;
        }

        public void AsignarMenuARol(int idRol, Menu menu)
        {
            // Lógica para asignar un menú a un rol en la base de datos
        }

        public override int Insertar(Menu obj)
        {
            throw new NotImplementedException();
        }

        public override int Editar(Menu obj)
        {
            throw new NotImplementedException();
        }

        public override int Borrar(Menu obj)
        {
            throw new NotImplementedException();
        }
    }
}
