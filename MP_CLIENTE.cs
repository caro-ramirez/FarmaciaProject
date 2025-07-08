using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL 
{
    public class MP_CLIENTE : MAPPER<BE.Cliente>
    {
        Acceso acceso = new Acceso();
        public override int Borrar(Cliente obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@c_dni", obj.c_dni));

            return acceso.Escribir("ELIMINAR_CLIENTE", parametros);
        }

        public override int Editar(Cliente obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            
            parametros.Add(acceso.CrearParametro("@c_dni", obj.c_dni));
            parametros.Add(acceso.CrearParametro("@c_nombre", obj.c_nombre));
            parametros.Add(acceso.CrearParametro("@c_apellido", obj.c_apellido));
      

            return acceso.Escribir("MODIFICAR_CLIENTE", parametros);
        }

        public override int Insertar(Cliente obj)
        {
            //ALTA CLIENTE

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@c_dni", obj.c_dni));
            parametros.Add(acceso.CrearParametro("@c_nombre", obj.c_nombre));
            parametros.Add(acceso.CrearParametro("@c_apellido", obj.c_apellido));


            return acceso.Escribir("REGISTRAR_CLIENTE", parametros);
        }
    

        public List<Cliente> Listar()
        {

            List<Cliente> ListaClientes = new List<Cliente>();


            DataTable tabla = acceso.Leer("LISTAR_CLIENTES", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //COD_PRODUCTO, P_NOMBRE, PRECIO, CATEGORIA

                Cliente cliente= new Cliente();

                cliente.c_dni = int.Parse(registro["c_dni"].ToString());
                cliente.c_nombre = registro["c_nombre"].ToString();
                cliente.c_apellido = registro["c_apellido"].ToString();



                ListaClientes.Add(cliente);

            }


            return ListaClientes;


        }

       
    }
}
