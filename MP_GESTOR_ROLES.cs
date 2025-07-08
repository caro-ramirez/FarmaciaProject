using BE;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_GESTOR_ROLES : MAPPER<BE.Directorio>
    {

        Acceso acceso = new Acceso();
        public override int Borrar(Directorio obj)
        {
            throw new NotImplementedException();
        }

        public override int Editar(Directorio obj)
        {
            throw new NotImplementedException();
        }

        public override int Insertar(Directorio obj)
        {
            //ALTA ROL 


            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@ID_ROL", obj.IDRol));
            parametros.Add(acceso.CrearParametro("NombreRol", obj.NombrePermiso));



            return acceso.Escribir("REGISTRAR_ROL", parametros);
        }


        public List<BE.Directorio> ListarRoles()
        {

            List<BE.Directorio> Roles = new List<BE.Directorio>();


            DataTable tabla = acceso.Leer("LISTAR_ROLES", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {


                BE.Directorio rol = new BE.Directorio(registro["NombreRol"].ToString(), int.Parse(registro["ID_ROL"].ToString()));

                // rol.AgregarHijo(rol);

                Roles.Add(rol);

            }

            return Roles;
        }

        public List<String> ListarAccesos(int rol){

            List<String> Accesos = new List<String>();
                
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@ID_ROL", rol));


            DataTable tabla = acceso.Leer("LISTAR_ACCESOS", parametros); //  "" -> proc almacenado


            foreach (DataRow registro in tabla.Rows)
            {

                Accesos.Add(registro["ID_PERMISO"].ToString());

               // BE_H.Permiso permiso = new BE_H.Permiso(registro["NombreRol"].ToString(), int.Parse(registro["ID_ROL"].ToString()));

                // rol.AgregarHijo(rol);

                //permisos.Add(permiso);

            }

            return Accesos;
        }

        public List<Usuario> ListarUsuariosYRoles() {

            List<Usuario> UsuariosRoles = new List<Usuario>();


            DataTable tabla = acceso.Leer("LISTAR_USUARIOS_ROLES", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {


                Usuario user = new Usuario();

                user.username = registro["username"].ToString();
                user.rolUser = int.Parse(registro["rol"].ToString());

                // rol.AgregarHijo(rol);

                UsuariosRoles.Add(user);

            }

            return UsuariosRoles;

        }

        public int EditarRolUser(Usuario obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@rol", obj.rolUser));
            


            return acceso.Escribir("EDITAR_ROL_USUARIO", parametros);
        }

        public List<Directorio> ListarRolPermiso()
        {

            List<Directorio> Roles = new List<Directorio>();
            List<Permiso> Permisos = new List<Permiso>();
            List<Directorio> RolPermiso = new List<Directorio>();

            DataTable tabla = acceso.Leer("LISTAR_ROL_PERMISOS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                
                Directorio dir = new Directorio(registro["NombreRol"].ToString(), int.Parse(registro["ID_ROL"].ToString()));




                // rol.AgregarHijo(rol);

                Roles.Add(dir);

            }

            foreach (DataRow registro in tabla.Rows)
            {

                Permiso per = new Permiso(registro["NombrePermiso"].ToString(), int.Parse(registro["ID_PERMISO"].ToString()));




                // rol.AgregarHijo(rol);

                Permisos.Add(per);

            }

            foreach (DataRow registro in tabla.Rows) {

                int idRol = int.Parse(registro["ID_ROL"].ToString());
                int idPermiso = int.Parse(registro["ID_PERMISO"].ToString());

                Directorio rol = Roles.FirstOrDefault(r => r.IDRol == idRol);
                Permiso permiso = Permisos.FirstOrDefault(p => p.IDRol == idPermiso);

                if (rol != null && permiso != null)
                {
                    rol.AgregarHijo(permiso); // Agregar el permiso al rol
                    RolPermiso.Add(rol);
                }
            }



            return RolPermiso;

        }
    }
}
