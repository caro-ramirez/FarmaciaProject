using BE;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_PRODUCTO : MAPPER<BE.Producto>
    {
        Acceso acceso = new Acceso();

        public override int Borrar(Producto obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@cod_producto", obj.cod_producto));

            return acceso.Escribir("ELIMINAR_PRODUCTO", parametros);
        }

        public override int Editar(Producto obj)
        {
            throw new NotImplementedException();
        }

        public override int Insertar(Producto obj)
        {
            //ALTA PRODUCTO

            obj.status = 1;

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@cod_producto", obj.cod_producto));
            parametros.Add(acceso.CrearParametro("@p_nombre", obj.p_nombre));
            parametros.Add(acceso.CrearParametro("@precio", obj.precio));
            parametros.Add(acceso.CrearParametro("@categoria", obj.categoria));
            parametros.Add(acceso.CrearParametro("@stock", obj.stock));
            parametros.Add(acceso.CrearParametro("@status", obj.status));
            parametros.Add(acceso.CrearParametro("@fecha", obj.fecha.ToString()));
            parametros.Add(acceso.CrearParametro("@usuario", obj.username));

            return acceso.Escribir("REGISTRAR_PRODUCTO", parametros);
        }

        public List<Producto> Listar()
        {

            List<Producto> ListaProductos = new List<Producto>();


            DataTable tabla = acceso.Leer("LISTAR_PRODUCTOS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //COD_PRODUCTO, P_NOMBRE, PRECIO, CATEGORIA

                Producto producto = new Producto();

                producto.cod_producto = int.Parse(registro["cod_producto"].ToString());
                producto.cod_cambio = int.Parse(registro["cod_cambio"].ToString());
                producto.p_nombre = registro["p_nombre"].ToString();
                producto.precio = int.Parse(registro["precio"].ToString());
                producto.categoria = registro["categoria"].ToString();
                producto.stock = int.Parse(registro["stock"].ToString());
                producto.status = int.Parse(registro["status"].ToString());
                producto.fecha = DateTime.Parse(registro["fecha"].ToString());
                producto.username = registro["usuario"].ToString();
                

                ListaProductos.Add(producto);

            }


            return ListaProductos;


        }

        public List<Producto> ListarProductosVentas()
        {

            List<Producto> ListaProductos = new List<Producto>();


            DataTable tabla = acceso.Leer("LISTAR_PRODUCTOS_VENTA", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //COD_PRODUCTO, P_NOMBRE, PRECIO, CATEGORIA

                Producto producto = new Producto();

                producto.cod_producto = int.Parse(registro["cod_producto"].ToString());
                producto.cod_cambio = int.Parse(registro["cod_cambio"].ToString());
                producto.p_nombre = registro["p_nombre"].ToString();
                producto.precio = int.Parse(registro["precio"].ToString());
                producto.categoria = registro["categoria"].ToString();
                producto.stock = int.Parse(registro["stock"].ToString());
                producto.status = int.Parse(registro["status"].ToString());


                ListaProductos.Add(producto);

            }


            return ListaProductos;


        }

        public int EditarProducto(Producto obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@cod_producto", obj.cod_producto));
            parametros.Add(acceso.CrearParametro("@p_nombre", obj.p_nombre));
            parametros.Add(acceso.CrearParametro("@precio", obj.precio));
            parametros.Add(acceso.CrearParametro("@categoria", obj.categoria));
            parametros.Add(acceso.CrearParametro("@usuario", obj.username));
            parametros.Add(acceso.CrearParametro("@fecha", obj.fecha.ToString()));
          

            return acceso.Escribir("MODIFICAR_PRODUCTO", parametros);
        }

        public int RestaurarEstado(Producto obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@cod_cambio", obj.cod_cambio));
            parametros.Add(acceso.CrearParametro("@cod_producto", obj.cod_producto));

            return acceso.Escribir("RESTAURAR_ESTADO_PRODUCTO", parametros);
        }
    }
}
