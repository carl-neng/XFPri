using System;
using Foundation;
using Memories.iOS.Services;
using Xamarin.Forms;
using System.Globalization;
using System.Threading;
using Memories.Resx.Extensions;


[assembly: Dependency(typeof(TranslationService))]
namespace Memories.iOS.Services
{
    public class TranslationService : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
           // var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;    // en_FR
            //var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;      // en
          //  var netLocale = iosLocaleAuto.Replace("_", "-");
            const string defaultCulture = "en";

            CultureInfo ci;
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                try
                {
                    var pref = NSLocale.PreferredLanguages[0];
                    var netLanguage = pref.Replace("_", "-");
                    ci = CultureInfo.CreateSpecificCulture(netLanguage);
                }
                catch
                {
                    ci = new CultureInfo(defaultCulture);
                }
            }
            else
            {
                ci = new CultureInfo(defaultCulture); // default, shouldn't really happen <img draggable="false" class="emoji" alt="🙂" src="https://s.w.org/images/core/emoji/2.2.1/svg/1f642.svg">
            }
            return ci;
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