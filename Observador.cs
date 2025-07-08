using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class Observador : IObservador
    {
        private readonly Form formulario;

        public Observador(Form formulario)
        {
            this.formulario = formulario;
        }

        public void Update(Sujeto sujeto)
        {
            var controles = formulario.Controls;
            foreach (var controlObject in controles)
            {
                if (controlObject is MenuStrip)
                {
                    var items = HelperStripMenu.GetToolStripMenuItemsRecursive(((MenuStrip)controlObject).Items);
                    items.ToList().ForEach(t => TraducirElementoSiSePuede(t));
                }
                else if (controlObject is StatusStrip)
                {
                    var items = HelperStripMenu.GetStatusStripItems(((StatusStrip)controlObject).Items);
                    items.ToList().ForEach(t => TraducirElementoSiSePuede(t));
                }
                else if (controlObject is DataGridView)
                {
                    var columns = (controlObject as DataGridView).Columns;
                    foreach (DataGridViewColumn column in columns)
                    {
                        TraducirElementoSiSePuede(column);
                    }
                }
                else if (controlObject is GroupBox)
                {
                    var controls = (controlObject as GroupBox).Controls;
                    foreach (Control control in controls)
                    {
                        TraducirElementoSiSePuede(control);
                    }
                }
                //else if (controlObject is Button) {

                //    var items = (controlObject as Button).Text;
                //    TraducirElementoSiSePuede(items);
                //}

                TraducirElementoSiSePuede(controlObject);
            }

            TraducirElementoSiSePuede(formulario);
        }

        private void TraducirElementoSiSePuede(object elemento)
        {
            if (HelperPropiedades.HasProperty(elemento, "Tag") && HelperPropiedades.HasProperty(elemento, "Text") && HelperPropiedades.GetPropertyValue(elemento, "Tag") != null)
            {
                var tag = HelperPropiedades.GetPropertyValue(elemento, "Tag");
                var text = HelperPropiedades.GetPropertyValue(elemento, "Text");
                var traduccion = Traducir(tag?.ToString(), text?.ToString());
                HelperPropiedades.SetPropertyValue(elemento, "Text", traduccion);
            }
            else if (HelperPropiedades.HasProperty(elemento, "Tag") && HelperPropiedades.HasProperty(elemento, "HeaderText") && HelperPropiedades.GetPropertyValue(elemento, "Tag") != null)
            {
                var tag = HelperPropiedades.GetPropertyValue(elemento, "Tag");
                var text = HelperPropiedades.GetPropertyValue(elemento, "HeaderText");
                var traduccion = Traducir(tag?.ToString(), text?.ToString());
                HelperPropiedades.SetPropertyValue(elemento, "HeaderText", traduccion);
            }
        }

        private string Traducir(string tag, string defaultText = "")
        {
            string finalTag = tag;
            string stringFinal = defaultText;
            if (tag.Contains("#"))
            {
                finalTag = GetStringBetweenCharacters(tag, '#', '#');
            }
            var traduccion = IdiomaService.GetInstance.GetTraduccion(finalTag);
            stringFinal = tag.Contains("#") ? tag.Replace("#" + finalTag + "#", traduccion) : traduccion;
            if (!string.IsNullOrEmpty(stringFinal))
            {
                return stringFinal;
            }

            return defaultText;
        }

        public static string GetStringBetweenCharacters(string input, char charFrom, char charTo)
        {
            int posFrom = input.IndexOf(charFrom);
            if (posFrom != -1) //if found char
            {
                int posTo = input.IndexOf(charTo, posFrom + 1);
                if (posTo != -1) //if found char
                {
                    return input.Substring(posFrom + 1, posTo - posFrom - 1);
                }
            }

            return string.Empty;
        }
    }
}
