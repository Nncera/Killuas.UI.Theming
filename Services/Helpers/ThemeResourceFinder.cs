using Microsoft.UI.Xaml;
using System.Collections.Generic;
using Killuas.UI.Theming.Resources;

//FINISCH: ThemeResourceFinder
namespace Killuas.UI.Theming.Services.Helpers
{
    /// <summary>
    /// Provides methods to locate <see cref="ThemeResources"/> instances within a given <see cref="ResourceDictionary"/> hierarchy.
    /// </summary>
    internal class ThemeResourceFinder
    {
        #region Helpers

        /// <summary>
        /// Recursively searches a <see cref="ResourceDictionary"/> and its merged dictionaries for <see cref="ThemeResources"/> instances.
        /// </summary>
        /// <param name="rootDictionary">The root dictionary to start the search from.</param>
        /// <param name="foundThemeResources">The list to populate with discovered <see cref="ThemeResources"/>.</param>
        private static void SearchRecursively(ResourceDictionary rootDictionary, List<ThemeResources> foundThemeResources)
        {
            if (rootDictionary == null) { return; }

            GetThemeResourcesFromValues(rootDictionary, foundThemeResources);
            GetThemeResourcesFromMergedDictionaries(rootDictionary, foundThemeResources);
        }

        /// <summary>
        /// Scans the values of a <see cref="ResourceDictionary"/> for any <see cref="ThemeResources"/> instances.
        /// </summary>
        /// <param name="rootDictionary">The dictionary whose values will be examined.</param>
        /// <param name="foundThemeResources">The list to populate with discovered <see cref="ThemeResources"/>.</param>
        private static void GetThemeResourcesFromValues(ResourceDictionary rootDictionary, List<ThemeResources> foundThemeResources)
        {
            foreach (var value in rootDictionary.Values)
            {
                //Trace.WriteLine($"📦 Wert gefunden: Typ = {value.GetType().FullName}");

                if (value is ThemeResources themeResource)
                {
                    foundThemeResources.Add(themeResource);
                }
            }
        }

        /// <summary>
        /// Recursively examines the <see cref="ResourceDictionary.MergedDictionaries"/> collection to find <see cref="ThemeResources"/>.
        /// </summary>
        /// <param name="rootDictionary">The dictionary whose merged dictionaries will be examined.</param>
        /// <param name="foundThemeResources">The list to populate with discovered <see cref="ThemeResources"/>.</param>
        private static void GetThemeResourcesFromMergedDictionaries(ResourceDictionary rootDictionary, List<ThemeResources> foundThemeResources)
        {
            foreach (var resourceDictionary in rootDictionary.MergedDictionaries)
            {
                //Trace.WriteLine($"📦 Wert gefunden: Typ = {resourceDictionary.GetType().FullName}");

                if (resourceDictionary is ThemeResources themeResources)
                {
                    foundThemeResources.Add(themeResources);
                }

                SearchRecursively(resourceDictionary, foundThemeResources);
            }
        }

        #endregion

        #region Main

        /// <summary>
        /// Attempts to find all <see cref="ThemeResources"/> instances within the specified root dictionary and its entire hierarchy.
        /// </summary>
        /// <param name="rootDictionary">The root resource dictionary to search.</param>
        /// <returns>A list of all discovered <see cref="ThemeResources"/> instances.</returns>
        internal static List<ThemeResources> TryFindThemeResources(ResourceDictionary rootDictionary)
        {
            List<ThemeResources> foundThemeResources = new List<ThemeResources>();

            SearchRecursively(rootDictionary, foundThemeResources);

            return foundThemeResources;
        }

        #endregion
    }
}
