using System;
using Killuas.UI.Theming.Resources;
using Killuas.UI.Theming.Services;

//FINISCH: NoDefaultKeyFoundException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a <see cref="ThemeDictionaryCollection"/> does not contain a dictionary with the key "Default".
    /// </summary>
    public class NoDefaultKeyFoundException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when a <see cref="ThemeDictionaryCollection"/> does not contain a dictionary with the key "Default".
        /// </summary>
        public NoDefaultKeyFoundException(ThemePart themePart)
            : base(
                  $"ThemeResources.{themePart} requires a 'Default' key.") { }
    }
}
