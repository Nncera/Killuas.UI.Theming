using System;
using System.Collections.Generic;
using System.Linq;
using Killuas.UI.Theming.Resources;
using Killuas.UI.Theming.Exceptions;

//FINISCH: ThemeResourceValidator
namespace Killuas.UI.Theming.Services.Validation
{
    /// <summary>
    /// Provides validation logic for verifying the structure and consistency of <see cref="ThemeResources"/> during initialization.
    /// Ensures that exactly one collection exists per <see cref="ThemePart"/> when multiple <see cref="ThemeResources"/> are defined.
    /// </summary>
    internal class ThemeResourceValidator
    {
        #region Helpers

        /// <summary>
        /// Ensures that at least one <see cref="ThemeResources"/> instance was found.
        /// <para>Throws <see cref="NoThemeResourcesFoundException"/> if no <see cref="ThemeResources"/> were found.</para>
        /// </summary>
        /// <param name="foundThemeResources">A list of discovered <see cref="ThemeResources"/>.</param>
        /// <exception cref="NoThemeResourcesFoundException"/>
        private static void EnsureThemeResourcesExist(List<ThemeResources> foundThemeResources)
        {
            if (foundThemeResources.Count is 0)
            {
                throw new NoThemeResourcesFoundException();
            }
        }

        /// <summary>
        /// Checks whether exactly one <see cref="ThemeResources"/> instance exists.
        /// </summary>
        /// <param name="foundThemeResources">A list of discovered <see cref="ThemeResources"/>.</param>
        /// <returns>True if only one instance exists; otherwise false.</returns>
        private static bool IsSingleThemeResource(List<ThemeResources> foundThemeResources)
        {
            return foundThemeResources.Count is 1;
        }

        /// <summary>
        /// Creates a dictionary to track which <see cref="ThemePart"/> have been encountered.
        /// </summary>
        /// <returns>A dictionary mapping each <see cref="ThemePart"/> to a boolean flag indicating presence.</returns>
        private static Dictionary<ThemePart, bool> GetFoundFlagsDictionary()
        {
            return Enum.GetValues(typeof(ThemePart)).Cast<ThemePart>().ToDictionary(tp => tp, tp => false);
        }

        /// <summary>
        /// Validates that the specified <paramref name="themeCollection"/> for a given <see cref="ThemePart"/> is defined only once.
        /// <para>Throws <see cref="MultipleThemeDictionaryCollectionException"/> if more than one collection is defined for the same <see cref="ThemePart"/>.</para>
        /// </summary>
        /// <param name="themeCollection">The <see cref="ThemeDictionaryCollection"/> to validate.</param>
        /// <param name="foundFlags">A dictionary tracking which <see cref="ThemePart"/> have already been found.</param>
        /// <param name="themePart">The <see cref="ThemePart"/> being checked.</param>
        /// <exception cref="MultipleThemeDictionaryCollectionException"/>
        private static void EnsureNoDuplicateThemeDictionaryCollection(ThemeDictionaryCollection themeCollection, Dictionary<ThemePart, bool> foundFlags, ThemePart themePart)
        {
            if (themeCollection.Count > 0)
            {
                if (foundFlags[themePart])
                {
                    throw new MultipleThemeDictionaryCollectionException(themePart);
                }
                else { foundFlags[themePart] = true; }
            }
        }

        #endregion

        #region Main

        /// <summary>
        /// Validates the structure and integrity of the provided <see cref="ThemeResources"/> list.
        /// Ensures that each <see cref="ThemePart"/> is defined only once across all instances.
        /// <para>Throws <see cref="NoThemeResourcesFoundException"/> if no <see cref="ThemeResources"/> were found.</para>
        /// <para>Throws <see cref="MultipleThemeDictionaryCollectionException"/> if more than one collection is defined for the same <see cref="ThemePart"/>.</para>
        /// </summary>
        /// <param name="foundThemeResources">The list of <see cref="ThemeResources"/> discovered during initialization.</param>
        /// <exception cref="NoThemeResourcesFoundException"/>
        /// <exception cref="MultipleThemeDictionaryCollectionException"/>
        internal static void ValidateThemeResources(List<ThemeResources> foundThemeResources)
        {
            EnsureThemeResourcesExist(foundThemeResources);

            if (IsSingleThemeResource(foundThemeResources)) { return; }

            Dictionary<ThemePart, bool> foundFlags = GetFoundFlagsDictionary();

            foreach (ThemeResources themeResources in foundThemeResources)
            {
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeDictionaries, foundFlags, ThemePart.ThemeDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeBrushesDictionaries, foundFlags, ThemePart.ThemeBrushesDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeCornersDictionaries, foundFlags, ThemePart.ThemeCornersDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeLocalizationsDictionaries, foundFlags, ThemePart.ThemeLocalizationsDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeSizesDictionaries, foundFlags, ThemePart.ThemeSizesDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeFontsDictionaries, foundFlags, ThemePart.ThemeFontsDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeOutlinesDictionaries, foundFlags, ThemePart.ThemeOutlinesDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeMarginsDictionaries, foundFlags, ThemePart.ThemeMarginsDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemePaddingsDictionaries, foundFlags, ThemePart.ThemePaddingsDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeIconsDictionaries, foundFlags, ThemePart.ThemeIconsDictionaries);
                EnsureNoDuplicateThemeDictionaryCollection(themeResources.ThemeImagesDictionaries, foundFlags, ThemePart.ThemeImagesDictionaries);
            }
        }

        #endregion
    }
}
