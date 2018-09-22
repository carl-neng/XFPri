using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Memories.Resx.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo _ci;
        readonly string _resourceId = "";

        public TranslateExtension()
        {
            _ci = string.IsNullOrEmpty("") ? DependencyService.Get<ILocalize>().GetCurrentCultureInfo() : DependencyService.Get<ILocalize>().GetCurrentCultureInfo("km");
            switch (_ci.Name)
            {
                case "km-KH":
                    _resourceId = "Memories.Resx.AppResource.kh";
                    break;
                case "en-US":
                    _resourceId = "Memories.Resx.AppResource";
                    break;
            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) return "";
            ResourceManager temp = new ResourceManager(_resourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);
            var translation = temp.GetString(Text, _ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, _resourceId, _ci.Name), "serviceProvider");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}
