using System.Globalization;
using System.Reflection;
using System.Resources;

namespace CpyFcDel.NET
{
    class TranslationManager
    {
        private static readonly TranslationManager manager = new TranslationManager();

        private readonly ResourceManager resManager;

        private TranslationManager() 
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            resManager = new ResourceManager(name + ".lang_" + lang, Assembly.GetExecutingAssembly());
        }

        public static string Translate(string str)
        {
            return manager.resManager.GetString(str);
        }
    }
}
