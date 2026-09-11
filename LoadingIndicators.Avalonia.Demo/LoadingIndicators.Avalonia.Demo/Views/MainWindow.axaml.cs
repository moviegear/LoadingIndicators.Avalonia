using System;
using System.Diagnostics;
using System.IO;

using Avalonia.Controls;
using Avalonia.Input;

namespace LoadingIndicators.Avalonia.Demo.Views;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        KeyDown += MainWindow_KeyDown;
    }

    private async void MainWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
            Close();

        if (e.Key == Key.F11)
            WindowState = WindowState == WindowState.FullScreen ? WindowState.Normal : WindowState.FullScreen;

#if DEBUG
        if (e.KeyModifiers == (KeyModifiers.Control | KeyModifiers.Shift) && e.Key == Key.S && Content is MainView mainView)
        {
            e.Handled = true;

            var baseTitle = "LoadingIndicators Demo";
            Title = $"{baseTitle} [capturing PNG sequence...]";

            try
            {
                var outputDirectory = await mainView.CaptureLoopFramesAsync();

                if (!string.IsNullOrWhiteSpace(outputDirectory))
                {
                    var captureFolderName = Path.GetFileName(outputDirectory);
                    Title = $"{baseTitle} [saved {captureFolderName}]";
                    Debug.WriteLine($"Saved PNG sequence to '{outputDirectory}'.");
                }
                else
                {
                    Title = baseTitle;
                }
            }
            catch (Exception ex)
            {
                Title = $"{baseTitle} [capture failed]";
                Debug.WriteLine(ex);
            }
        }
#endif
    }
}