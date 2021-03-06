﻿using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace CpyFcDel.NET.Localization
{
    class TranslationManager
    {
        private static readonly TranslationManager manager = new TranslationManager();

        private readonly ResourceManager resManager;

        private TranslationManager() 
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            resManager = new ResourceManager(name + ".Localization.lang_" + lang, Assembly.GetExecutingAssembly());

        }

        public static string Translate(string str)
        {
            return string.Format(manager.resManager.GetString(str),"\n","\t");
        }
    }
}
