using Killuas.UI.Theming.Services;
using Killuas.UI.Theming.Resources;

//FINISCH: IThemeViewModel
namespace Killuas.UI.Theming.ViewModels
{
    /// <summary>
    /// Interface for theme-aware view models.
    /// Defines methods for applying different categories of <see cref="ThemeValue"/>.
    /// Supports all 3 single-mode, modular-mode and hybrid-mode application themes.
    /// </summary>
    public interface IThemeViewModel
    {
        /// <summary>
        /// Applies a complete theme in single <see cref="ThemeMode"/>.
        /// </summary>
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        void ApplyTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies brush-related <see cref="ThemeValue"/> (e.g., colors, gradients).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyBrushTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies corner-related <see cref="ThemeValue"/> (e.g., border radius).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyCornerTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies localization-related <see cref="ThemeValue"/> (e.g., strings, translations).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyLocalizationTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies size-related <see cref="ThemeValue"/> (e.g., width, height).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplySizeTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies font-related <see cref="ThemeValue"/> (e.g., font size, family).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyFontTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies outline-related <see cref="ThemeValue"/> (e.g., border stroke).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyOutlineTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies margin-related <see cref="ThemeValue"/>.
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyMarginTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies padding-related <see cref="ThemeValue"/>.
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyPaddingTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies icon-related <see cref="ThemeValue"/> (e.g., geometry, symbolic paths).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyIconTheme(ThemeDictionary themeDictionary);

        /// <summary>
        /// Applies image-related <see cref="ThemeValue"/>.
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        void ApplyImageTheme(ThemeDictionary themeDictionary);
    }
}
