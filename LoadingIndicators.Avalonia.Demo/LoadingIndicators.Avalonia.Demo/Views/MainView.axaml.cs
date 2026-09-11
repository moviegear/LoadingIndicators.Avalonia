using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace LoadingIndicators.Avalonia.Demo.Views;

public sealed partial class MainView : UserControl
{
#if DEBUG
    private const int CaptureFramesPerSecond = 30;
    private const int MaxPendingFrameSaves = 8;
    private static readonly TimeSpan CaptureDuration = TimeSpan.FromSeconds(12);
    private static readonly int CaptureEncoderCount = Math.Clamp(Environment.ProcessorCount / 2, 2, 4);

    private bool _isCapturing;
#endif

    public MainView()
    {
        InitializeComponent();
    }

#if DEBUG
    public async Task<string?> CaptureLoopFramesAsync()
    {
        if (_isCapturing || Bounds.Width <= 0 || Bounds.Height <= 0)
            return null;

        _isCapturing = true;

        try
        {
            var outputDirectory = Path.Combine(AppContext.BaseDirectory, "captures", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(outputDirectory);

            await ResetAnimationsAsync();

            var renderScaling = (VisualRoot as TopLevel)?.RenderScaling ?? 1;
            var pixelSize = new PixelSize(Math.Max(1, (int)Math.Ceiling(Bounds.Width * renderScaling)), Math.Max(1, (int)Math.Ceiling(Bounds.Height * renderScaling)));
            var dpi = new Vector(96 * renderScaling, 96 * renderScaling);

            var frameCount = (int)(CaptureDuration.TotalSeconds * CaptureFramesPerSecond);
            var frameInterval = TimeSpan.FromSeconds(1d / CaptureFramesPerSecond);
            var stopwatch = Stopwatch.StartNew();
            var pendingSaves = new List<Task>(MaxPendingFrameSaves);

            using var saveThrottle = new SemaphoreSlim(CaptureEncoderCount);

            for (var frameIndex = 0; frameIndex < frameCount; frameIndex++)
            {
                var targetElapsed = TimeSpan.FromTicks(frameInterval.Ticks * frameIndex);
                var remaining = targetElapsed - stopwatch.Elapsed;

                if (remaining > TimeSpan.Zero)
                    await Task.Delay(remaining);

                var filePath = Path.Combine(outputDirectory, $"frame_{frameIndex:D4}.png");
                var snapshot = await Dispatcher.UIThread.InvokeAsync(() => CaptureFrameSnapshot(pixelSize, dpi), DispatcherPriority.Render);

                pendingSaves.Add(SaveFrameAsync(snapshot, filePath, saveThrottle));
                await DrainCompletedSavesAsync(pendingSaves);

                if (pendingSaves.Count >= MaxPendingFrameSaves)
                {
                    var completedSave = await Task.WhenAny(pendingSaves);
                    pendingSaves.Remove(completedSave);
                    await completedSave;
                }
            }

            await Task.WhenAll(pendingSaves);

            return outputDirectory;
        }
        finally
        {
            _isCapturing = false;
        }
    }

    private async Task ResetAnimationsAsync()
    {
        var indicators = this.GetVisualDescendants().OfType<LoadingIndicator>().ToArray();

        if (indicators.Length == 0)
            return;

        foreach (var indicator in indicators)
        {
            indicator.IsActive = false;
        }

        await Dispatcher.UIThread.InvokeAsync(static () => { }, DispatcherPriority.Render);

        foreach (var indicator in indicators)
        {
            indicator.IsActive = true;
        }

        await Dispatcher.UIThread.InvokeAsync(static () => { }, DispatcherPriority.Render);
    }

    private WriteableBitmap CaptureFrameSnapshot(PixelSize pixelSize, Vector dpi)
    {
        using var renderBitmap = new RenderTargetBitmap(pixelSize);
        renderBitmap.Render(this);

        var snapshot = new WriteableBitmap(pixelSize, dpi);
        using var framebuffer = snapshot.Lock();
        renderBitmap.CopyPixels(framebuffer);

        return snapshot;
    }

    private static async Task SaveFrameAsync(WriteableBitmap snapshot, string filePath, SemaphoreSlim saveThrottle)
    {
        await saveThrottle.WaitAsync();

        try
        {
            await Task.Run(() =>
            {
                using (snapshot)
                    snapshot.Save(filePath);
            });
        }
        finally
        {
            saveThrottle.Release();
        }
    }

    private static async Task DrainCompletedSavesAsync(List<Task> pendingSaves)
    {
        for (var index = pendingSaves.Count - 1; index >= 0; index--)
        {
            if (!pendingSaves[index].IsCompleted)
                continue;

            var completedSave = pendingSaves[index];
            pendingSaves.RemoveAt(index);
            await completedSave;
        }
    }
#endif
}