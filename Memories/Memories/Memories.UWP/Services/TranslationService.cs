using System;
using Memories.UWP.Services;
using Xamarin.Forms;
using System.Globalization;
using System.Threading;
using Memories.Resx.Extensions;

[assembly: Dependency(typeof(TranslationService))]
namespace Memories.UWP.Services
{
    public class TranslationService : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            return new CultureInfo("");
        }
        public CultureInfo GetCurrentCultureInfo(string sLanguageCode)
        {
            return CultureInfo.CreateSpecificCulture(sLanguageCode);
        }
        public void SetLocale()
        {
            var ci = GetCurrentCultureInfo();
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            Console.WriteLine("SetLocale: " + ci.Name);
        }
        public void ChangeLocale(string sLanguageCode)
        {
            var ci = CultureInfo.CreateSpecificCulture(sLanguageCode);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            Console.WriteLine("ChangeToLanguage: " + ci.Name);
        }
    }
}
