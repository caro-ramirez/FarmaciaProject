using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Services;

namespace BLL
{
    public class IdiomaService : IdiomaSubject
    {
        private static IdiomaService idiomaService;
        private static List<Idioma> _idiomas;


        public static IdiomaService GetInstance
        {
            
            get
            {
                if (idiomaService == null)
                {
                    idiomaService = new IdiomaService();
                }
                        
                return idiomaService;
            }
        }

        public void Initialize(List<Idioma> idiomas)
        {
            _idiomas = idiomas;
            if (this.Idioma == null)
            {
                this.GetDefaultIdioma();
            }
        }


        private void GetDefaultIdioma()
        {
            this.Idioma = ((Usuario)SessionManager.GetInstance.Usuario).idioma;
        }


        public List<Idioma> GetIdiomas()
        {
            return _idiomas;
        }

        public string GetTraduccion(string tag)
        {
            BLLIdioma bllidioma = new BLLIdioma();

            List<IdiomaControl> listaTraducciones = bllidioma.RetornarTraducciones(this.Idioma.NombreIdioma);
            if (listaTraducciones != null && listaTraducciones.Any(t => t.CodControl == tag))
            {
                var idiomaTag = listaTraducciones.FirstOrDefault(t => t.CodControl == tag);
                return idiomaTag.Traduccion;
            }
            return null;
        }
    }
}
