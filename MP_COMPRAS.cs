using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_COMPRAS : MAPPER<BE.Compra>
    {
        Acceso acceso = new Acceso();
        public override int Borrar(Compra obj)
        {
            throw new NotImplementedException();
        }

        public override int Editar(Compra obj)
        {
            throw new NotImplementedException();
        }

        public override int Insertar(Compra obj)
        {
            //Registrar Pago Factura

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@codproveedor", obj.codproveedor));
            parametros.Add(acceso.CrearParametro("@nombreproveedor", obj.nombreproveedor));
            parametros.Add(acceso.CrearParametro("@totalventa", obj.totalventa));
            parametros.Add(acceso.CrearParametro("@totalproductos", obj.totalproductos));
            parametros.Add(acceso.CrearParametro("@horario", (obj.horario).ToString()));



            return acceso.Escribir("REGISTRAR_COMPRA", parametros);
        }
    }
}
