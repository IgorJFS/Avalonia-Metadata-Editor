using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Docs_Metadata_Editor.ViewModels;
using Docs_Metadata_Editor.Services;

namespace Docs_Metadata_Editor.Views;

/// <summary>
/// Interaction logic for MainWindow.axaml.
/// </summary>
public partial class MainWindow : Window
{
    private readonly WindowNotificationManager _notificationManager;

    public MainWindow()
    {
        InitializeComponent();

        _notificationManager = new WindowNotificationManager(this)
        {
            Position = NotificationPosition.BottomRight,
            MaxItems = 4
        };

        var notificationService = new NotificationService(_notificationManager);
        DataContext = new MainWindowViewModel(new MetadataService(), new ProfileService(), notificationService);
    }

    /// <summary>
    /// Handles the "Select PDF Resume" button click. Launches the file open picker.
    /// </summary>
    private async void LoadFileButton_Click(object? sender, RoutedEventArgs e)
    {
        var topLevel = GetTopLevel(this);
        if (topLevel == null) return;

        try
        {
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select PDF Resume/CV File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("PDF Documents") { Patterns = new[] { "*.pdf" } }
                }
            });

            if (files.Count > 0)
            {
                var filePath = files[0].Path.LocalPath;
                if (DataContext is MainWindowViewModel viewModel)
                {
                    await viewModel.LoadFileCommand.ExecuteAsync(filePath);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"File Picker Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles the "Save Metadata" button click. If download mode is selected,
    /// launches the save file picker before executing the save command.
    /// </summary>
    private async void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel viewModel) return;
        if (!viewModel.IsFileLoaded) return;

        try
        {
            string? destinationPath = null;

            if (viewModel.IsDownloadMode)
            {
                var topLevel = GetTopLevel(this);
                if (topLevel == null) return;

                // Pre-calculate suggested filename
                var baseName = Path.GetFileNameWithoutExtension(viewModel.LoadedFilePath) ?? "Resume";
                var suggestedName = $"{baseName}_ATS.pdf";

                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Download Optimized PDF Copy",
                    DefaultExtension = "pdf",
                    SuggestedFileName = suggestedName,
                    FileTypeChoices = new[]
                    {
                        new FilePickerFileType("PDF Documents") { Patterns = new[] { "*.pdf" } }
                    }
                });

                if (file == null)
                {
                    // User canceled the Save File dialog
                    return;
                }

                destinationPath = file.Path.LocalPath;
            }

            await viewModel.SaveCommand.ExecuteAsync(destinationPath);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Save File Picker Error: {ex.Message}");
        }
    }
}