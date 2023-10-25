# LoadingIndicators.Avalonia
[![Nuget](https://img.shields.io/nuget/v/LoadingIndicators.Avalonia)](https://www.nuget.org/packages/LoadingIndicators.Avalonia)

![Demo](https://raw.githubusercontent.com/moviegear/LoadingIndicators.Avalonia/master/.github/demo.gif)

LoadingIndicators.Avalonia is an adaptation for Avalonia of the [LoadingIndicators.WPF](https://github.com/zeluisping/LoadingIndicators.WPF) collection of 9 animated loading indicators.

## Styles
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
- Adjustable animation speed
- Easy activation/deactivation
- Easy change of theme

## Usage
- Include Namespace
```xml
<Window ...
        xmlns:li="using:LoadingIndicators.Avalonia">
```
- Add Loading indicator and select mode
```xml
<li:LoadingIndicator SpeedRatio="{Binding SpeedRatio}" IsActive="{Binding IsArcsActive}" Mode="Arcs" />
```
