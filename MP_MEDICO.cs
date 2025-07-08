using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    /*
    public class MP_MEDICO : MAPPER<BE_H.Medico>
    {
        Acceso acceso = new Acceso();
        public override int Borrar(Medico obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@id", obj.id));

            return acceso.Escribir("medico_borrar", parametros);
        }

        public override int Editar(Medico obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", obj.id));
            parametros.Add(acceso.CrearParametro("@nombre", obj.nombre));
            parametros.Add(acceso.CrearParametro("@apellido", obj.apellido));

            return acceso.Escribir("medico_editar", parametros);
        }

        public override int Insertar(Medico obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("nombre", obj.nombre));
            parametros.Add(acceso.CrearParametro("apellido", obj.apellido));

            return acceso.Escribir("medico_insertar", parametros);
        }

        public List<BE_H.Medico> Listar() {

            List<BE_H.Medico> medicos = new List<BE_H.Medico>();


            DataTable tabla = acceso.Leer("medico_listar", null); // "procedure"


            foreach (DataRow registro in tabla.Rows) {

                BE_H.Medico medico = new BE_H.Medico();

                medico.id = int.Parse(registro["id"].ToString());
                medico.nombre = registro["nombre"].ToString();
                medico.apellido = registro["apellido"].ToString();
                

                medicos.Add(medico);

            }
            return medicos;
        }
    }

        */

}
