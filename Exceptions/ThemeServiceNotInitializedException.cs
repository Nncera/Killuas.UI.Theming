using System;
using Killuas.UI.Theming.Services;

//FINISCH: ThemeServiceNotInitializedException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when the <see cref="ThemeService"/> has not been initialized before usage.
    /// </summary>
    public class ThemeServiceNotInitializedException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when the <see cref="ThemeService"/> has not been initialized before usage.
        /// </summary>
        public ThemeServiceNotInitializedException()
            : base(
                  $"ThemeService is not initialized.\n" +
                  "Please call ThemeService.InitializeThemes first.") { }
    }
}
