using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Services
{
    public class IdiomaSubject : Sujeto
    {
        private Idioma _idioma;

        public Idioma Idioma
        {
            get
            {
                return _idioma;
            }
            set
            {
                this._idioma = value;
                this.Notify();
            }
        }

        public override void Attach(IObservador observador)
        {
            base.Attach(observador);
            if (observador != null) observador.Update(this);
        }


    }
}
