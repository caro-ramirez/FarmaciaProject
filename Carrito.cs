using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Carrito
    {
        public static List<BE.Carrito> Carritos = new List<BE.Carrito>();
        public static List<BE.Carrito> CarritosProveedor = new List<BE.Carrito>();


        public static List<BE.Carrito> ObtenerCarritos()
        {
            return Carritos;
        }
        public static List<BE.Carrito> ObtenerCarritosProveedor()
        {
            return CarritosProveedor;
        }
        public Carrito()
        {
            productos = new List<Producto>();
        }
        private List<Producto> productos;

        public List<Producto> ObtenerProductos()
        {
            return productos;
        }
        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
        }

        public void ConfeccionarPedido(Cliente _cliente) {

            BE.Carrito carrito = new BE .Carrito();

            carrito.cliente = _cliente;
            carrito.productosCliente = productos;

            Carritos.Add(carrito);

            productos = null;
        }

        public void ConfeccionarPedido(Proveedor _cliente) // Cotizaciones Proveedor
        {

            BE.Carrito carrito = new BE.Carrito();

            carrito.proveedor = _cliente;
            carrito.productosCliente = productos;

            CarritosProveedor.Add(carrito);

            productos = null;
        }
    }
}
