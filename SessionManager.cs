using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Services
{
    public class SessionManager
    {

        private static SessionManager _session;
        private static object _lock = new Object();

        public Usuario Usuario { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Rol { get; set; }
        private Idioma Idioma;

        public Idioma idioma { get => Idioma; set => Idioma = value; }

        public static SessionManager GetInstance
        {
            get
            {
                if (_session == null)
                {
                    _session = null;

                    return _session;
                }
                else
                {
                    return _session;
                }
                // throw new Exception("Session no iniciada");
            }
        }

        public static void Login(Usuario usuario)
        {
            lock (_lock)
            {
                if (_session == null)
                {
                    _session = new SessionManager();
                    _session.Usuario = usuario;
                    _session.FechaInicio = DateTime.Now;
                    _session.Rol = usuario.rolUser;
                    _session.Idioma = usuario.idioma;
                }
                else if (_session.Usuario != null)
                {
                    throw new Exception("Sesion ya iniciada");
                }
            }
        }

        public static void Logout()
        {
            lock (_lock)
            {
                if (_session != null)
                {
                    _session = null;
                }
                else
                {
                    throw new Exception("Session no iniciada");
                }
            }
        }

        private SessionManager()
        {



        }

    }
}
