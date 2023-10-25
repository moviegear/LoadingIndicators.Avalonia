using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace LoadingIndicators.Avalonia;

public class LoadingIndicator : TemplatedControl
{
    private const string INACTIVE_STATE = ":inactive";
    private const string ACTIVE_STATE = ":active";

    private static readonly StyledProperty<LoadingIndicatorMode> modeProperty =
        AvaloniaProperty.Register<LoadingIndicator, LoadingIndicatorMode>(nameof(Mode));
    private static readonly StyledProperty<bool> isActiveProperty =
        AvaloniaProperty.Register<LoadingIndicator, bool>(nameof(IsActive), true);
    private static readonly StyledProperty<double> speedRatioProperty =
        AvaloniaProperty.Register<LoadingIndicator, double>(nameof(SpeedRatio), 1d);

    private static readonly string[] modeNames;
    private static readonly IDictionary<string, ControlTheme> controlThemes;

    protected override Type StyleKeyOverride => typeof(LoadingIndicator);

    public LoadingIndicatorMode Mode
    {
        get => GetValue(modeProperty);
        set => SetValue(modeProperty, value);
    }
    public bool IsActive
    {
        get => GetValue(isActiveProperty);
        set => SetValue(isActiveProperty, value);
    }
    public double SpeedRatio
    {
        get => GetValue(speedRatioProperty);
        set => SetValue(speedRatioProperty, value);
    }

    static LoadingIndicator()
    {
        var resourcesUri = new Uri("avares://LoadingIndicators.Avalonia/LoadingIndicators.axaml");
        if (AvaloniaXamlLoader.Load(resourcesUri) is not ResourceDictionary resourceDictionary) throw new NullReferenceException();
        modeNames = Enum.GetNames(typeof(LoadingIndicatorMode));
        controlThemes = new AvaloniaDictionary<string, ControlTheme>(resourceDictionary.MergedDictionaries.Count);
        foreach (var resourceProvider in resourceDictionary.MergedDictionaries)
        {
            var dictionary = (ResourceDictionary)resourceProvider;
            var key = (string)dictionary.Keys.First();
            if (!dictionary.TryGetValue(key, out var value)) throw new ArgumentNullException(nameof(value));
            if (value is not ControlTheme theme) throw new ArgumentNullException(nameof(theme));
            controlThemes.Add(key, theme);
        }
    }

    public LoadingIndicator() => UpdateTheme();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        UpdateVisualStates();
        UpdateTheme();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == isActiveProperty) UpdateVisualStates();
        if (change.Property == modeProperty) UpdateTheme();
    }

    private void UpdateVisualStates()
    {
        PseudoClasses.Remove(ACTIVE_STATE);
        PseudoClasses.Remove(INACTIVE_STATE);
        PseudoClasses.Add(IsActive ? ACTIVE_STATE : INACTIVE_STATE);
    }

    private void UpdateTheme() => Theme = controlThemes[modeNames[(int)Mode]];
}