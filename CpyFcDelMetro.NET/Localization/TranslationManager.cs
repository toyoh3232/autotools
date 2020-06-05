using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace CpyFcDelMetro.NET.Localization
{
    class TranslationManager
    {
        private static readonly TranslationManager manager = new TranslationManager();
        private DataSet languageDataset = new DataSet();

        private readonly ResourceManager resManager;

        private TranslationManager() 
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            resManager = new ResourceManager(name + ".Localization.lang_" + lang, Assembly.GetExecutingAssembly());
            var xmlStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name + ".Localization.lang_" + lang + ".xml");

            DataSet importDataset = new DataSet();
            importDataset.ReadXml(xmlStream);

            languageDataset.Merge(importDataset);
            xmlStream.Close();

        }

        public static string Translate(string str)
        {
            return string.Format(manager.resManager.GetString(str),"\n","\t");
        }

        public static string Translate2(string key)
        {
            DataRow[] languageRows = manager.languageDataset.Tables["Localization"].Select("Key='" + key + "'");
            return languageRows[0]["Value"].ToString();
        }
    }
}
