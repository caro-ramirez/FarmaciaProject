using BE;
using Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_USUARIO : MAPPER<Services.Usuario>
    {
        Acceso acceso = new Acceso();

        public override int Borrar(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public override int Editar(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public override int Insertar(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public int CambiarClave(Usuario obj) {

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@userpass", obj.password));

            return acceso.Escribir("CAMBIAR_PASS", parametros);
        }
    }
}
