using BE;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BLL
{
    public class GestionPermisos
    {
        Componente rootComp = new Directorio("Root", 1); // Jerarquia General
        List<ToolStripMenuItem> bllStrip = new List<ToolStripMenuItem>();
        List<Componente> ListaPermiso = new List<Componente>(); // permisos simples
        List<Directorio> ListaRol = new List<Directorio>(); // roles compuestos por permisos
        public static List<BE.Directorio> Roles = new List<BE.Directorio>();


        void ObtenerControles(MenuStrip MenuUI) {

            foreach (ToolStripMenuItem item in MenuUI.Items)
            {

                bllStrip.Add(item);

            }

        }
        public TreeNode ListarJerarquia(Componente componente, TreeNode parentNode)
        {

            if (componente is Directorio directory)
            {

                foreach (Componente child in directory.ObtenerHijos())
                {
                    TreeNode comp = new TreeNode(child.NombrePermiso);
                    parentNode.Nodes.Add(comp);
                    ListarJerarquia(child, parentNode);

                }
            }

            return parentNode;
        }

        public List<Componente> ListarPermisos() {

            return ListaPermiso;

        }

        public List<Componente> ListarAccesosRol(int rol) {


            foreach (Componente c in rootComp.ObtenerHijos()) {

                if (c.IDRol == rol) {

                    return c.ObtenerHijos();
                }

            }
            //si el rol no se encuentra, devuelve lista vacia
            return new List<Componente>();

        }

        public List<Componente> ListarRoles()
        {
            if (rootComp is Directorio rootDirectory) {

                return rootDirectory.ObtenerHijos();
            }

            //si el rol no se encuentra, devuelve lista vacia
            return new List<Componente>();

        }

        public void NuevoPermiso(string permisoNombre, int id) {

            Permiso permiso = new Permiso(permisoNombre, id); // archivo 1 es un permiso simple y tiene acceso a un form simple

            ListaPermiso.Add(permiso);

        }

        public void ListarRolesBDD() // carga los roles que hay en la BDD
        {
            DAL.MP_GESTOR_ROLES mp = new MP_GESTOR_ROLES();
            
            foreach (Directorio dir in mp.ListarRoles()) {

               
                Directorio Rol = new Directorio(dir.NombrePermiso, dir.IDRol);
                ListaRol.Add(Rol);
                rootComp.AgregarHijo(Rol);

            }

            //ListaRol.Add(Rol);

        }

        public void AgregarRol(Directorio rol) {

            DAL.MP_GESTOR_ROLES mp = new MP_GESTOR_ROLES();
            rootComp.AgregarHijo(rol);

            mp.Insertar(rol);


        }

        public void AgregarPermiso(Permiso permiso)
        {

            DAL.MP_GESTOR_PERMISOS mp = new MP_GESTOR_PERMISOS();
            rootComp.AgregarHijo(permiso);

            mp.Insertar(permiso);


        }

        public void ListarPermisosBDD() // carga los permisos que hay en la BDD
        {
            DAL.MP_GESTOR_PERMISOS mp = new MP_GESTOR_PERMISOS();


            foreach (Permiso per in mp.ListarPermisos())
            {


                Permiso permiso = new Permiso(per.NombrePermiso, per.IDRol);
                ListaPermiso.Add(permiso);

                Directorio directorioPadre = (Directorio)rootComp.ObtenerHijos().FirstOrDefault(d => d.IDRol == permiso.IDRol);
                if (directorioPadre != null && directorioPadre is Directorio) {

                    directorioPadre.AgregarHijo(permiso);
                   // rootComp.AgregarHijo(permiso);
                }

            }

            //ListaRol.Add(Rol);

        }

        public void AgregarPermiso(int idRol, int idPermiso) {

            //foreach (Directorio rol in rootComp.ObtenerHijos()) {

            //    if (idRol == rol.IDRol) {

            //        foreach (Permiso per in ListaPermiso) {

            //            if (idPermiso == per.IDRol) {

            //                rol.AgregarHijo(per);
            //            }

            //        }
            //    }

            //}

            Componente rol = CargarRoles().FirstOrDefault(r => r.IDRol == idRol);
            Componente permiso = CargarPermisos().FirstOrDefault(p => p.IDRol == idPermiso);

            if (rol != null && permiso != null)
            {
                rol.AgregarHijo(permiso); // actualiza composite
                AsignarPermiso(rol.IDRol, permiso.IDRol); // actualiza DB

            }

        }

        public string BuscarPermiso(int idRol)
        {
            string Rol;

            Componente rol = ListarRoles().FirstOrDefault(r => r.IDRol == idRol);
            //Permiso permiso = ListaPermiso.FirstOrDefault(p => p.IDRol == idPermiso);

            if (rol != null)
            {

                Rol = rol.NombrePermiso;
            }
            else {

                Rol = "N/A";
            }

            return Rol;
        }

        public List<String> DevolverAccesos(int rol) {

            DAL.MP_GESTOR_ROLES mp = new DAL.MP_GESTOR_ROLES();
            List<String> Accesos = new List<string>(); 

            Accesos = mp.ListarAccesos(rol);

            return Accesos;

        }

        public TreeNode ListarNodos() {

            DAL.MP_GESTOR_ROLES mp = new MP_GESTOR_ROLES();

            rootComp.ObtenerHijos().Clear();
            TreeNode rootNode = new TreeNode("Jerarquia de Roles y Permisos");
            rootNode.Nodes.Clear();
             
            ListarRolesBDD();
            ListarPermisosBDD();

            List<Directorio> rolPermisos =  mp.ListarRolPermiso();


            Dictionary<int, TreeNode> rolNodes = new Dictionary<int, TreeNode>();

            foreach (Directorio rolPermiso in rolPermisos)
            {
                if (!rolNodes.ContainsKey(rolPermiso.IDRol))
                {
                    TreeNode rolNode = new TreeNode(rolPermiso.NombrePermiso);
                    rolNodes.Add(rolPermiso.IDRol, rolNode);
                    rootNode.Nodes.Add(rolNode);
                }

                TreeNode parentRolNode = rolNodes[rolPermiso.IDRol];

                foreach (Permiso permiso in rolPermiso.ObtenerHijos())
                {
                    if (!ExistePermisoEnRol(parentRolNode, permiso))
                    {
                        TreeNode permisoNode = new TreeNode(permiso.NombrePermiso);
                        parentRolNode.Nodes.Add(permisoNode);
                    }
                }
            }


            //if (rootComp is Directorio rootDirectory)
            //{


            //    foreach (Componente child in rootDirectory.ObtenerHijos())
            //    {


            //        TreeNode childNode = ListarJerarquia(child, new TreeNode(child.NombrePermiso));
            //        rootNode.Nodes.Add(childNode);
            //    }

            //}
            return rootNode;
        }

        private bool ExistePermisoEnRol(TreeNode rolNode, Permiso permiso)
        {
            foreach (TreeNode childNode in rolNode.Nodes)
            {
                if (childNode.Text == permiso.NombrePermiso)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddRoles(TreeNode parentNode, Componente componente)
        {

            TreeNode componentNode = new TreeNode(componente.NombrePermiso);

            parentNode.Nodes.Add(componentNode);

            if (componente is Directorio directory)
            {

                foreach (Componente child in directory.ObtenerHijos())
                {

                    AddRoles(componentNode, child);

                }
            }

        }

        public List<BE.Directorio> CargarRoles()
        {

            DAL.MP_GESTOR_ROLES mp = new DAL.MP_GESTOR_ROLES();

            return mp.ListarRoles();

        }

        public List<BE.Permiso> CargarPermisos()
        {

            DAL.MP_GESTOR_PERMISOS mp = new DAL.MP_GESTOR_PERMISOS();

            return mp.ListarPermisos();

        }

        public void AsignarPermiso(int rol, int permiso) {

            DAL.MP_GESTOR_PERMISOS mp = new MP_GESTOR_PERMISOS();

            mp.InsertarPermisoNuevo(rol, permiso);
 
        }

        public List<Usuario> ListarUsuariosRoles() // carga los permisos que hay en la BDD
        {
            DAL.MP_GESTOR_ROLES mp = new MP_GESTOR_ROLES();

            return mp.ListarUsuariosYRoles();
            
        }

        public int CambiarRolUsuario(Usuario user) {

            DAL.MP_GESTOR_ROLES mp = new MP_GESTOR_ROLES();

            return mp.EditarRolUser(user);
        }

    }
}
