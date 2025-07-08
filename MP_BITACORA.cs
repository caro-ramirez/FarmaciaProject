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
    public class MP_BITACORA : MAPPER<BE.BITACORA>
    {
        Acceso acceso = new Acceso();
        public override int Borrar(BITACORA obj)
        {
            throw new NotImplementedException();
        }

        public override int Editar(BITACORA obj)
        {
            throw new NotImplementedException();
        }

        public override int Insertar(BITACORA obj)
        {
            //REPORTAR BITACORA

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@usuario", obj.usuario));
            parametros.Add(acceso.CrearParametro("@accion", obj.accion));
            parametros.Add(acceso.CrearParametro("@horario", (obj.horario).ToString()));

            return acceso.Escribir("REGISTRAR_BITACORA", parametros);
        }

        public List<BITACORA> Listar()
        {

            List<BITACORA> ListaBitacora = new List<BITACORA>();


            DataTable tabla = acceso.Leer("LISTAR_BITACORA", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //COD_PRODUCTO, P_NOMBRE, PRECIO, CATEGORIA

                BITACORA bitacora = new BITACORA();

                bitacora.usuario = registro["usuario"].ToString();
                bitacora.accion = registro["accion"].ToString();
                bitacora.horario =DateTime.Parse( registro["horario"].ToString());



                ListaBitacora.Add(bitacora);

            }


            return ListaBitacora;


        }
    }
}
