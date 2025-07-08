using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Vendedor
    {
        public void GenerarVenta(Venta venta) { 
        
            MP_VENTAS mp = new MP_VENTAS();

            mp.Insertar(venta);
        }

    }
}
