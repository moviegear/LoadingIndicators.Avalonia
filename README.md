# Optris.LoadingIndicators.Avalonia

> **Optris-maintained fork.** The [original LoadingIndicators.Avalonia](https://github.com/moviegear/LoadingIndicators.Avalonia) has been unmaintained for over two years, and an open community PR for newer Avalonia versions has been sitting unreviewed for months. Optris GmbH has adopted this project to keep it alive for the Avalonia community, starting with an Avalonia 12 port. Published to NuGet as **`Optris.LoadingIndicators.Avalonia`** to avoid colliding with the original package ID. Namespaces are unchanged, so consumer code only needs to swap the package reference.
>
> **Repository:** https://github.com/Optris/Optris.LoadingIndicators.Avalonia

---

[![Nuget](https://img.shields.io/nuget/v/Optris.LoadingIndicators.Avalonia)](https://www.nuget.org/packages/Optris.LoadingIndicators.Avalonia)

![Demo](https://raw.githubusercontent.com/moviegear/LoadingIndicators.Avalonia/master/.github/demo.gif)

[Online demo](https://moviegear.github.io/LoadingIndicators.Avalonia)

LoadingIndicators.Avalonia is an adaptation for Avalonia of the [LoadingIndicators.WPF](https://github.com/zeluisping/LoadingIndicators.WPF) collection of 9 animated loading indicators.


## Themes
- Arc
- Arcs
- Arcs Ring
- Double Bounce
- FlipPlane
- Pulse
- Ring
- Three Dots
- Wave

## Features
- Easy activation/deactivation
- Easy theme change
- Adjustable animation speed

## Usage
1. Add resources in App.axaml
```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceInclude Source="avares://LoadingIndicators.Avalonia/LoadingIndicators.axaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```
2. Include namespace
```xml
<Window ...
        xmlns:li="using:LoadingIndicators.Avalonia">
```
3. Add indicator and select mode
```xml
<li:LoadingIndicator IsActive="{Binding IsBusy}" Mode="Arcs" SpeedRatio="1.2" />
```
