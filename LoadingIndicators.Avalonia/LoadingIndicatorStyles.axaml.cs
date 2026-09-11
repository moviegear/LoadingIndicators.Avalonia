using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace LoadingIndicators.Avalonia;

/// <summary>
/// The loading indicator themes, as a <see cref="Styles"/> collection that can be added from code:
/// <code>
/// Styles.Add(new LoadingIndicatorStyles());
/// </code>
/// Equivalent to including <c>avares://LoadingIndicators.Avalonia/LoadingIndicators.axaml</c> from
/// XAML, but as a typed reference a trimmer can follow, so the themes survive trimming and AOT.
/// </summary>
public partial class LoadingIndicatorStyles : Styles
{
    /// <summary>
    /// Loads the themes.
    /// </summary>
    /// <param name="serviceProvider">Optional provider supplied by the XAML loader.</param>
    public LoadingIndicatorStyles(IServiceProvider? serviceProvider = null)
    {
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
