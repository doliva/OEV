using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OEVApp.i18n
{
    public static class I18n
    {
        public static XmlDocument documento = new XmlDocument();

        public static void cargarIdioma(EnumIdioma idioma)
        {
            switch (idioma)
            {
                case EnumIdioma.Español: documento.Load("..\\..\\i18n\\String_Es.xml");
                    break;
                case EnumIdioma.English: documento.Load("..\\..\\i18n\\String_En.xml");
                    break;
                default: documento.Load("..\\..\\i18n\\String_Es.xml");
                    break;
            }
        }

        public static String obtenerString(String formulario, String etiqueta)
        {
            XmlNodeList form = documento.GetElementsByTagName("EtiquetasComunes");
            if (form.Item(0).SelectSingleNode(etiqueta) != null)
            {
                return form.Item(0).SelectSingleNode(etiqueta).InnerText;
            }
            else
            {
                form = documento.GetElementsByTagName(formulario);
                return form.Item(0).SelectSingleNode(etiqueta).InnerText;
            }
        } 
    }
}
