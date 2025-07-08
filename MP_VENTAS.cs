using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_VENTAS : MAPPER<Venta>
    {
        Acceso acceso = new Acceso();
        public override int Borrar(Venta obj)
        {
            throw new NotImplementedException();
        }

        public override int Editar(Venta obj)
        {
            throw new NotImplementedException();
        }

        public override int Insertar(Venta obj)
        {
            //Registrar Venta

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@dnicliente", obj.dnicliente));
            parametros.Add(acceso.CrearParametro("@nombrecliente", obj.nombrecliente));
            parametros.Add(acceso.CrearParametro("@totalventa", obj.totalventa));
            parametros.Add(acceso.CrearParametro("@totalproductos", obj.totalproductos));
            parametros.Add(acceso.CrearParametro("@horario", (obj.horario).ToString()));



            return acceso.Escribir("REGISTRAR_VENTA", parametros);
        }

        public List<Venta> ListarVentas()
        {

            List<Venta> ListaVentas = new List<Venta>();


            DataTable tabla = acceso.Leer("LISTAR_VENTAS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //COD_PRODUCTO, P_NOMBRE, PRECIO, CATEGORIA

                Venta venta = new Venta();

                venta.dnicliente = int.Parse(registro["dnicliente"].ToString());
                venta.nombrecliente = registro["nombrecliente"].ToString();
                venta.totalventa = int.Parse(registro["totalventa"].ToString());
                venta.totalproductos = int.Parse(registro["totalproductos"].ToString());
                venta.horario = DateTime.Parse(registro["horario"].ToString());


                ListaVentas.Add(venta);

            }


            return ListaVentas;


        }

        public List<Venta> ListarVentasHorario(DateTime obj, DateTime obj2)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@horario", obj.ToShortDateString()));
            parametros.Add(acceso.CrearParametro("@horario2", obj2.ToShortDateString()));

            List<Venta> ListaVentas = new List<Venta>();

            DataTable tabla = acceso.Leer("LISTAR_VENTAS_HORARIO", parametros); 

            foreach (DataRow registro in tabla.Rows)
            {

                Venta venta = new Venta();

                venta.dnicliente = int.Parse(registro["dnicliente"].ToString());
                venta.nombrecliente = registro["nombrecliente"].ToString();
                venta.totalventa = int.Parse(registro["totalventa"].ToString());
                venta.totalproductos = int.Parse(registro["totalproductos"].ToString());
                venta.horario = DateTime.Parse(registro["horario"].ToString());


                ListaVentas.Add(venta);

            }


            return ListaVentas;


        }
    }

}
