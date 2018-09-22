using Memories.Resx.Extensions;
using Xamarin.Forms;

namespace CMemories.Resx.Extensions
{
    public static class GetFromName
    {
        public static string ResourceId()
        {
            string resourceId = "";
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo("");
            switch (ci.Name)
            {
                case "km-KH":
                    resourceId = "Memories.Resx.AppResource.kh";
                    break;
                case "en-US":
                    resourceId = "Memories.Resx.AppResource";
                    break;
            }
            return resourceId;
        }
    }
}
