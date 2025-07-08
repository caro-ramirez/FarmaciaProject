using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ITraductorDatos
    {
        Dictionary<string, string> ObtenerTraducciones(string idioma);

    }
}
