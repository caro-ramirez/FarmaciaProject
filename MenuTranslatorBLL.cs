using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BLL
{
    public class MenuTranslatorBLL: ITranslationObserver
    {
        private List<ITranslationObserver> observers = new List<ITranslationObserver>();

        public void OnMenuTranslated(string language, MenuStrip translatedMenuStrip)
        {
            throw new NotImplementedException();
        }

        public void RegisterObserver(ITranslationObserver observer)
        {
            observers.Add(observer);
        }

        public void TranslateMenu(string language, MenuStrip menuStrip)
        {
            // Realizar la traducción aquí utilizando algún servicio o librería externa

            // Crear un nuevo MenuStrip con los items traducidos
            MenuStrip translatedMenuStrip = new MenuStrip();
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                // Realizar la traducción de cada item y agregarlo al nuevo MenuStrip
                string translatedText = TranslateText(language, item.Text);
                translatedMenuStrip.Items.Add(translatedText);
            }

            // Notificar a los observadores el MenuStrip traducido
            foreach (var observer in observers)
            {
                observer.OnMenuTranslated(language, translatedMenuStrip);
            }
        }

        private string TranslateText(string language, string text)
        {
            // Realizar la traducción aquí utilizando algún servicio o librería externa
            return text; // En este ejemplo, la traducción simplemente retorna el mismo texto
        }
    }
}
