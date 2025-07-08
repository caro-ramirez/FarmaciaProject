using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.Data;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class MP_LOGIN : MAPPER<Services.Usuario>
    {
        Acceso acceso = new Acceso();


        public override int Borrar(Usuario obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@username", obj.username));

            return acceso.Escribir("", parametros);
        }

        public override int Editar(Usuario obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@username", obj.username));

            return acceso.Escribir("DESBLOQUEAR", parametros);
        }

        public override int Insertar(Usuario obj)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@password", obj.password));

            return acceso.Escribir("REGISTRAR", parametros);
        }

        public int LogUser(Usuario obj) { 
        
        List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@userpass", obj.password));

            return acceso.Escribir("LOGUEAR", parametros);

        }
        public List<Usuario> CheckBloq()
        {

            List<Usuario> L_CheckBloq = new List<Usuario>();


            DataTable tabla = acceso.Leer("LISTAR_USUARIOS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //DNI, NOMBRE, APELLIDO, FECHA, TELEFONO, USERNAME, PASSWORD, INTENTOS, BLOQUEADO

                Usuario usu = new Usuario();


                usu.username = registro["username"].ToString();
                usu.intentos = int.Parse(registro["intentos"].ToString());
                usu.bloqueado = int.Parse(registro["bloqueado"].ToString());

                L_CheckBloq.Add(usu);

            }
            return L_CheckBloq;
        }

        public List<Usuario> CheckPass()
        {

            List<Usuario> L_CheckPass = new List<Usuario>();


            DataTable tabla = acceso.Leer("LISTAR_USUARIOS", null); 

            foreach (DataRow registro in tabla.Rows)
            {
                //DNI, NOMBRE, APELLIDO, FECHA, TELEFONO, USERNAME, PASSWORD, INTENTOS, BLOQUEADO

                Usuario usu = new Usuario();


                usu.username = registro["username"].ToString();
                usu.password = registro["userpass"].ToString();

                L_CheckPass.Add(usu);

            }
            return L_CheckPass;
        }

        public void BloquearUsuario(Usuario obj) {

             
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", obj.username));
            

                acceso.Escribir("BLOQUEAR", parametros);
           
        }
        public int CambiarClave(Usuario obj)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@username", obj.password));


            return acceso.Escribir("COMPROBARCLAVE", parametros);
        }

        public void SumaIntentos(Usuario obj, int intentos)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@intentos", intentos));


            acceso.Escribir("SUMA_INTENTOS", parametros);
        }



            public List<Services.Usuario> Listar() {

            List<Services.Usuario> usuarios = new List<Usuario>();
            

            DataTable tabla = acceso.Leer("LOGUEAR", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows) { 
            
            Services.Usuario usuario = new Services.Usuario();

                usuario.username = registro["username"].ToString();
                usuario.password = registro["password"].ToString(); 
                
                usuarios.Add(usuario);

            }
            
            
            return usuarios;
        

        }

        public int CheckRol(Usuario userLogueado)
        {

            List<Usuario> L_CheckRol = new List<Usuario>();
           
            int Rol = -1;

            DataTable tabla = acceso.Leer("LISTAR_USUARIOS", null);

            foreach (DataRow registro in tabla.Rows)
            {
                //DNI, NOMBRE, 

                Usuario usu = new Usuario();


                usu.username = registro["username"].ToString();
                usu.rolUser = int.Parse(registro["rol"].ToString());

                Idioma idi = new Idioma();
                idi.NombreIdioma = registro["Idioma"].ToString();
                usu.idioma = idi;


                L_CheckRol.Add(usu);

            }

            foreach (Usuario usu in L_CheckRol) {

                if (userLogueado.username == usu.username) {
                    Rol = usu.rolUser;
                }
            }

            return Rol;
        }

        public Idioma CheckIdioma(Usuario userLogueado)
        {

            List<Usuario> L_CheckIdioma = new List<Usuario>();
            Idioma idioma = new Idioma();
            DataTable tabla = acceso.Leer("LISTAR_USUARIOS", null);

            foreach (DataRow registro in tabla.Rows)
            {
                //DNI, NOMBRE, 

                Usuario usu = new Usuario();
                Idioma idi = new Idioma();


                usu.username = registro["username"].ToString();
                idi.NombreIdioma = registro["Idioma"].ToString();
                usu.idioma = idi;


                L_CheckIdioma.Add(usu);

            }

            foreach (Usuario usu in L_CheckIdioma)
            {

                if (userLogueado.username == usu.username)
                {
                    idioma = usu.idioma;
                }
            }

            return idioma;
        }
    }
}
