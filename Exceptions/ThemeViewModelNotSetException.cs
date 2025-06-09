using System;
using Killuas.UI.Theming.ViewModels;

//FINISCH: ThemeViewModelNotSetException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a specific theme-related <see cref="IThemeViewModel"/> property has not been initialized.
    /// </summary>
    public class ThemeViewModelNotSetException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when a specific theme-related <see cref="IThemeViewModel"/> property has not been initialized.
        /// </summary>
        public ThemeViewModelNotSetException(string propertyName)
            : base(
                  $"{propertyName} is not set.\n" +
                  "ThemeService.InitializeThemes was not called") { }
    }
}
