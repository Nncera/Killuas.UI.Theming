using System;
using Killuas.UI.Theming.Resources;

//FINISCH: NoThemeResourcesFoundException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when no <see cref="ThemeResources"/> instances could be found.
    /// </summary>
    public class NoThemeResourcesFoundException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when no <see cref="ThemeResources"/> instances could be found.
        /// </summary>
        public NoThemeResourcesFoundException()
            : base(
                  "No ThemeResources was found.\n" +
                  "Make sure to define and add ThemeResources to the MergedDictionaries in App.xaml.") { }
    }
}
