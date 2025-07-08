using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_GESTOR_PERMISOS : MAPPER<BE.Permiso>
    {
        Acceso acceso = new Acceso();

        public override int Borrar(Permiso obj)
        {
            throw new NotImplementedException();
        }

        public override int Editar(Permiso obj)
        {
            throw new NotImplementedException();
        }

        public override int Insertar(Permiso obj)
        {
            //ALTA PERMISO 


            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@ID_PERMISO", obj.IDRol));
            parametros.Add(acceso.CrearParametro("NombrePermiso", obj.NombrePermiso));



            return acceso.Escribir("REGISTRAR_PERMISO", parametros);
        }

        public List<BE.Permiso> ListarPermisos()
        {

            List<BE.Permiso> Roles = new List<BE.Permiso>();


            DataTable tabla = acceso.Leer("LISTAR_PERMISOS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {


                BE.Permiso permiso = new BE.Permiso(registro["NombrePermiso"].ToString(), int.Parse(registro["ID_PERMISO"].ToString()));

                // rol.AgregarHijo(rol);

                Roles.Add(permiso);

            }

            return Roles;
        }

        public int InsertarPermisoNuevo(int rol, int permiso)
        {
            //Agregar permiso a rol


            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@ID_ROL", rol));
            parametros.Add(acceso.CrearParametro("@ID_PERMISO", permiso));



            return acceso.Escribir("AGREGAR_PERMISO", parametros);
        }
    }
}
