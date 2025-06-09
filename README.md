# ThemeResources (v1.0.16-alpha1)

Killuas.UI.Theming is a flexible, component-based theming system for **WinUI 3**. It provides a structured, declarative, and fully bindable way to manage app appearance — supporting real-time and runtime theme switching.

## Key Features

- **Modular Structure**
  Separate theme categories for **Brushes**, **Corners**, **Fonts**, **Animations**, etc. – cleanly organized in specialized dictionaries.

- **Dynamic Theme Switching via MVVM Binding**
  No code-behind required – themes update **reactively via binding** through a central ViewModel or observable properties.

- **Live Updates Without UI Restart**
  Theme changes take effect immediately — styles, templates, and visual states respond automatically.

- **Three Operating Modes**

  - **Single** – A central `ThemeDictionaryCollection`
  - **Modular** – Separate categories for maximum control
  - **Hybrid** – A combination of both for the best of both worlds

- **MVVM-Friendly**
  Fully bindable with support for ObservableCollections, `INotifyPropertyChanged`, and dedicated sub-ViewModels for each category.

## Installation

```bash
dotnet add package ThemeResources --version 1.0.16-alpha1
```

## Theme Resource Architecture

### Example (Single Mode)

```xml
<theme:ThemeResources x:Key="AppTheme1">
  <theme:ThemeResources.ThemeDictionaries>
    <theme:ThemeDictionary Key="Default">
      <SolidColorBrush x:Key="Background" Color="#FF222222" />
      <CornerRadius x:Key="ButtonCorner">6</CornerRadius>
    </theme:ThemeDictionary>
    <theme:ThemeDictionary Key="Light">
      <SolidColorBrush x:Key="Background" Color="#FFFFFFFF" />
      <CornerRadius x:Key="ButtonCorner">12</CornerRadius>
    </theme:ThemeDictionary>
  </theme:ThemeResources.ThemeDictionaries>
</theme:ThemeResources>
```

### Example (Modular Mode)

```xml
<theme:ThemeResources x:Key="AppTheme2">
  <theme:ThemeResources.ThemeBrushesDictionaries>
    <theme:ThemeDictionary Key="Default">
      <SolidColorBrush x:Key="Background" Color="Black" />
    </theme:ThemeDictionary>
    <theme:ThemeDictionary Key="Light">
      <SolidColorBrush x:Key="Background" Color="White" />
    </theme:ThemeDictionary>
  </theme:ThemeResources.ThemeBrushesDictionaries>

  <theme:ThemeResources.ThemeCornersDictionaries>
    <theme:ThemeDictionary Key="Default">
      <CornerRadius x:Key="Small">4</CornerRadius>
    </theme:ThemeDictionary>
    <theme:ThemeDictionary Key="Medium">
      <CornerRadius x:Key="Medium">8</CornerRadius>
    </theme:ThemeDictionary>
  </theme:ThemeResources.ThemeCornersDictionaries>
</theme:ThemeResources>
```

### Example (Hybrid Mode)

```xml
<theme:ThemeResources x:Key="AppTheme3">
  <theme:ThemeResources.ThemeDictionaries>
    <theme:ThemeDictionary Key="Default">
      <String x:Key="ShadowColor">Hi</String>
    </theme:ThemeDictionary>
    <theme:ThemeDictionary Key="Long">
      <String x:Key="ShadowColor">Hello</String>
    </theme:ThemeDictionary>
  </theme:ThemeResources.ThemeDictionaries>

  <theme:ThemeResources.ThemeBrushesDictionaries>
  <theme:ThemeDictionary Key="Default">
      <SolidColorBrush x:Key="ShadowColor" Color="Black" />
    </theme:ThemeDictionary>
    <theme:ThemeDictionary Key="Dark">
      <SolidColorBrush x:Key="ShadowColor" Color="#FF1E1E1E" />
    </theme:ThemeDictionary>
  </theme:ThemeResources.ThemeBrushesDictionaries>
</theme:ThemeResources>
```

## ThemeService Initialization

To ensure smooth and consistent theming across your app, initialize the `ThemeService` during app startup — ideally inside `App.xaml.cs` within the `OnLaunched` method:

```csharp
protected override void OnLaunched(LaunchActivatedEventArgs args)
{
    ResourceDictionary rootDictionary = Application.Current.Resources;

    ThemeService.InitializeThemes(new MainViewModel(), ThemeMode.SingleDictionary); // before Window.Activate
    rootDictionary["Themes"] = ThemeService.CurrentViewModel;

    _window = new MainWindow();
    _window.Activate();
}
```

`MainViewModel` is your theme-aware view model, and `ThemeMode` selects the desired architecture (`SingleDictionary`, `ModularDictionary`, or `HybridDictionary`).

And in `App.xaml`, define your ViewModel as a static resource:

```xml
<Application.Resources>
  <vm:MainViewModel x:Key="Themes" />
</Application.Resources>
```

> The static resource `"Themes"` is referenced for binding in your XAML UI and must match the assigned key in `App.xaml`.

## Using ViewModels

### Single Mode Exapmple

```csharp
internal class MainViewModel : ThemeViewModelBase
{
    public ThemeValue Background { get; } = new();
    public ThemeValue CornerRadiusSmall { get; } = new();

    public override void ApplyTheme(ThemeDictionary themeDictionary)
    {
        if (themeDictionary["Background"] is SolidColorBrush backgroundBrush)
        {
          Background.Value = backgroundBrush;
        }

        if (themeDictionary["CornerRadiusSmall"] is CornerRadius smallCorner)
        {
          CornerRadiusSmall.Value = smallCorner;
        }
    }
}
```

### Modular Mode Example

```csharp
internal class MainViewModel : ThemeViewModelBase
{
    public SubBrushesViewModel ThemeBrushes { get; } = new();
    public SubCornersViewModel ThemeCorners { get; } = new();

    public override void ApplyBrushTheme(ThemeDictionary themeDictionary)
    {
      ThemeBrushes.ApplyTheme(themeDictionary);
    }

    public override void ApplyCornerTheme(ThemeDictionary themeDictionary)
    {
      ThemeCorners.ApplyTheme(themeDictionary);
    }
}
```

```csharp
internal class SubBrushesViewModel : ThemeViewModelBase
{
    public ThemeValue Background { get; } = new();

    public override void ApplyTheme(ThemeDictionary themeDictionary)
    {
        if (themeDictionary["Background"] is SolidColorBrush backgroundBrush)
        {
          Background.Value = backgroundBrush;
        }
    }
}
```

```csharp
internal class SubCornersViewModel : ThemeViewModelBase
{
    public ThemeValue CornerRadiusSmall { get; } = new();

    public override void ApplyTheme(ThemeDictionary themeDictionary)
    {
        if (themeDictionary["CornerRadiusSmall"] is CornerRadius smallCorner)
        {
          CornerRadiusSmall.Value = smallCorner;
        }
    }
}
```

### Hybrid Mode Example

```csharp
internal class MainViewModel : ThemeViewModelBase
{
    public ThemeValue BorderSmall { get; } = new();
    public SubBrushesViewModel ThemeBrushes { get; } = new();

    public override void ApplyTheme(ThemeDictionary themeDictionary)
    {
        if (themeDictionary["BorderSmall"] is Thickness border)
        {
          BorderSmall.Value = border;
        }
    }

    public override void ApplyBrushTheme(ThemeDictionary themeDictionary)
    {
      ThemeBrushes.ApplyTheme(themeDictionary);
    }
}
```

```csharp
internal class SubBrushesViewModel : ThemeViewModelBase
{
    public ThemeValue Background { get; } = new();

    public override void ApplyTheme(ThemeDictionary themeDictionary)
    {
        if (themeDictionary["Background"] is SolidColorBrush brush)
        {
          Background.Value = brush;
        }
    }
}
```

## Runtime Theme Switching via Binding

### Single Mode Binding

```xml
<Button
  Background="{Binding Background.Value, Source={StaticResource Themes}}"
  CornerRadius="{Binding CornerRadiusSmall.Value, Source={StaticResource Themes}}" >
</Button>
```

### Modular Mode Binding

```xml
<Button
  Background="{Binding ThemeBrushes.Background.Value, Source={StaticResource Themes}}"
  CornerRadius="{Binding ThemeCorners.CornerRadiusSmall, Source={StaticResource Themes}}" >
</Button>
```

### Hybrid Mode Binding

```xml
<Border
  Background="{Binding ThemeBrushes.Background.Value, Source={StaticResource Themes}}"
  BorderThickness="{Binding BorderSmall.Value, Source={StaticResource Themes}}" >
</Border>
```

```csharp
ThemeService.ApplyTheme("Light");
```

> This updates the theme based on the selected key.  
> Depending on the architecture (Single, Modular, or Hybrid), this may include calling:
>
>- `ApplyBrushTheme("Light")`
>- `ApplyCornerTheme("Rounded")`
>- `ApplyLocalizationTheme("EN")`
>
> In Single mode, `ApplyTheme` updates all theme values from a central dictionary.  
> In Modular or Hybrid mode, you must explicitly call the relevant methods (`ApplyBrushTheme`, etc.) to apply specific theme parts.  
> All bound UI elements are updated automatically when their corresponding theme values change.  

## API Overview

### Classes

| Classes                                                            | Description                                                                                                       |
|--------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| `ThemeResources`                                                   | Main container for theme-related resources and collections.                                                       |
| `ThemeDictionaryCollection`                                        | Collection of ThemeDictionary objects, e.g. brushes, corners.                                                     |
| `ThemeDictionary`                                                  | Single dictionary holding resources for a specific theme.                                                         |
| `ThemeService`                                                     | Static class for initializing and switching themes.                                                               |
| `ViewModelBase`                                                    | Base class for theme view models with binding support.                                                            |
| `IThemeViewModel`                                                  | Interface for view models supporting theme switching.                                                             |
| `ThemeValue`                                                       | Wrapper class for bindable theme values (e.g. brushes).                                                           |

### Enums

| Enum                                                               | Description                                                                                                       |
|--------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| `ThemeMode`                                                        | Specifies the operating mode of the theming system (Single, Modular, Hybrid).                                     |
| `ThemePart`                                                        | Defines the types of ThemeDictionaryCollections, e.g. Brushes, Corners, Localizations, etc.                       |

### `ThemeDictionaryCollection` Properties (in `ThemeResources`)

| Property                                                           | Description                                                                                                       |
|--------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| `ThemeDictionaries`                                                | Non-type-specific collection containing arbitrary resources of various types — essentially an “all-in-one” theme. |
| `ThemeBrushesDictionaries`                                         | Collection of brush themes (used in Modular Mode).                                                                |
| `ThemeCornersDictionaries`                                         | Collection of corner themes.                                                                                      |
| `ThemeLocalizationsDictionaries`                                   | Collection of localization themes.                                                                                |
| `ThemeSizesDictionaries`                                           | Collection of size themes.                                                                                        |
| `ThemeFontsDictionaries`                                           | Collection of font themes.                                                                                        |
| `ThemeOutlinesDictionaries`                                        | Collection of outline themes.                                                                                     |
| `ThemeMarginsDictionaries`                                         | Collection of margin themes.                                                                                      |
| `ThemePaddingsDictionaries`                                        | Collection of padding themes.                                                                                     |
| `ThemeIconsDictionaries`                                           | Collection of icon themes.                                                                                        |
| `ThemeImagesDictionaries`                                          | Collection of image themes.                                                                                       |

### Public Variables (`ThemeService`)

| Variable                                                           | Description                                                                                                       |
|--------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| `CurrentTheme`                                                     | Currently applied Non-type-specific theme.                                                                        |
| `CurrentBrushTheme`                                                | Current brush theme.                                                                                              |
| `CurrentCornerTheme`                                               | Current corner theme.                                                                                             |
| `CurrentLocalizationTheme`                                         | Current localization theme.                                                                                       |
| `CurrentSizeTheme`                                                 | Current size theme.                                                                                               |
| `CurrentFontTheme`                                                 | Current font theme.                                                                                               |
| `CurrentOutlineTheme`                                              | Current outline theme.                                                                                            |
| `CurrentMarginTheme`                                               | Current margin theme.                                                                                             |
| `CurrentPaddingTheme`                                              | Current padding theme.                                                                                            |
| `CurrentIconTheme`                                                 | Current icon theme.                                                                                               |
| `CurrentImageTheme`                                                | Current image theme.                                                                                              |
| `CurrentViewModel`                                                 | Currently bound ViewModel containing all theme values.                                                            |

### Methods (`ThemeService`)

| Method                                                             | Description                                                                                                       |
|--------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| `InitializeThemes(IThemeViewModel themeViewModel, ThemeMode mode)` | Initializes the ThemeService with a ViewModel and mode.                                                           |
| `ApplyTheme(string themeKey)`                                      | Applies a complete theme across all theme parts.                                                                  |
| `ApplyBrushTheme(string themeKey)`                                 | Applies only the brush theme.                                                                                     |
| `ApplyCornerTheme(string themeKey)`                                | Applies only the corner theme.                                                                                    |
| `ApplyLocalizationTheme(string themeKey)`                          | Applies only the localization theme.                                                                              |
| `ApplySizeTheme(string themeKey)`                                  | Applies only the size theme.                                                                                      |
| `ApplyFontTheme(string themeKey)`                                  | Applies only the font theme.                                                                                      |
| `ApplyOutlineTheme(string themeKey)`                               | Applies only the outline theme.                                                                                   |
| `ApplyMarginTheme(string themeKey)`                                | Applies only the margin theme.                                                                                    |
| `ApplyPaddingTheme(string themeKey)`                               | Applies only the padding theme.                                                                                   |
| `ApplyIconTheme(string themeKey)`                                  | Applies only the icon theme.                                                                                      |
| `ApplyImageTheme(string themeKey)`                                 | WApplies only the image theme.                                                                                    |

## Exceptions

| Exception Name                                                     | Description                                                                                                       |
|--------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| **NoThemeResourcesFoundException**                                 | No `ThemeResources` found in merged dictionaries; ensure proper definition in `App.xaml.`                         |
| **MultipleThemeDictionaryCollectionException**                     | Only one theme collection of each type allowed per app; duplicate collections cause this error.                   |
| **NoDefaultKeyFoundException**                                     | Every theme dictionary collection requires a `"Default"` key entry; missing it triggers this.                     |
| **MissingSingleThemeDictionaryCollectionException**                | In `Single` or `Hybrid` mode, central `ThemeDictionaries` must exist; otherwise, this exception is thrown.        |
| **MissingModularThemeDictionaryCollectionException**               | In `Modular` or `Hybrid` mode, modular collections (e.g., `ThemeBrushesDictionaries`) must be defined.            |
| **ThemeServiceNotInitializedException**                            | Using `ThemeService` before calling `InitializeThemes` results in this exception.                                 |
| **ThemeKeyNotFoundException**                                      | Requested theme key does not exist during theme switching.                                                        |
| **ThemePropertyNotSetException**                                   | A required property (e.g., ViewModel or ResourceDictionary) is missing or not set properly.                       |
| **ThemeViewModelNotSetException**                                  | ViewModel is missing; typically thrown if `ThemeService.InitializeThemes` was not called.                         |

## License

This project is licensed under the [MIT License](./LICENSE).

## Feedback & Contributions

Found a bug or have an idea? Open an issue or submit a pull request.

> Note: This is an alpha release. APIs and structure may still change before the final release.
