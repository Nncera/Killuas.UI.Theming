using Microsoft.UI.Xaml;

//FINISCH: ThemeResources
namespace Killuas.UI.Theming.Resources
{
    /// <summary>
    /// Serves as the central entry point for the custom theming system.
    /// Supports both "Single" mode, "Modular" mode and "Hybrid" mode.
    /// </summary>
    public sealed class ThemeResources : ResourceDictionary
    {
        /// <summary>
        /// Central <see cref="ThemeDictionaryCollection"/> used in "Single" <see cref="ThemeMode"/>.
        /// All themed resources are defined in a single flat list.
        /// </summary>
        public new ThemeDictionaryCollection ThemeDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for brushes (colors, gradients, etc.)(backgrounds foregrounds, borderBrushes, strokes).
        /// </summary>
        public ThemeDictionaryCollection ThemeBrushesDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for CornerRadius.
        /// </summary>
        public ThemeDictionaryCollection ThemeCornersDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for localization and string resources.
        /// </summary>
        public ThemeDictionaryCollection ThemeLocalizationsDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for size-related values (e.g., width, height, sizes).
        /// </summary>
        public ThemeDictionaryCollection ThemeSizesDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for font families, and styles.
        /// </summary>
        public ThemeDictionaryCollection ThemeFontsDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for outlines and borders.
        /// </summary>
        public ThemeDictionaryCollection ThemeOutlinesDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for margin values.
        /// </summary>
        public ThemeDictionaryCollection ThemeMarginsDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for padding values.
        /// </summary>
        public ThemeDictionaryCollection ThemePaddingsDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for icons or symbolic resources.
        /// </summary>
        public ThemeDictionaryCollection ThemeIconsDictionaries { get; set; } = new();

        /// <summary>
        /// Modular <see cref="ThemeDictionaryCollection"/> for image resources.
        /// </summary>
        public ThemeDictionaryCollection ThemeImagesDictionaries { get; set; } = new();
    }
}
