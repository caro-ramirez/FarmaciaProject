using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TraductorDatos : ITraductorDatos
    {
        public Dictionary<string, string> ObtenerTraducciones(string idioma)
        {
            
            Dictionary<string, string> traducciones = new Dictionary<string, string>();

            if (idioma == "es")
            {
                traducciones.Add("Archivo", "Archivo");
                traducciones.Add("Abrir", "Abrir");
                traducciones.Add("Guardar", "Guardar");
                traducciones.Add("Editar", "Editar");
                traducciones.Add("Copiar", "Copiar");
                traducciones.Add("Pegar", "Pegar");
            }
            else if (idioma == "en")
            {
                traducciones.Add("Archivo", "File");
                traducciones.Add("Abrir", "Open");
                traducciones.Add("Guardar", "Save");
                traducciones.Add("Editar", "Edit");
                traducciones.Add("Copiar", "Copy");
                traducciones.Add("Pegar", "Paste");
            }

            return traducciones;
        }
    }
}
