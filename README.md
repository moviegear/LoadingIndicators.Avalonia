# LoadingIndicators.Avalonia
====================
![Demo](./.github/demo.gif)

LoadingIndicators.Avalonia is an adaptation for Avalonia of the [LoadingIndicators.WPF](https://github.com/zeluisping/LoadingIndicators.WPF) collection of 9 animated loading indicators.

## Styles
- Arcs
- Arcs Ring
- Double Bounce
- FlipPlane
- Pulse
- Ring
- Three Dots
- Wave

## Features
- Variable Animation Speed
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
<li:LoadingIndicator Grid.Column="0" Grid.Row="0" SpeedRatio="{Binding SpeedRatio}" IsActive="{Binding IsArcsActive}" Mode="Arcs" />
```