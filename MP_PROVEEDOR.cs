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
    public class MP_PROVEEDOR : MAPPER<Proveedor>
    {
        Acceso acceso = new Acceso();
        public override int Borrar(Proveedor obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@cod_proveedor", obj.cod_proveedor));

            return acceso.Escribir("ELIMINAR_PROVEEDOR", parametros);
        }

        public override int Editar(Proveedor obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@cod_proveedor", obj.cod_proveedor));
            parametros.Add(acceso.CrearParametro("@p_nombre", obj.p_nombre));
            parametros.Add(acceso.CrearParametro("@p_categoria", obj.p_categoria));


            return acceso.Escribir("MODIFICAR_PROVEEDOR", parametros);
        }

        public override int Insertar(Proveedor obj)
        {
            //ALTA PROVEEDOR

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@cod_proveedor", obj.cod_proveedor));
            parametros.Add(acceso.CrearParametro("@p_nombre", obj.p_nombre));
            parametros.Add(acceso.CrearParametro("@p_categoria", obj.p_categoria));


            return acceso.Escribir("REGISTRAR_PROVEEDOR", parametros);
        }

        public List<Proveedor> Listar()
        {

            List<Proveedor> ListaProveedor = new List<Proveedor>();


            DataTable tabla = acceso.Leer("LISTAR_PROVEEDORES", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
               
                Proveedor proveedor = new Proveedor();

                proveedor.cod_proveedor = int.Parse(registro["cod_proveedor"].ToString());
                proveedor.p_nombre = registro["p_nombre"].ToString();
                proveedor.p_categoria  = registro["p_categoria"].ToString();

                ListaProveedor.Add(proveedor);

            }


            return ListaProveedor;


        }

    }
}
