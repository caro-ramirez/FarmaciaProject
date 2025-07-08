using BE;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Administrador
    {
        MP_BITACORA mp_bitacora = new MP_BITACORA();
        BE.BITACORA bitacora = new BE.BITACORA();

        //************************************ GESTION USUARIOS ************************************//
        public List<BE.Administrador> Listar()
        {

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            return mp.Listar();

        }
       
        public int Grabar(BE.Empleado empleado) {

           int res = 0;

           DAL.MP_ADMINISTRADOR mp_admin = new DAL.MP_ADMINISTRADOR();
     
           res = mp_admin.Insertar(empleado);

            bitacora.usuario = empleado.username;
            bitacora.accion = "Usuario Creado";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);

            return res;
        }

        public int Modificar(BE.Empleado empleado) {

            int res = 0;
            DAL.MP_ADMINISTRADOR mp_admin = new DAL.MP_ADMINISTRADOR();

            res = mp_admin.Editar(empleado);

            bitacora.usuario = empleado.username;
            bitacora.accion = "Se modifico el usuario";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);
            return res;
        }
        public int Loguear(Services.Usuario usuario)
        {

            DAL.MP_LOGIN mp = new DAL.MP_LOGIN();

            int res = mp.LogUser(usuario);

            if (res > 0)
            {

                return 1; // usuario logueado
            }
            else
            {

                return 0; // usuario no logueado
            }
        }
        public int Desbloquear(Usuario usu)
        {

            int res = 0;
            DAL.MP_ADMINISTRADOR mp_admin = new DAL.MP_ADMINISTRADOR();

            res = mp_admin.Desbloquear(usu);
            bitacora.usuario = usu.username;
            bitacora.accion = "Se desbloqueó el usuario";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);
            return res;
        }


        //************************************ GESTION PRODUCTOS ************************************//
        public List<BE.Producto> ListarProductosVentas()
        {

            DAL.MP_PRODUCTO mp = new DAL.MP_PRODUCTO();

            return mp.ListarProductosVentas();

        }

        public List<BE.Producto> ListarProductos()
        {

            DAL.MP_PRODUCTO mp = new DAL.MP_PRODUCTO();

            return mp.Listar();

        }

        public int RegistrarProducto(BE.Producto producto)
        {

            int res = 0;

            DAL.MP_PRODUCTO mp = new DAL.MP_PRODUCTO();

            res = mp.Insertar(producto);

            return res;
        }

        public int ModificarProducto(BE.Producto producto)
        {

            int res = 0;
            DAL.MP_PRODUCTO mp = new DAL.MP_PRODUCTO();

            res = mp.EditarProducto(producto);

            return res;
        }
        public int EliminarProducto(BE.Producto producto)
        {

            int res = 0;
            DAL.MP_PRODUCTO mp = new DAL.MP_PRODUCTO();

            res = mp.Borrar(producto);

            return res;
        }

        public int RestaurarProducto(BE.Producto producto)
        {

            int res = 0;

            DAL.MP_PRODUCTO mp = new DAL.MP_PRODUCTO();

            res = mp.RestaurarEstado(producto);

            return res;
        }

        //************************************ GESTION CLIENTES ************************************//
        public List<BE.Cliente> ListarClientes()
        {

            DAL.MP_CLIENTE mp = new DAL.MP_CLIENTE();

            return mp.Listar();

        }

        public int RegistrarCliente(BE.Cliente cliente)
        {

            int res = 0;

            DAL.MP_CLIENTE mp = new DAL.MP_CLIENTE();

            res = mp.Insertar(cliente);
            bitacora.usuario = cliente.c_nombre;
            bitacora.accion = "Se registro el cliente";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);

            return res;
        }

        public int ModificarCliente(BE.Cliente cliente)
        {

            int res = 0;
            DAL.MP_CLIENTE mp = new DAL.MP_CLIENTE();

            res = mp.Editar(cliente);
            bitacora.usuario = cliente.c_nombre;
            bitacora.accion = "Se modifico el cliente";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);
            return res;
        }
        public int EliminarCliente(BE.Cliente cliente)
        {

            int res = 0;
            DAL.MP_CLIENTE mp = new DAL.MP_CLIENTE();

            res = mp.Borrar(cliente);
            bitacora.usuario = cliente.c_nombre;
            bitacora.accion = "Se elimino el cliente";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);
            return res;
        }

        //************************************ GESTION PROVEEDORES ************************************//

        public List<BE.Proveedor> ListarProveedores()
        {

            DAL.MP_PROVEEDOR mp = new DAL.MP_PROVEEDOR();

            return mp.Listar();

        }

        public int RegistrarProveedor(BE.Proveedor proveedor)
        {

            int res = 0;

            DAL.MP_PROVEEDOR mp = new DAL.MP_PROVEEDOR();

            res = mp.Insertar(proveedor);
            bitacora.usuario = proveedor.p_nombre;
            bitacora.accion = "Se registro el proveedor";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);
            return res;
        }

        public int ModificarProveedor(BE.Proveedor proveedor)
        {

            int res = 0;
            DAL.MP_PROVEEDOR mp = new DAL.MP_PROVEEDOR();

            res = mp.Editar(proveedor);
            bitacora.usuario = proveedor.p_nombre;
            bitacora.accion = "Se modifico el proveedor";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);
            return res;
        }

        public int EliminarProveedor(BE.Proveedor proveedor)
        {

            int res = 0;
            DAL.MP_PROVEEDOR mp = new DAL.MP_PROVEEDOR();

            res = mp.Borrar(proveedor);
            bitacora.usuario = proveedor.p_nombre;
            bitacora.accion = "Se elimino el proveedor";
            bitacora.horario = DateTime.Now;
            mp_bitacora.Insertar(bitacora);
            return res;
        }

        public List<BE.BITACORA> ListarBitacora()
        {

            DAL.MP_BITACORA mp = new DAL.MP_BITACORA();

            return mp.Listar();

        }

        //************************************ GESTION BDD ************************************//

        public int GuardarBD(string ruta)
        {

            int res = 0;

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            res = mp.GuardarBD(ruta);


            return res;
        }

        public string RestaurarBD(string ruta)
        {

            //string res = "0";

            //DAL_H.MP_ADMINISTRADOR mp = new DAL_H.MP_ADMINISTRADOR();

            //res = mp.RestaurarBD(ruta);


            //return res;

            string status;

            try
            {

                SqlConnection conexionMaster = new SqlConnection("Data Source=DESKTOP-GP7K86O\\SQLEXPRESS;Initial Catalog=Farmacia321;Integrated Security=True");
                conexionMaster.Open();

                //List<SqlParameter> parametros = new List<SqlParameter>();

                //parametros.Add(acceso.CrearParametro("@rutacompleta1", ruta));

                using (SqlCommand comandoMultiUser = new SqlCommand($"USE MASTER ALTER DATABASE Farmacia321 SET OFFLINE WITH ROLLBACK IMMEDIATE; RESTORE DATABASE Farmacia321 FROM DISK= '{ruta}' WITH REPLACE; ALTER DATABASE Farmacia321 SET ONLINE WITH ROLLBACK IMMEDIATE", conexionMaster))
                {
                    comandoMultiUser.ExecuteNonQuery();
                }

                status = "1";
            }
            catch (Exception ex)
            {

                status = "hubo un error || " + ex;
            }
            //return acceso.Escribir("RESTAURAR_BD", parametros);

            return status;
        }

        public int GuardarDV(string tabla, string dv, string fecha)
        {

            int res = 0;

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            res = mp.GuardarDV(tabla, dv, fecha);


            return res;
        }

        public List<string> ListarTablas()
        {

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            return mp.ListarTablas();

        }

        public DataTable ListarTablaEspecifica(string tablaSeleccionada)
        {

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            return mp.ListarTablaEspecifica(tablaSeleccionada);

        }
        public List<(string Tabla, string Digito, string Fecha)> ListarDigitos()
        {

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            return mp.ListarDigitos();

        }

        //************************************ GESTION REPORTES ************************************//

        public List<BE.Compra> ListarCompras()
        {

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            return mp.ListarCompras();

        }
        public List<BE.Compra> ListarComprasHorario(DateTime horario, DateTime horario2)
        {

            DAL.MP_ADMINISTRADOR mp = new DAL.MP_ADMINISTRADOR();

            return mp.ListarComprasHorario(horario, horario2);

        }
    }
}
