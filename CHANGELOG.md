# Changelog

All notable changes to this project will be documented in this file.

## [1.0.16-alpha1] - 2025-06-04

### Added

- Documentation for the entire NuGet package, including `ThemeService`, `ThemeResource` classes, ViewModel classes, helper classes, and exception types.
- Introduced `CHANGELOG` file to track project changes.
- Added `LICENSE` and updated `README` documentation to improve project clarity and legal coverage.
- Introduced dedicated exception types for better diagnostics:
  - `MissingSingleThemeDictionaryCollectionException`
  - `MissingModularThemeDictionaryCollectionException`
  - `MultipleThemeDictionaryCollectionException`
  - `NoDefaultKeyFoundException`
  - `NoThemeResourcesFoundException`
  - `ThemeKeyNotFoundException`
  - `ThemePropertyNotSetException`
  - `ThemeServiceNotInitializedException`
  - `ThemeViewModelNotSetException`

### Changed

- Replaced generic `InvalidOperationException` and `ArgumentException` with custom exceptions for clearer failure reasons.
- Renamed several method parameters to improve naming consistency and readability.
- Standardized method names in `ThemeService`: changed plural naming to singular for `ApplyXTheme` methods
  (e.g., `ApplyBrushesTheme` → `ApplyBrushTheme`, `ApplyCornersTheme` → `ApplyCornerTheme`).

### Refactored

- Cleaned up ThemeService by extracting logic into helper classes: (ThemeResourceValidator, ThemeResourceFinder, ThemeDictionaryInitializer).
  - `ThemeResourceValidator`
  - `ThemeResourceFinder`
  - `ThemeDictionaryInitializer`

### Class Design

- Marked key internal classes as `sealed` to prevent unintended inheritance and ensure controlled extension behavior.

## [1.0.0-alpha1 - 1.0.15-alpha1] – Internal Release

### Initial Development & Refactoring

- Initial architecture established:
  - `ThemeResources` introduced as the central container structure for theming.
  - `ThemeService` implemented for managing and applying themes.
  - `ViewModelBase` and `IThemeViewModel` created to support bindable theme data.
  - `ThemeValue` added as a bindable wrapper class for individual theme values.
  - `ThemeDictionary` added for storing theme-specific resources.
  - `ThemeDictionaryCollection` introduced for organized grouping of themes.
- Added `Single`, `Modular` and `Hybrid` `ThemeMode` implemented to support flexible theming architectures.
- Added error handling using InvalidOperationException and ArgumentException where appropriate.
- Refactored ThemeService:
  - Extracted core logic into helper methods.
  - Improved initialization and robustness.
- Testing and stabilization phase:
  - Fixed various bugs related to theme switching.
  - Resolved initialization issues.
  - Full documentation of previous version changes is not available.
