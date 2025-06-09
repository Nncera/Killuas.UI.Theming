using System;
using Killuas.UI.Theming.Resources;
using Killuas.UI.Theming.Services;

//FINISCH: MissingModularThemeDictionaryCollectionException
namespace Killuas.UI.Theming.Exceptions
{
    /// <summary>
    /// An exception that is thrown when no <see cref="ThemeDictionaryCollection"/> is found within a <see cref="ThemeResources"/> instance,
    /// despite using <see cref="ThemeMode.Modular"/> or <see cref="ThemeMode.Hybrid"/>.
    /// </summary>
    public class MissingModularThemeDictionaryCollectionException : InvalidOperationException
    {
        /// <summary>
        /// An exception that is thrown when no <see cref="ThemeDictionaryCollection"/> is found within a <see cref="ThemeResources"/> instance,
        /// despite using <see cref="ThemeMode.Modular"/> or <see cref="ThemeMode.Hybrid"/>.
        /// </summary>
        public MissingModularThemeDictionaryCollectionException()
            : base(
                  "No ThemeDictionaryCollection found in ThemeResources.\n" +
                  "If you are using ThemeMode.Modular or ThemeMode.Hybrid, make sure to add at least one of the following blocks:\n" +
                  "<theme:ThemeResource.ThemeBrushesDictionaries>\n" +
                  "<theme:ThemeResource.ThemeCornersDictionaries>\n" +
                  "<theme:ThemeResource.ThemeLocalizationsDictionaries>\n" +
                  "<theme:ThemeResource.ThemeSizesDictionaries>\n" +
                  "<theme:ThemeResource.ThemeFontsDictionaries>\n" +
                  "<theme:ThemeResource.ThemeOutlinesDictionaries>\n" +
                  "<theme:ThemeResource.ThemeMarginsDictionaries>\n" +
                  "<theme:ThemeResource.ThemePaddingsDictionaries>\n" +
                  "<theme:ThemeResource.ThemeIconsDictionaries>\n" +
                  "<theme:ThemeResource.ThemeImagesDictionaries>") { }
    }
}
