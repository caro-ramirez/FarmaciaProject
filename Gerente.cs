using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Gerente : Empleado
    {
        public void PagarFactura(Compra compra)
        {

            MP_COMPRAS mp = new MP_COMPRAS();

            mp.Insertar(compra);
        }

    }
}
