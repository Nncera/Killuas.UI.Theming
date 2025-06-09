using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using Killuas.UI.Theming.ViewModels;
using Killuas.UI.Theming.Services.Validation;
using Killuas.UI.Theming.Exceptions;
using Killuas.UI.Theming.Resources;
using Killuas.UI.Theming.Services.Helpers;

//FINISCH: ThemeService
namespace Killuas.UI.Theming.Services
{
    /// <summary>
    /// Provides centralized management for application theming including initialization, validation, and applying themes.
    /// Supports different <see cref="ThemeMode"/>: Single dictionary, Modular dictionaries, and Hybrid.
    /// Manages multiple <see cref="ThemePart"/> like brushes, corners, fonts, sizes, icons, etc., each with their own collections.
    /// </summary>
    public sealed class ThemeService
    {
        #region Variables

        /// <summary>
        /// The root resource dictionary, typically Application.Current.Resources, which contains all theme resources.
        /// </summary>
        private static ResourceDictionary _rootDictionary = Application.Current.Resources;

        /// <summary>
        /// The currently active <see cref="IThemeViewModel"/>, responsible for applying theme changes to the UI.
        /// </summary>
        private static IThemeViewModel? _currentViewModel;

        private static Dictionary<string, ThemeDictionary> _themeDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeBrushesDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeCornersDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeLocalizationsDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeSizesDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeFontsDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeOutlinesDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeMarginsDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themePaddingsDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeIconsDictionaries = new();
        private static Dictionary<string, ThemeDictionary> _themeImagesDictionaries = new();

        private static string? _currentTheme;
        private static string? _currentBrushTheme;
        private static string? _currentCornerTheme;
        private static string? _currentLocalizationTheme;
        private static string? _currentSizeTheme;
        private static string? _currentFontTheme;
        private static string? _currentOutlineTheme;
        private static string? _currentMarginTheme;
        private static string? _currentPaddingTheme;
        private static string? _currentIconTheme;
        private static string? _currentImageTheme;

        /// <summary>
        /// Gets the key of the currently applied general theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentTheme => ThrowIfNotSet(_currentTheme, nameof(CurrentTheme), ThemePart.ThemeDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied brushes theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentBrushTheme => ThrowIfNotSet(_currentBrushTheme, nameof(CurrentBrushTheme), ThemePart.ThemeBrushesDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied corners theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentCornerTheme => ThrowIfNotSet(_currentCornerTheme, nameof(CurrentCornerTheme), ThemePart.ThemeCornersDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied localizations theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentLocalizationTheme => ThrowIfNotSet(_currentLocalizationTheme, nameof(CurrentLocalizationTheme), ThemePart.ThemeLocalizationsDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied sizes theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentSizeTheme => ThrowIfNotSet(_currentSizeTheme, nameof(CurrentSizeTheme), ThemePart.ThemeSizesDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied fonts theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentFontTheme => ThrowIfNotSet(_currentFontTheme, nameof(CurrentFontTheme), ThemePart.ThemeFontsDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied outlines theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentOutlineTheme => ThrowIfNotSet(_currentOutlineTheme, nameof(CurrentOutlineTheme), ThemePart.ThemeOutlinesDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied margins theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentMarginTheme => ThrowIfNotSet(_currentMarginTheme, nameof(CurrentMarginTheme), ThemePart.ThemeMarginsDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied paddings theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentPaddingTheme => ThrowIfNotSet(_currentPaddingTheme, nameof(CurrentPaddingTheme), ThemePart.ThemePaddingsDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied icons theme.
        /// <para>Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.</para>
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentIconTheme => ThrowIfNotSet(_currentIconTheme, nameof(CurrentIconTheme), ThemePart.ThemeIconsDictionaries)!;

        /// <summary>
        /// Gets the key of the currently applied images theme.
        /// Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        public static string CurrentImageTheme => ThrowIfNotSet(_currentImageTheme, nameof(CurrentImageTheme), ThemePart.ThemeImagesDictionaries)!;


        /// <summary>
        /// Gets the currently active <see cref="IThemeViewModel"/> used for applying theme changes.
        /// <para>Throws <see cref="ThemeViewModelNotSetException"/> if the <see cref="ThemeService"/> were not initialized.</para>
        /// </summary>
        /// <exception cref="ThemeViewModelNotSetException"/>
        public static IThemeViewModel CurrentViewModel => ThrowIfNotSet(_currentViewModel, nameof(CurrentViewModel))!;

        #endregion

        #region Helper Methods ModeValidation

        /// <summary>
        /// Ensures that the single <see cref="ThemeDictionaryCollection"/> has been initialized.
        /// <para>Throws <see cref="MissingSingleThemeDictionaryCollectionException"/> if no themes are loaded in the single <see cref="ThemeMode"/>.</para>
        /// </summary>
        /// <exception cref="MissingSingleThemeDictionaryCollectionException"/>
        private static void ValidateSingleThemeDictionariesInitialized()
        {
            if (_themeDictionaries.Count == 0)
            {
                throw new MissingSingleThemeDictionaryCollectionException();
            }
        }

        /// <summary>
        /// Ensures that at least one modular <see cref="ThemeDictionaryCollection"/> has been initialized.
        /// <para>Throws <see cref="MissingModularThemeDictionaryCollectionException"/> if all modular theme dictionaries are empty.</para>
        /// </summary>
        /// <exception cref="MissingModularThemeDictionaryCollectionException"/>
        private static void ValidateModularThemeDictionariesInitialized()
        {
            if (_themeBrushesDictionaries.Count == 0 && _themeCornersDictionaries.Count == 0 
                && _themeLocalizationsDictionaries.Count == 0 && _themeSizesDictionaries.Count == 0 
                && _themeFontsDictionaries.Count == 0 && _themeOutlinesDictionaries.Count == 0 
                && _themeMarginsDictionaries.Count == 0 && _themePaddingsDictionaries.Count == 0 
                && _themeIconsDictionaries.Count == 0 && _themeImagesDictionaries.Count == 0)
            {
                throw new MissingModularThemeDictionaryCollectionException();
            }
        }

        #endregion

        #region Helper Methods ThemeChanging

        /// <summary>
        /// Checks if the given theme key is already the current applied theme key.
        /// </summary>
        /// <param name="currentTheme">Currently applied theme key</param>
        /// <param name="themeKey">Theme key to check</param>
        /// <returns>True if the keys match, false otherwise.</returns>
        private static bool IsCurrentTheme(string? currentTheme, string themeKey)
        {
            return currentTheme == themeKey;
        }

        /// <summary>
        /// Validates that the <see cref="ThemeService"/> has been initialized with a <see cref="IThemeViewModel"/>.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// </summary>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        private static void ValidateInitialization()
        {
            if (_currentViewModel is null)
            {
                throw new ThemeServiceNotInitializedException();
            }
        }

        /// <summary>
        /// Retrieves and validates a <see cref="ThemeDictionary"/> for the given key from a <see cref="ThemeDictionaryCollection"/>.
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the theme key is not found or invalid.</para>
        /// </summary>
        /// <exception cref="ThemeKeyNotFoundException"/>
        private static ThemeDictionary GetValidatedThemeDictionary(Dictionary<string, ThemeDictionary> themeDictionaries, string themeKey, ThemePart themePart)
        {
            return ValidateContainsKey(themeDictionaries, themeKey, themePart);
        }

        /// <summary>
        /// Checks if the specified <see cref="ThemeDictionaryCollection"/> contains the given theme key.
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the theme key is not found or invalid.</para>
        /// </summary>
        /// <exception cref="ThemeKeyNotFoundException"/>
        private static ThemeDictionary ValidateContainsKey(Dictionary<string, ThemeDictionary> themeDictionaries, string themeKey, ThemePart themePart)
        {
            if (!themeDictionaries.TryGetValue(themeKey, out var newTheme))
            {
                throw new ThemeKeyNotFoundException(themeKey, themePart);
            }

            return newTheme;
        }

        #endregion

        #region Helper Methods VariableExceptions

        /// <summary>
        /// Throws <see cref="ThemePropertyNotSetException"/> if the <see cref="ThemeService"/> is not initialized.
        /// </summary>
        /// <exception cref="ThemePropertyNotSetException"/>
        private static T ThrowIfNotSet<T>(T value, string propertyName, ThemePart themePart)
        {
            if (value == null)
            {
                throw new ThemePropertyNotSetException(propertyName, themePart);
            }

            return value;
        }

        /// <summary>
        /// Throws <see cref="ThemeViewModelNotSetException"/> if the <see cref="ThemeService"/> is not initialized.
        /// </summary>
        /// <exception cref="ThemeViewModelNotSetException"/>
        private static T ThrowIfNotSet<T>(T value, string propertyName)
        {
            if (value == null)
            {
                throw new ThemeViewModelNotSetException(propertyName);
            }

            return value;
        }

        #endregion

        #region Main

        /// <summary>
        /// Initializes the themes based on the given <see cref="IThemeViewModel"/> and <see cref="ThemeMode"/>.
        /// It searches for all <see cref="ThemeResources"/> in the root dictionary, validates them,
        /// and initializes themes according to the selected mode: SingleDictionary, ModularDictionaries, or HybridDictionaries.
        /// <para>Throws <see cref="NoThemeResourcesFoundException"/> if no <see cref="ThemeResources"/> were found.</para>
        /// <para>Throws <see cref="MultipleThemeDictionaryCollectionException"/> if more than one collection is defined for the same <see cref="ThemePart"/>.</para>
        /// <para>Throws <see cref="NoDefaultKeyFoundException"/> if the dictionary does not contain a "Default" key.</para>
        /// <para>Throws <see cref="MissingSingleThemeDictionaryCollectionException"/> if no themes are loaded in the single <see cref="ThemeMode"/>.</para>
        /// <para>Throws <see cref="MissingModularThemeDictionaryCollectionException"/> if all modular <see cref="ThemeDictionaryCollection"/> are empty.</para>
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeDictionaryCollection"/>.</para>
        /// </summary>
        /// <param name="themeViewModel">The <see cref="IThemeViewModel"/> that will apply the theme changes.</param>
        /// <param name="themeMode">The mode that defines how themes are structured and applied.</param>
        /// <exception cref="NoThemeResourcesFoundException"/>
        /// <exception cref="MultipleThemeDictionaryCollectionException"/>
        /// <exception cref="NoDefaultKeyFoundException"/>
        /// <exception cref="MissingSingleThemeDictionaryCollectionException"/>
        /// <exception cref="MissingModularThemeDictionaryCollectionException"/>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void InitializeThemes(IThemeViewModel themeViewModel, ThemeMode themeMode)
        {
            _currentViewModel = themeViewModel;

            List<ThemeResources> foundThemeResources = ThemeResourceFinder.TryFindThemeResources(_rootDictionary);

            ThemeResourceValidator.ValidateThemeResources(foundThemeResources);

            switch (themeMode)
            {
                case ThemeMode.SingleDictionary:
                    InitializeSingleThemes(_rootDictionary, foundThemeResources);
                    break;

                case ThemeMode.ModularDictionaries:
                    InitializeModularThemes(_rootDictionary, foundThemeResources);
                    break;

                case ThemeMode.HybridDictionaries:
                    InitializeSingleThemes(_rootDictionary, foundThemeResources);
                    InitializeModularThemes(_rootDictionary, foundThemeResources);
                    break;
            }
        }

        /// <summary>
        /// Applies a theme internally by validating that the given theme key exists in the provided <see cref="ThemeDictionaryCollection"/>,
        /// then executing the given <paramref name="applyTheme"/> action, and finally updating the current theme state.
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the theme key is not found or invalid.</para>
        /// </summary>
        /// <param name="themeDictionaries">The <see cref="ThemeDictionaryCollection"/> to find the theme in.</param>
        /// <param name="currentTheme">Reference to the current theme key to update.</param>
        /// <param name="themeKey">The theme key to apply.</param>
        /// <param name="themePart">The part of the theme to apply (e.g. brushes, corners).</param>
        /// <param name="applyTheme">The delegate that applies the theme to the <see cref="IThemeViewModel"/>.</param>
        /// <exception cref="ThemeKeyNotFoundException"/>
        private static void ApplyThemeInternal(Dictionary<string, ThemeDictionary> themeDictionaries, ref string? currentTheme, string themeKey, ThemePart themePart, Action<ThemeDictionary> applyTheme)
        {
            ThemeDictionary newTheme = GetValidatedThemeDictionary(themeDictionaries, themeKey, themePart);

            applyTheme(newTheme);

            currentTheme = themeKey;
        }

        #endregion

        #region Manage Single ThemeResource.ThemeDictionaries

        /// <summary>
        /// Initializes single <see cref="ThemeDictionaryCollection"/> from the found theme resources by collecting their <see cref="ThemeDictionary"/>.
        /// <para>Throws <see cref="MissingSingleThemeDictionaryCollectionException"/> if no themes are loaded in the single <see cref="ThemeMode"/>.</para>
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeDictionaryCollection"/>.</para>
        /// </summary>
        /// <param name="_rootDictionary">The root <see cref="ResourceDictionary"/> where themes are located.</param>
        /// <param name="foundThemeResources">A list of <see cref="ThemeResources"/> discovered in the root dictionary.</param>
        /// <exception cref="MissingSingleThemeDictionaryCollectionException"/>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        private static void InitializeSingleThemes(ResourceDictionary _rootDictionary, List<ThemeResources> foundThemeResources)
        {
            foreach (ThemeResources themeResource in foundThemeResources)
            {
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeDictionaries, ref _themeDictionaries, ThemePart.ThemeDictionaries, ApplyTheme);
            }

            ValidateSingleThemeDictionariesInitialized();
        }

        /// <summary>
        /// Applies the theme by key for the single <see cref="ThemeMode"/>.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key of the theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentTheme, themeKey)) { return; }

            ApplyThemeInternal(_themeDictionaries, ref _currentTheme, themeKey, ThemePart.ThemeDictionaries, _currentViewModel!.ApplyTheme);
        }

        #endregion

        #region Manage Modular ThemeResources... ThemeBrushesDictionaries, ThemeCornersDictionaries and more

        /// <summary>
        /// Initializes modular themes from the found <see cref="ThemeResources"/> by collecting various <see cref="ThemeDictionaryCollection"/> like brushes, corners, fonts, etc.
        /// <para>Throws <see cref="MissingModularThemeDictionaryCollectionException"/> if all modular <see cref="ThemeDictionaryCollection"/> are empty.</para>
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeDictionaryCollection"/>.</para>
        /// </summary>
        /// <param name="_rootDictionary">The root <see cref="ResourceDictionary"/> where themes are located.</param>
        /// <param name="foundThemeResources">A list of <see cref="ThemeResources"/> discovered in the root dictionary.</param>
        /// <exception cref="MissingModularThemeDictionaryCollectionException"/>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        private static void InitializeModularThemes(ResourceDictionary _rootDictionary, List<ThemeResources> foundThemeResources)
        {
            foreach (ThemeResources themeResource in foundThemeResources)
            {
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeBrushesDictionaries, ref _themeBrushesDictionaries, ThemePart.ThemeBrushesDictionaries, ApplyBrushTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeCornersDictionaries, ref _themeCornersDictionaries, ThemePart.ThemeCornersDictionaries, ApplyCornerTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeLocalizationsDictionaries, ref _themeLocalizationsDictionaries, ThemePart.ThemeLocalizationsDictionaries, ApplyLocalizationTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeSizesDictionaries, ref _themeSizesDictionaries, ThemePart.ThemeSizesDictionaries, ApplySizeTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeFontsDictionaries, ref _themeFontsDictionaries, ThemePart.ThemeFontsDictionaries, ApplyFontTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeOutlinesDictionaries, ref _themeOutlinesDictionaries, ThemePart.ThemeOutlinesDictionaries, ApplyOutlineTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeMarginsDictionaries, ref _themeMarginsDictionaries, ThemePart.ThemeMarginsDictionaries, ApplyMarginTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemePaddingsDictionaries, ref _themePaddingsDictionaries, ThemePart.ThemePaddingsDictionaries, ApplyPaddingTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeIconsDictionaries, ref _themeIconsDictionaries, ThemePart.ThemeIconsDictionaries, ApplyIconTheme);
                ThemeDictionaryInitializer.TryInitializeModularThemes(themeResource.ThemeImagesDictionaries, ref _themeImagesDictionaries, ThemePart.ThemeImagesDictionaries, ApplyImageTheme);
            }

            ValidateModularThemeDictionariesInitialized();
        }

        /// <summary>
        /// Applies the specified Brushes theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeBrushesDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Brushes theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyBrushTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentBrushTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeBrushesDictionaries, ref _currentBrushTheme, themeKey, ThemePart.ThemeBrushesDictionaries, _currentViewModel!.ApplyBrushTheme);
        }

        /// <summary>
        /// Applies the specified Corners theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeCornersDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Corners theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyCornerTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentCornerTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeCornersDictionaries, ref _currentCornerTheme, themeKey, ThemePart.ThemeCornersDictionaries, _currentViewModel!.ApplyCornerTheme);
        }

        /// <summary>
        /// Applies the specified Localizations theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeLocalizationsDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Localizations theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyLocalizationTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentLocalizationTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeLocalizationsDictionaries, ref _currentLocalizationTheme, themeKey, ThemePart.ThemeLocalizationsDictionaries, _currentViewModel!.ApplyLocalizationTheme);
        }

        /// <summary>
        /// Applies the specified Sizes theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeSizesDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Sizes theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplySizeTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentSizeTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeSizesDictionaries, ref _currentSizeTheme, themeKey, ThemePart.ThemeSizesDictionaries, _currentViewModel!.ApplySizeTheme);
        }

        /// <summary>
        /// Applies the specified Fonts theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeFontsDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Fonts theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyFontTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentFontTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeFontsDictionaries, ref _currentFontTheme, themeKey, ThemePart.ThemeFontsDictionaries, _currentViewModel!.ApplyFontTheme);
        }

        /// <summary>
        /// Applies the specified Outlines theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeOutlinesDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Outlines theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyOutlineTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentOutlineTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeOutlinesDictionaries, ref _currentOutlineTheme, themeKey, ThemePart.ThemeOutlinesDictionaries, _currentViewModel!.ApplyOutlineTheme);
        }

        /// <summary>
        /// Applies the specified Margins theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeMarginsDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Margins theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyMarginTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentMarginTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeMarginsDictionaries, ref _currentMarginTheme, themeKey, ThemePart.ThemeMarginsDictionaries, _currentViewModel!.ApplyMarginTheme);
        }

        /// <summary>
        /// Applies the specified Paddings theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemePaddingsDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Paddings theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyPaddingTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentPaddingTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themePaddingsDictionaries, ref _currentPaddingTheme, themeKey, ThemePart.ThemePaddingsDictionaries, _currentViewModel!.ApplyPaddingTheme);
        }

        /// <summary>
        /// Applies the specified Icons theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeIconsDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Icons theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyIconTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentIconTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeIconsDictionaries, ref _currentIconTheme, themeKey, ThemePart.ThemeIconsDictionaries, _currentViewModel!.ApplyIconTheme);
        }

        /// <summary>
        /// Applies the specified Images theme by key.
        /// Does nothing if the requested theme is already applied.
        /// <para>Throws <see cref="ThemeServiceNotInitializedException"/> if <see cref="ThemeService"/> were not initialized.</para>
        /// <para>Throws <see cref="ThemeKeyNotFoundException"/> if the provided key does not exist in the <see cref="ThemeResources.ThemeImagesDictionaries"/>.</para>
        /// </summary>
        /// <param name="themeKey">The key identifying the Images theme to apply.</param>
        /// <exception cref="ThemeServiceNotInitializedException"/>
        /// <exception cref="ThemeKeyNotFoundException"/>
        public static void ApplyImageTheme(string themeKey)
        {
            if (IsCurrentTheme(_currentImageTheme, themeKey)) { return; }

            ValidateInitialization();

            ApplyThemeInternal(_themeImagesDictionaries, ref _currentImageTheme, themeKey, ThemePart.ThemeImagesDictionaries, _currentViewModel!.ApplyImageTheme);
        }

        #endregion
    }
}
