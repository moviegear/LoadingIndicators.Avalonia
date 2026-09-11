using Avalonia;
using Avalonia.VisualTree;

namespace LoadingIndicators.Avalonia;

/// <summary>
/// Watches one property across every visual ancestor of a target and reports when any of them
/// changes it. Avalonia raises no notification when an ancestor stops being visible, so a control
/// that has to react to that has to subscribe to the whole chain itself.
/// </summary>
public sealed class AncestorPropertyTracker
{
    private readonly Visual _target;
    private readonly AvaloniaProperty _property;
    private readonly List<Visual> _ancestors = [];

    /// <summary>
    /// Creates a tracker. It subscribes to nothing until <see cref="Rebuild"/> is called.
    /// </summary>
    /// <param name="target">The visual whose ancestors are watched.</param>
    /// <param name="property">The property to watch on each ancestor.</param>
    public AncestorPropertyTracker(Visual target, AvaloniaProperty property)
    {
        _target = target;
        _property = property;
    }

    /// <summary>
    /// Raised when any tracked ancestor changes the watched property.
    /// </summary>
    public event EventHandler? Changed;

    /// <summary>
    /// Drops the current subscriptions and subscribes to the target's ancestors as they are now.
    /// Call this whenever the target moves in the visual tree, since the previous chain no longer
    /// describes it.
    /// </summary>
    public void Rebuild()
    {
        Clear();
        foreach (var ancestor in _target.GetVisualAncestors())
        {
            ancestor.PropertyChanged += OnAncestorPropertyChanged;
            _ancestors.Add(ancestor);
        }
    }

    /// <summary>
    /// Unsubscribes from every tracked ancestor. Call this when the target leaves the visual tree,
    /// otherwise the ancestors keep it alive through their event handlers.
    /// </summary>
    public void Clear()
    {
        foreach (var ancestor in _ancestors)
        {
            ancestor.PropertyChanged -= OnAncestorPropertyChanged;
        }
        _ancestors.Clear();
    }

    private void OnAncestorPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == _property)
            Changed?.Invoke(this, EventArgs.Empty);
    }
}
