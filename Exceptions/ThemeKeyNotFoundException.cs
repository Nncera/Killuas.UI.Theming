using System;
using Killuas.UI.Theming.Services;

//FINISCH: ThemeKeyNotFoundException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a specified theme key is not found in a given <see cref="ThemePart"/>.
    /// </summary>
    public class ThemeKeyNotFoundException : ArgumentException
    {
        /// <summary>
        /// An exception that is thrown when a specified theme key is not found in a given <see cref="ThemePart"/>.
        /// </summary>
        public ThemeKeyNotFoundException(string themeKey, ThemePart themePart)
            : base(
                  $"Theme Key '{themeKey}' not found in ThemeResource.{themePart}") { }
    }
}
