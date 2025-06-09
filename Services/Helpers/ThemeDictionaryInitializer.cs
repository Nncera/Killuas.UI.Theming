using Killuas.UI.Theming.Exceptions;
using Killuas.UI.Theming.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

//FINISCH: ThemeDictionaryInitializer
namespace Killuas.UI.Theming.Services.Helpers
{
    /// <summary>
    /// Provides methods to initialize and validate modular <see cref="ThemeDictionaryCollection"/>.
    /// </summary>
    internal class ThemeDictionaryInitializer
    {
        #region Helpers

        /// <summary>
        /// Extracts a dictionary of <see cref="ThemeDictionary"/> instances from a <see cref="ThemeDictionaryCollection"/>, indexed by their yey.
        /// </summary>
        /// <param name="themeCollection">The source collection of theme dictionaries.</param>
        /// <returns>A dictionary mapping keys to <see cref="ThemeDictionary"/> instances.</returns>
        private static Dictionary<string, ThemeDictionary> GetThemeDictionariesFromCollection(ThemeDictionaryCollection themeCollection)
        {
            return themeCollection.Where(d => !string.IsNullOrEmpty(d.Key)).ToDictionary(d => d.Key);
        }

        /// <summary>
        /// Ensures that the given dictionary of theme entries contains a "Default" entry.
        /// <para>Throws <see cref="NoDefaultKeyFoundException"/> if the dictionary does not contain a "Default" key.</para>
        /// </summary>
        /// <param name="themeDictionaries">The dictionary of theme entries to validate.</param>
        /// <param name="themePart">The <see cref="ThemePart"/> being validated (used for error context).</param>
        /// <exception cref="NoDefaultKeyFoundException"/>
        private static void ValidateHasDefaultKey(Dictionary<string, ThemeDictionary> themeDictionaries, ThemePart themePart)
        {
            if (!themeDictionaries.ContainsKey("Default"))
            {
                throw new NoDefaultKeyFoundException(themePart);
            }
        }

        #endregion

        #region Main

        /// <summary>
        /// Attempts to initialize a single or modular <see cref="ThemeDictionaryCollection"/> by converting it into a dictionary and applying the default theme.
        /// <para>Throws <see cref="NoDefaultKeyFoundException"/> if the dictionary does not contain a "Default" key.</para>
        /// </summary>
        /// <param name="themeDictionaryCollection">The theme dictionary collection to initialize.</param>
        /// <param name="themeDictionaries">A reference to the dictionary that will be populated with the parsed theme entries.</param>
        /// <param name="themePart">The <see cref="ThemePart"/> being initialized.</param>
        /// <param name="applyDefaultTheme">An action that applies the default theme using the key "Default".</param>
        /// <remarks>
        /// The method only initializes if the provided collection is not empty.
        /// </remarks>
        /// <exception cref="NoDefaultKeyFoundException"/>
        internal static void TryInitializeModularThemes(ThemeDictionaryCollection themeDictionaryCollection, ref Dictionary<string, ThemeDictionary> themeDictionaries, ThemePart themePart, Action<string> applyDefaultTheme)
        {
            if (themeDictionaryCollection.Count > 0)
            {
                themeDictionaries = GetThemeDictionariesFromCollection(themeDictionaryCollection);

                ValidateHasDefaultKey(themeDictionaries, themePart);

                applyDefaultTheme("Default");
            }
        }

        #endregion
    }
}
