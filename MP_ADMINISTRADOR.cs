using Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Dynamic;

namespace DAL
{
    public class MP_ADMINISTRADOR : MAPPER<BE.Empleado>
    {
        Acceso acceso = new Acceso();

        public override int Borrar(Empleado obj)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@username", obj.nombre));

            return acceso.Escribir("", parametros);
        }

        public override int Editar(Empleado obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@userpass", obj.password));
            parametros.Add(acceso.CrearParametro("@dni", obj.dni));
            parametros.Add(acceso.CrearParametro("@nombre", obj.nombre));
            parametros.Add(acceso.CrearParametro("@apellido", obj.apellido));
            parametros.Add(acceso.CrearParametro("@fechanacimiento", (obj.fechanacimiento).ToString("dd/MM/yyyy")));
            parametros.Add(acceso.CrearParametro("@telefono", obj.telefono));
            parametros.Add(acceso.CrearParametro("@intentos", obj.intentos));
            parametros.Add(acceso.CrearParametro("@bloqueado", obj.bloqueado));

            return acceso.Escribir("MODIFICAR_USUARIO", parametros);
        }

        public override int Insertar(Empleado obj)
        {

            //ALTA USUARIO 
            ////DNI, NOMBRE, APELLIDO, FECHANACIMIENTO, TELEFONO <-- DATOS DE EMPLEADO
            ///USERNAME, USERPASS <-- DATOS DE USUARIO
            
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", obj.username));
            parametros.Add(acceso.CrearParametro("@userpass", obj.password));
            parametros.Add(acceso.CrearParametro("@dni", obj.dni));
            parametros.Add(acceso.CrearParametro("@nombre", obj.nombre));
            parametros.Add(acceso.CrearParametro("@apellido", obj.apellido));
            parametros.Add(acceso.CrearParametro("@fechanacimiento", (obj.fechanacimiento).ToString("dd/MM/yyyy")));
            parametros.Add(acceso.CrearParametro("@telefono", obj.telefono));
            parametros.Add(acceso.CrearParametro("@intentos", obj.intentos));
            parametros.Add(acceso.CrearParametro("@bloqueado", obj.bloqueado));
            parametros.Add(acceso.CrearParametro("@rol", obj.Rol));
            parametros.Add(acceso.CrearParametro("@idioma", obj.idioma));

            return acceso.Escribir("REGISTRAR", parametros);
        }

        public List<Administrador> Listar()
        {
            //LISTAR USUARIOS

            List<Administrador> ListaUsuarios = new List<Administrador>();


            DataTable tabla = acceso.Leer("LISTAR_USUARIOS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //DNI, NOMBRE, APELLIDO, FECHA, TELEFONO, USERNAME, PASSWORD, INTENTOS, BLOQUEADO

                Administrador admin = new Administrador();

                admin.dni = int.Parse(registro["dni"].ToString());
                admin.username = registro["username"].ToString();
                admin.nombre = registro["nombre"].ToString();
                admin.apellido = registro["apellido"].ToString();
                admin.fechanacimiento = DateTime.Parse(registro["fechanacimiento"].ToString());
                admin.telefono = int.Parse(registro["telefono"].ToString());
                admin.password = registro["userpass"].ToString();
                string hashing = Encriptador.Hash(admin.password);
                admin.password = hashing;
                admin.intentos = int.Parse(registro["intentos"].ToString());
                admin.bloqueado = int.Parse(registro["bloqueado"].ToString());

                 

                ListaUsuarios.Add(admin);

            }


            return ListaUsuarios;


        }

        public int Desbloquear(Usuario obj)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", obj.username));

            return acceso.Escribir("DESBLOQUEAR", parametros);

        }

        public int GuardarBD(string ruta) {

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@ruta", ruta));


            return acceso.Escribir("GUARDAR_BD", parametros);
        }

        public string RestaurarBD(string ruta)
        {
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

        public int GuardarDV(string tabla, string DV, string fecha)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@tabla", tabla));
            parametros.Add(acceso.CrearParametro("@dv", DV));
            parametros.Add(acceso.CrearParametro("@fecha", fecha));


            return acceso.Escribir("GUARDAR_DV", parametros);
        }


        public List<string> ListarTablas()
        {
            //LISTAR TABLAS

            List<string> ListaTablas = new List<string>();


            DataTable tabla = acceso.Leer("LISTAR_TABLAS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {

                //admin.dni = int.Parse(registro["dni"].ToString());

                ListaTablas.Add(registro["TABLE_NAME"].ToString());

            }

            return ListaTablas;

        }

        //public List<dynamic> ListarTablaEspecifica( string tablaSeleccionada)
        //{
        //    List<dynamic> ListaEspecifica = new List<dynamic>();

        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(acceso.CrearParametro("@tabla", tablaSeleccionada));

        //    DataTable tabla = acceso.Leer("SELECCIONAR_TABLA", parametros);

        //    foreach (DataRow registro in tabla.Rows)
        //    {
        //        dynamic objeto = new ExpandoObject();

        //        foreach (DataColumn columna in tabla.Columns)
        //        {
        //            ((IDictionary<string, object>)objeto)[columna.ColumnName] = registro[columna];
        //        }

        //        ListaEspecifica.Add(objeto);
        //    }

        //    return ListaEspecifica;
        //}

        public DataTable ListarTablaEspecifica(string tablaSeleccionada)
        {
            DataTable tabla = new DataTable();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@tabla", tablaSeleccionada));

            tabla = acceso.Leer("SELECCIONAR_TABLA", parametros);

            return tabla;
        }

        public List<(string Tabla, string Digito, string Fecha)> ListarDigitos()
        {
            List<(string Tabla, string Digito, string Fecha)> ListaDigitos = new List<(string Tabla, string Digito, string Fecha)>();

            DataTable tabla = acceso.Leer("LISTAR_DIGITOS", null);

            foreach (DataRow registro in tabla.Rows)
            {
                string tablaNombre = registro["tabla"].ToString();
                string digito = registro["DV"].ToString();
                string fecha = registro["Fecha"].ToString(); 

                ListaDigitos.Add((tablaNombre, digito, fecha));
            }

            return ListaDigitos;
        }


        public List<Compra> ListarCompras()
        {

            List<Compra> ListaCompras = new List<Compra>();


            DataTable tabla = acceso.Leer("LISTAR_COMPRAS", null); //  "" -> proc almacenado

            foreach (DataRow registro in tabla.Rows)
            {
                //COD_PRODUCTO, P_NOMBRE, PRECIO, CATEGORIA

                Compra compra = new Compra();

                compra.codproveedor = int.Parse(registro["codproveedor"].ToString());
                compra.nombreproveedor = registro["nombreproveedor"].ToString();
                compra.totalventa = int.Parse(registro["totalventa"].ToString());
                compra.totalproductos = int.Parse(registro["totalproductos"].ToString());
                compra.horario = DateTime.Parse(registro["horario"].ToString());


                ListaCompras.Add(compra);

            }


            return ListaCompras;


        }

        public List<Compra> ListarComprasHorario(DateTime obj, DateTime obj2)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@horario", obj.ToShortDateString()));
            parametros.Add(acceso.CrearParametro("@horario2", obj2.ToShortDateString()));

            List<Compra> ListaCompras = new List<Compra>();

            DataTable tabla = acceso.Leer("LISTAR_COMPRAS_HORARIO", parametros);

            foreach (DataRow registro in tabla.Rows)
            {

                Compra compra = new Compra();

                compra.codproveedor = int.Parse(registro["codproveedor"].ToString());
                compra.nombreproveedor = registro["nombreproveedor"].ToString();
                compra.totalventa = int.Parse(registro["totalventa"].ToString());
                compra.totalproductos = int.Parse(registro["totalproductos"].ToString());
                compra.horario = DateTime.Parse(registro["horario"].ToString());


                ListaCompras.Add(compra);

            }


            return ListaCompras;


        }
    }
}
