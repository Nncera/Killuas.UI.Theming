using System;
using Killuas.UI.Theming.Resources;
using Killuas.UI.Theming.Services;

//FINISCH: MultipleThemeDictionaryCollectionException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when more than one <see cref="ThemeResources"/> instance defines the same <see cref="ThemePart"/>.
    /// </summary>
    public class MultipleThemeDictionaryCollectionException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when more than one <see cref="ThemeResources"/> instance defines the same <see cref="ThemePart"/>.
        /// </summary>
        public MultipleThemeDictionaryCollectionException(ThemePart themePart)
            : base(
                  $"Multiple ThemeResources instances are allowed, but only one may contain {themePart}.") { }
    }
}
