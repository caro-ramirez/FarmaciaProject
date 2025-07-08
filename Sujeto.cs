using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public abstract class Sujeto
    {
        private IList<IObservador> observadores { get; set; } = new List<IObservador>();

        public virtual void Attach(IObservador observador)
        {
            if (observador != null && !observadores.Contains(observador))
            {
                observadores.Add(observador);
            }
        }

        public virtual void Detach(IObservador observador)
        {
            if (observador != null && observadores.Contains(observador))
            {
                observadores.Remove(observador);
            }
        }

        public virtual void Notify()
        {
            foreach (var observador in observadores)
            {
                observador.Update(this);
            }
        }
    }
}
