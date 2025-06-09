using System;
using Killuas.UI.Theming.Resources;
using Killuas.UI.Theming.Services;

//FINISCH: MissingSingleThemeDictionaryCollectionException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when no <see cref="ThemeDictionaryCollection"/> is found within a <see cref="ThemeResources"/> instance
    /// of <see cref="ThemeResources"/> while using <see cref="ThemeMode.Single"/> or <see cref="ThemeMode.Hybrid"/>.
    /// </summary>
    public class MissingSingleThemeDictionaryCollectionException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when no <see cref="ThemeDictionaryCollection"/> is found within a <see cref="ThemeResources"/> instance
        /// of <see cref="ThemeResources"/> while using <see cref="ThemeMode.Single"/> or <see cref="ThemeMode.Hybrid"/>.
        /// </summary>
        public MissingSingleThemeDictionaryCollectionException()
            : base(
                  "No ThemeDictionaryCollection found in ThemeResources.\n" +
                  "If you are using ThemeMode.Single or ThemeMode.Hybrid, make sure to add the following block:\n" +
                  "<theme:ThemeResource.ThemeDictionaries>") { }
    }
}
