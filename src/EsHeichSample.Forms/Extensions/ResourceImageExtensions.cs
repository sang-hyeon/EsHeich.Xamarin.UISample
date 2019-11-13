
namespace EsHeichSample.Forms
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using FFImageLoading.Forms;

    [ContentProperty("ResourceID")]
    public class ResourceImageExtension : IMarkupExtension
    {
        public const string DEFAULT_ASEEMBLY_NAME = "EsHeichSample.Forms.Resources.";
        public static string BaseAseeblyName { get; set; } = DEFAULT_ASEEMBLY_NAME;
        /// <summary>
        /// As 'AssemblyName.Folder,Folder.Image.jpg'
        /// </summary>
        public string ResourceID { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(ResourceID))
                return default;

            else return FromResource(ResourceID);
        }

        public static ImageSource FromResource(string resourceID)
        {
            return new EmbeddedResourceImageSource(
                                new Uri($"resource://{BaseAseeblyName}{resourceID}")
                            );
        }
    }
}
