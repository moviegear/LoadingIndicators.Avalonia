namespace LoadingIndicators.Avalonia;

/// <summary>
/// Selects which animation a <see cref="LoadingIndicator"/> shows. Each value is resolved at
/// runtime to the <see cref="global::Avalonia.Styling.ControlTheme"/> resource of the same name,
/// so a value only takes effect while the theme it names is present in the application resources.
/// </summary>
public enum LoadingIndicatorMode
{
    /// <summary>A single 270° arc that spins continuously.</summary>
    Arc,

    /// <summary>
    /// A short quarter-arc spinning around a faint full-circle track with cubic ease-in-out easing.
    /// </summary>
    ArcEase,

    /// <summary>
    /// Two concentric arcs where the outer rotates fully and the inner oscillates, varying the gap.
    /// </summary>
    ArcGrow,

    /// <summary>Two concentric arcs counter-rotating at different speeds.</summary>
    Arcs,

    /// <summary>
    /// Short arc segments arranged in a ring that flash bright in sequence with staggered delays.
    /// </summary>
    ArcsRing,

    /// <summary>
    /// Two overlapping circles that alternately grow and shrink with a half-cycle offset.
    /// </summary>
    Bounce,

    /// <summary>
    /// A rectangle that alternately collapses vertically then horizontally, simulating a plane flip.
    /// </summary>
    Plane,

    /// <summary>
    /// A filled circle that expands from nothing while fading out, creating a soft pulse effect.
    /// </summary>
    Pulse,

    /// <summary>Small dots arranged in a circle that pulse in and out in sequence.</summary>
    Circle,

    /// <summary>Three dots that sequentially pop in and out with staggered delays.</summary>
    Flow,

    /// <summary>Five vertical bars that scale up and down in a rolling wave pattern.</summary>
    Wave,

    /// <summary>
    /// Six dots orbit in a ring while shrinking and growing in a staggered chase pattern.
    /// </summary>
    Chase,

    /// <summary>Twelve dots arranged in a ring that fade in sequence around the circle.</summary>
    CircleFade,

    /// <summary>
    /// Two opposite dots rotate around the center while growing and shrinking out of phase.
    /// </summary>
    Swing,

    /// <summary>Nine squares collapse and restore in a diagonal wave across a 3x3 grid.</summary>
    Grid,

    /// <summary>Four square faces fold in sequence around a central diamond silhouette.</summary>
    Fold,

    /// <summary>
    /// Two squares wander around a square path while rotating and shrinking at alternating corners.
    /// </summary>
    Wander,

    /// <summary>Two opposing ring segments rotate continuously around the center.</summary>
    DualRing,

    /// <summary>
    /// Two stroked rings expand from the center in a half-cycle stagger while fading away.
    /// </summary>
    Ripple,

    /// <summary>
    /// Twelve radial bars form a rotating spinner with a linear trailing fade gradient.
    /// </summary>
    Spinner
}
