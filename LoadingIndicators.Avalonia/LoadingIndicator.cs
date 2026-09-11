using Avalonia;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;

namespace LoadingIndicators.Avalonia;

/// <summary>
/// An animated loading indicator, in one of the styles of <see cref="LoadingIndicatorMode"/>.
/// The animation is driven by the <c>:active</c> and <c>:inactive</c> pseudo-classes, which follow
/// <see cref="IsActive"/> together with whether the control is in the visual tree and effectively
/// visible, so an indicator scrolled out of view or inside a collapsed parent stops animating
/// instead of burning frames.
/// </summary>
/// <remarks>
/// The animation is drawn in the control's <c>Foreground</c>, which defaults to the FluentTheme
/// accent colour and can be overridden per instance. The styles have to be included in the
/// application for the themes to resolve:
/// <code>
/// &lt;StyleInclude Source="avares://LoadingIndicators.Avalonia/LoadingIndicators.axaml" /&gt;
/// </code>
/// </remarks>
[PseudoClasses(INACTIVE_STATE, ACTIVE_STATE)]
public class LoadingIndicator : TemplatedControl
{
    private const string INACTIVE_STATE = ":inactive";
    private const string ACTIVE_STATE = ":active";

    // ReSharper disable InconsistentNaming
    /// <summary>Identifies the <see cref="IsActive"/> property.</summary>
    public static readonly StyledProperty<bool> IsActiveProperty =
        AvaloniaProperty.Register<LoadingIndicator, bool>(nameof(IsActive), true);

    /// <summary>Identifies the <see cref="Mode"/> property.</summary>
    public static readonly StyledProperty<LoadingIndicatorMode> ModeProperty =
        AvaloniaProperty.Register<LoadingIndicator, LoadingIndicatorMode>(nameof(Mode));

    /// <summary>Identifies the <see cref="SpeedRatio"/> property.</summary>
    public static readonly StyledProperty<double> SpeedRatioProperty =
        AvaloniaProperty.Register<LoadingIndicator, double>(nameof(SpeedRatio), 1.0);

    /// <summary>Identifies the <see cref="Thickness"/> property.</summary>
    public static readonly StyledProperty<double> ThicknessProperty =
        AvaloniaProperty.Register<LoadingIndicator, double>(nameof(Thickness), 4);
    // ReSharper restore InconsistentNaming

    /// <summary>
    /// Whether the indicator should animate. Defaults to <see langword="true"/>. Setting it does
    /// not force animation on its own: the control also has to be attached and effectively visible.
    /// </summary>
    public bool IsActive
    {
        get => GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    /// <summary>
    /// Which animation to show. Defaults to <see cref="LoadingIndicatorMode.Arc"/>. The value names
    /// the control theme that is applied, so it has no effect unless that theme is in the
    /// application resources.
    /// </summary>
    public LoadingIndicatorMode Mode
    {
        get => GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    /// <summary>
    /// Multiplier applied to the animation speed. Defaults to <c>1.0</c>; higher is faster.
    /// </summary>
    public double SpeedRatio
    {
        get => GetValue(SpeedRatioProperty);
        set => SetValue(SpeedRatioProperty, value);
    }

    /// <summary>
    /// Stroke thickness of the animated shapes, in device independent pixels. Defaults to <c>4</c>.
    /// Themes that draw no strokes ignore it.
    /// </summary>
    public double Thickness
    {
        get => GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    private static Dictionary<LoadingIndicatorMode, ControlTheme>? _themes;
    private bool? _animating;
    private bool _attached;
    private readonly AncestorPropertyTracker _ancestorVisibility;

    /// <summary>
    /// Creates a loading indicator and applies the control theme for the current <see cref="Mode"/>.
    /// </summary>
    public LoadingIndicator()
    {
        _ancestorVisibility = new AncestorPropertyTracker(this, IsVisibleProperty);
        _ancestorVisibility.Changed += (_, _) => UpdateVisualStates();
        UpdateTheme();
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        UpdateVisualStates();
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == IsActiveProperty || change.Property == IsVisibleProperty)
            UpdateVisualStates();
        else if (change.Property == ModeProperty)
            UpdateTheme();
    }

    /// <inheritdoc />
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _attached = true;
        _ancestorVisibility.Rebuild();
        UpdateVisualStates();
    }

    /// <inheritdoc />
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _attached = false;
        _ancestorVisibility.Clear();
        UpdateVisualStates();
    }

    private static bool TryGetThemes(out Dictionary<LoadingIndicatorMode, ControlTheme> controlThemes)
    {
        controlThemes = [];
        if (Application.Current is null)
            return false;

#if NETSTANDARD2_0
        var modes = (LoadingIndicatorMode[])Enum.GetValues(typeof(LoadingIndicatorMode));
#else
        var modes = Enum.GetValues<LoadingIndicatorMode>();
#endif
        foreach (var mode in modes)
        {
#if NETSTANDARD2_0
            var name = Enum.GetName(typeof(LoadingIndicatorMode), mode)!;
#else
            var name = Enum.GetName(mode)!;
#endif
            if (!Application.Current.TryGetResource(name, null, out var resource))
                continue;
            if (resource is not ControlTheme theme)
                continue;
            controlThemes.Add(mode, theme);
        }
        return controlThemes.Count > 0;
    }

    private void UpdateTheme()
    {
        if (_themes is null || _themes.Count == 0)
            TryGetThemes(out _themes);
        if (_themes is not null && _themes.TryGetValue(Mode, out var theme))
            Theme = theme;
    }

    private void UpdateVisualStates()
    {
        var animating = IsActive && _attached && IsEffectivelyVisible;
        if (_animating == animating)
            return;
        _animating = animating;

        PseudoClasses.Remove(ACTIVE_STATE);
        PseudoClasses.Remove(INACTIVE_STATE);
        PseudoClasses.Add(animating ? ACTIVE_STATE : INACTIVE_STATE);
    }
}
