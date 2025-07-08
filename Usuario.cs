using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace BLL   
{
    public class bUsuario
    {

        public int CambiarClave(Usuario usu)
        {

            int res = 0;

            DAL.MP_USUARIO mp_usu = new DAL.MP_USUARIO();

            res = mp_usu.CambiarClave(usu);

            return res;
        }
    }
}
