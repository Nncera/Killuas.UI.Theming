using Microsoft.UI.Xaml;

//FINISCH: ThemeDictionary
namespace Killuas.UI.Theming.Resources
{
    /// <summary>
    /// Represents a single named theme variant that contains key-value XAML resources.
    /// Can define brushes, thicknesses, strings, fonts, and more.
    /// </summary>
    public sealed class ThemeDictionary : ResourceDictionary
    {
        /// <summary>
        /// Gets or sets the unique key name of this theme variant (e.g., "Default", "Light", "Dark", "Flat", "Rounded").
        /// </summary>
        public string Key { get; set; } = string.Empty;
    }
}
