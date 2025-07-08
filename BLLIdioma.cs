using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    public class BLLIdioma
    {

        DaoIdioma daoidioma = new DaoIdioma();
        public bool CargarIdioma(Idioma idioma)
        {
            return daoidioma.CargarIdioma(idioma);
        }

        public bool CargarIdiomaControl(IdiomaControl idiomacontrol)
        {
            return daoidioma.CargarIdiomaControl(idiomacontrol);
        }

        public List<ControlUsuario> RetornarControles(string formulario)
        {
            return daoidioma.RetornarControles(formulario);
        }

        public List<IdiomaControl> RetornarTraducciones(string idioma)
        {
            return daoidioma.RetornarTraducciones(idioma);
        }

        public DataTable RetornarTablaIdioma(string idioma)
        {
            return daoidioma.RetornarTablaIdioma(idioma);
        }

        public List<string> DevolverIdiomas()
        {
            return daoidioma.DevolverIdiomas();
        }
        public void EditarTraduccion(string codControl, string traduccion, string idioma)
        {
            daoidioma.EditarTraduccion(codControl, traduccion, idioma);
        }

        public DataTable RetornarIdiomas()
        {
            return daoidioma.RetornarIdiomas();
        }

        public void EditarIdioma(string idioma, string nuevonombre)
        {
            daoidioma.EditarIdioma(idioma, nuevonombre);
        }
        public void BorrarIdioma(string idioma)
        {
            daoidioma.BorrarIdioma(idioma);
        }

    }
}
