using System;
using Killuas.UI.Theming.Services;

//FINISCH: ThemePropertyNotSetException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a theme-bound property has not been initialized properly.
    /// </summary>
    public class ThemePropertyNotSetException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when a theme-bound property has not been initialized properly.
        /// </summary>
        public ThemePropertyNotSetException(string propertyName, ThemePart themePart)
            : base(
                  $"{propertyName} is not set.\n" +
                  $"Either ThemeService.InitializeThemes was not called, or the {themePart} ResourceDictionary was not found.") { }
    }
}
