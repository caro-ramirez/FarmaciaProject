using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Empleado
    {
        public List<BE.Venta> ListarVentas()
        {

            DAL.MP_VENTAS mp = new DAL.MP_VENTAS();

            return mp.ListarVentas();

        }
        public List<BE.Venta> ListarVentasHorario(DateTime horario, DateTime horario2)
        {

            DAL.MP_VENTAS mp = new DAL.MP_VENTAS();

            return mp.ListarVentasHorario(horario, horario2);

        }
    }
}
