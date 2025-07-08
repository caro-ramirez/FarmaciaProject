using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Services;

namespace BLL
{
    public class Login
    {
        DAL.MP_BITACORA mp_bitacora = new DAL.MP_BITACORA();
        BE.BITACORA bitacora = new BE.BITACORA();
       
        public List<Services.Usuario> Listar() { 
        
        DAL.MP_LOGIN mp = new DAL.MP_LOGIN();

            return mp.Listar(); 

        }

        public int Loguear(Services.Usuario usuario) {


            DAL.MP_LOGIN mp = new DAL.MP_LOGIN();

            int res = mp.LogUser(usuario);

            if (res > 0)
            {
                bitacora.usuario = usuario.username;
                bitacora.accion = " Inicio de Sesion";
                bitacora.horario = DateTime.Now;

                mp_bitacora.Insertar(bitacora);

                return 1; // usuario logueado

            }
            else {
                bitacora.usuario = usuario.username;
                bitacora.accion = " Credenciales Incorrectas";
                bitacora.horario = DateTime.Now;

                mp_bitacora.Insertar(bitacora);

                return 0; // usuario no logueado
            }
        }

        public int L_CheckBloq(Usuario usu, int intentos)
        {
            List<Usuario> usus = new List<Usuario>();

            DAL.MP_BITACORA mp_bitacora = new DAL.MP_BITACORA();
            DAL.MP_LOGIN mp = new DAL.MP_LOGIN();
            BE.BITACORA bitacora = new BITACORA(); 

            usus = mp.CheckBloq();

            int bloq = 0;

            foreach (Usuario u in usus)
            {

                if (u.username == usu.username)
                {
                    if (u.bloqueado == 1)
                    {
                        bloq = 1;
                    }
                    else if (u.bloqueado == 0)
                    {
                        bloq = 0;
                    }
                }
                else if (intentos > 0 && intentos < 3)
                {
                    mp.SumaIntentos(usu, intentos);
                }
                else if (intentos == 3) {
                    mp.SumaIntentos(usu, intentos);
                    mp.BloquearUsuario(usu);
                    bloq = 1;  
                }
            }

            if (bloq == 1) {

                bitacora.usuario = usu.username;
                bitacora.accion = "Usuario bloqueado";
                bitacora.horario = DateTime.Now;

                mp_bitacora.Insertar(bitacora);
            }

            return bloq;

        }

        public int L_CheckPass(Usuario usu, string claveActual)
        {
            List<Usuario> usus = new List<Usuario>();

            DAL.MP_LOGIN mp = new DAL.MP_LOGIN();

            usus = mp.CheckPass();

            int pass = 0;

            foreach (Usuario u in usus)
            {

                if (u.username == usu.username)
                {
                    if (u.password == claveActual)
                    {
                        pass = 1;
                    }
                    else if (u.password != claveActual)
                    {
                        pass = 0;
                    }
                }
            }

            return pass;

        }

        public int AsignarRol(Usuario usuario)
        {

            DAL.MP_LOGIN mp = new DAL.MP_LOGIN();

            return mp.CheckRol(usuario);

        }

        public Idioma AsignarIdiomal(Usuario usuario)
        {
            Idioma idio = new Idioma();

            DAL.MP_LOGIN mp = new DAL.MP_LOGIN();

            idio = mp.CheckIdioma(usuario);

            return idio;
        }

        public void cierroSesion(Usuario usu) {

            DAL.MP_BITACORA mp_bitacora = new DAL.MP_BITACORA();
            BE.BITACORA bitacora = new BITACORA();

            bitacora.usuario = usu.username;
            bitacora.accion = "Cerró la sesión";
            bitacora.horario = DateTime.Now;

            mp_bitacora.Insertar(bitacora);
        }
    }
}
