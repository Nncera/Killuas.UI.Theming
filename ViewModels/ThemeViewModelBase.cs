using System.ComponentModel;
using Killuas.UI.Theming.Resources;

namespace Killuas.UI.Theming.ViewModels
{
    /// <summary>
    /// Abstract base class for all theme-aware view models.
    /// Provides default (empty) implementations of <see cref="IThemeViewModel"/> methods.
    /// Includes <see cref="INotifyPropertyChanged"/> support for property bindings.
    /// </summary>
    public class ThemeViewModelBase : IThemeViewModel, INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Default no-op implementations

        /// <summary>
        /// Applies a complete theme in single <see cref="ThemeMode"/>.
        /// </summary>
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        public virtual void ApplyTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies brush-related <see cref="ThemeValue"/> (e.g., colors, gradients).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyBrushTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies corner-related <see cref="ThemeValue"/> (e.g., border radius).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyCornerTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies localization-related <see cref="ThemeValue"/> (e.g., strings, translations).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyLocalizationTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies size-related <see cref="ThemeValue"/> (e.g., width, height).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplySizeTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies font-related <see cref="ThemeValue"/> (e.g., font size, family).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyFontTheme(ThemeDictionary themethemeDictionaryDict) { }

        /// <summary>
        /// Applies outline-related <see cref="ThemeValue"/> (e.g., border stroke).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyOutlineTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies margin-related <see cref="ThemeValue"/>.
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyMarginTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies padding-related <see cref="ThemeValue"/>.
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyPaddingTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies icon-related <see cref="ThemeValue"/> (e.g., geometry, symbolic paths).
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyIconTheme(ThemeDictionary themeDictionary) { }

        /// <summary>
        /// Applies image-related <see cref="ThemeValue"/>.
        /// <param name="themeDictionary">The complete <see cref="ThemeDictionary"/>.</param>
        /// </summary>
        public virtual void ApplyImageTheme(ThemeDictionary themeDictionary) { }
    }
}
