# ![LoadingIndicators.Avalonia icon](https://raw.githubusercontent.com/Der-Floh/LoadingIndicators.Avalonia/master/Assets/icon-x64.png) LoadingIndicators.Avalonia

[![NuGet Version](https://img.shields.io/nuget/vpre/LoadingIndicators.Avalonia.New)](https://www.nuget.org/packages/LoadingIndicators.Avalonia.New)
[![NuGet Downloads](https://img.shields.io/nuget/dt/LoadingIndicators.Avalonia.New)](https://www.nuget.org/packages/LoadingIndicators.Avalonia.New)
[![CI](https://github.com/Der-Floh/LoadingIndicators.Avalonia/actions/workflows/ci.yml/badge.svg)](https://github.com/Der-Floh/LoadingIndicators.Avalonia/actions/workflows/ci.yml)

![Demo](https://raw.githubusercontent.com/der-floh/LoadingIndicators.Avalonia/master/Assets/demo.gif)

[Online demo](https://der-floh.github.io/LoadingIndicators.Avalonia)

**`LoadingIndicators.Avalonia`** is an adaptation for Avalonia of the [LoadingIndicators.WPF](https://github.com/zeluisping/LoadingIndicators.WPF) collection of animated loading indicators. It provides 20 ready-to-use animated spinner styles as a single `LoadingIndicator` control.

## Installation

```sh
dotnet add package LoadingIndicators.Avalonia.New
```

**Requirements**: .NET Standard 2.0 or later (Avalonia 11.x), or .NET 8.0 or later (Avalonia 12.x+).

## Key Features

- **20 Built-in Animation Styles**: Choose from Arc, ArcEase, ArcGrow, Arcs, ArcsRing, Bounce, Plane, Pulse, Circle, Flow, Wave, Chase, CircleFade, Swing, Grid, Fold, Wander, DualRing, Ripple, and Spinner.
- **Easy Activation**: Toggle the indicator on and off with the `IsActive` property.
- **Adjustable Speed**: Control animation speed independently of other UI elements via `SpeedRatio`.
- **Theme-Aware Color**: Animation color defaults to the application's Fluent accent color automatically; override per-instance with the `Foreground` property.
- **Customisable Stroke**: Adjust line thickness for arc-based styles with the `Thickness` property.
- **Cross-Platform**: Runs on all platforms supported by Avalonia (Windows, macOS, Linux, Browser, Mobile).
- **Compiled Bindings**: Uses Avalonia compiled bindings by default for improved performance.

## Getting Started

### Setup

Add the styles to `App.axaml` so all `LoadingIndicator` controls in your application can resolve their themes:

```xml
<Application.Styles>
    <FluentTheme />
    <StyleInclude Source="avares://LoadingIndicators.Avalonia/LoadingIndicators.axaml" />
</Application.Styles>
```

Or register them from code, which a trimmer can follow, so the themes survive trimming and AOT:

```csharp
Styles.Add(new LoadingIndicatorStyles());
```

### Basic Usage

Place a `LoadingIndicator` in any view and bind `IsActive` to your view-model's busy flag:

```xml
<LoadingIndicator IsActive="{Binding IsBusy}" Mode="Arcs" />
```

Optionally adjust speed, thickness, or override the color:

```xml
<LoadingIndicator IsActive="{Binding IsBusy}" Mode="Arc" SpeedRatio="1.0" Thickness="6" />
<LoadingIndicator Mode="ArcGrow" Foreground="CornflowerBlue" />
```

### LoadingIndicator Properties

| Property     | Type                   | Default      | Description                                               |
| ------------ | ---------------------- | ------------ | --------------------------------------------------------- |
| `IsActive`   | `bool`                 | `true`       | Shows or hides the animation                              |
| `Mode`       | `LoadingIndicatorMode` | `Arc`        | Selects the animation style                               |
| `SpeedRatio` | `double`               | `1.0`        | Multiplier for the animation speed (1.0 = normal speed)   |
| `Thickness`  | `double`               | `4`          | Stroke thickness used by arc-based animation styles       |
| `Foreground` | `IBrush`               | accent color | Animation color; defaults to the FluentTheme accent color |

### Available Modes

| Mode         | Description                                                                                  |
| ------------ | -------------------------------------------------------------------------------------------- |
| `Arc`        | A single 270° arc that spins continuously                                                    |
| `ArcEase`    | A short quarter-arc spinning around a faint full-circle track with cubic ease-in-out easing  |
| `ArcGrow`    | Two concentric arcs where the outer rotates fully and the inner oscillates, varying the gap  |
| `Arcs`       | Two concentric arcs counter-rotating at different speeds                                     |
| `ArcsRing`   | Short arc segments arranged in a ring that flash bright in sequence with staggered delays    |
| `Bounce`     | Two overlapping circles that alternately grow and shrink with a half-cycle offset            |
| `Plane`      | A rectangle that alternately collapses vertically then horizontally, simulating a plane flip |
| `Pulse`      | A filled circle that expands from nothing while fading out, creating a soft pulse effect     |
| `Circle`     | Small dots arranged in a circle that pulse in and out in sequence                            |
| `Flow`       | Three dots that sequentially pop in and out with staggered delays                            |
| `Wave`       | Five vertical bars that scale up and down in a rolling wave pattern                          |
| `Chase`      | Six dots orbit in a ring while shrinking and growing in a staggered chase pattern            |
| `CircleFade` | Twelve dots arranged in a ring that fade in sequence around the circle                       |
| `Swing`      | Two opposite dots rotate around the center while growing and shrinking out of phase          |
| `Grid`       | Nine squares collapse and restore in a diagonal wave across a 3x3 grid                       |
| `Fold`       | Four square faces fold in sequence around a central diamond silhouette                       |
| `Wander`     | Two squares wander around a square path while rotating and shrinking at alternating corners  |
| `DualRing`   | Two opposing ring segments rotate continuously around the center                             |
| `Ripple`     | Two stroked rings expand from the center in a half-cycle stagger while fading away           |
| `Spinner`    | Twelve radial bars form a rotating spinner with a linear trailing fade gradient              |

## Dependencies

- [Avalonia](https://www.nuget.org/packages/Avalonia/) `[11.1.0, 12.0.0)` *(netstandard2.0)*
- [Avalonia](https://www.nuget.org/packages/Avalonia/) `[12.0.0, )` *(net8.0)*
