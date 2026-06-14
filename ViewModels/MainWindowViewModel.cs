using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Docs_Metadata_Editor.Models;
using Docs_Metadata_Editor.Services;

namespace Docs_Metadata_Editor.ViewModels;

/// <summary>
/// Main ViewModel that drives the user interface.
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IMetadataService _metadataService;
    private readonly IProfileService _profileService;
    private readonly INotificationService _notificationService;
    
    // Master list of all profiles loaded from JSON
    private readonly List<AtsProfile> _allProfiles = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFileLoaded))]
    [NotifyPropertyChangedFor(nameof(FileName))]
    [NotifyPropertyChangedFor(nameof(IsAutoFillEnabled))]
    private string? _loadedFilePath;

    /// <summary>
    /// Gets whether a PDF file is currently loaded.
    /// </summary>
    public bool IsFileLoaded => !string.IsNullOrEmpty(LoadedFilePath);

    /// <summary>
    /// Gets the display name of the loaded file.
    /// </summary>
    public string FileName => IsFileLoaded ? Path.GetFileName(LoadedFilePath!) : "No PDF loaded";

    // Editable Metadata Fields
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _author = string.Empty;

    [ObservableProperty]
    private string _subject = string.Empty;

    [ObservableProperty]
    private string _keywords = string.Empty;

    [ObservableProperty]
    private string _comments = string.Empty;

    // Saving Mode Bindings
    [ObservableProperty]
    private bool _isOverwriteMode = true;

    [ObservableProperty]
    private bool _isDownloadMode = false;

    // Languages Selection
    public string[] Languages { get; } = new[] { "EN", "PT-BR" };

    [ObservableProperty]
    private string _selectedLanguage = "PT-BR";

    // ATS Auto-Fill Profiles
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAutoFillEnabled))]
    private AtsProfile? _selectedProfile;

    /// <summary>
    /// Collection of profiles filtered by selected language.
    /// </summary>
    public ObservableCollection<AtsProfile> FilteredProfiles { get; } = new();

    /// <summary>
    /// Gets whether Auto-Fill command is allowed to execute.
    /// </summary>
    public bool IsAutoFillEnabled => SelectedProfile != null && IsFileLoaded;

    [ObservableProperty]
    private bool _isLoading = false;

    /// <summary>
    /// Default constructor for design-time support.
    /// </summary>
    public MainWindowViewModel() : this(new MetadataService(), new ProfileService(), new DesignTimeNotificationService())
    {
    }

    /// <summary>
    /// Injected constructor.
    /// </summary>
    public MainWindowViewModel(IMetadataService metadataService, IProfileService profileService, INotificationService notificationService)
    {
        _metadataService = metadataService;
        _profileService = profileService;
        _notificationService = notificationService;
        
        // Asynchronously load the profile JSON database
        _ = LoadProfilesAsync();
    }

    private async Task LoadProfilesAsync()
    {
        try
        {
            var loaded = await _profileService.GetProfilesAsync();
            _allProfiles.Clear();
            _allProfiles.AddRange(loaded);
            
            // Trigger initial filter
            UpdateFilteredProfiles();
        }
        catch (Exception ex)
        {
            _notificationService.ShowError("Database Error", $"Failed to load ATS profiles: {ex.Message}");
        }
    }

    /// <summary>
    /// Triggered automatically when SelectedLanguage changes.
    /// </summary>
    partial void OnSelectedLanguageChanged(string value)
    {
        UpdateFilteredProfiles();
    }

    private void UpdateFilteredProfiles()
    {
        FilteredProfiles.Clear();
        foreach (var profile in _allProfiles)
        {
            if (string.Equals(profile.Language, SelectedLanguage, StringComparison.OrdinalIgnoreCase))
            {
                FilteredProfiles.Add(profile);
            }
        }
        
        // Reset selected profile when language shifts
        SelectedProfile = null;
    }

    /// <summary>
    /// Loads a PDF document and extracts its metadata properties.
    /// </summary>
    [RelayCommand]
    public async Task LoadFileAsync(string? path)
    {
        if (string.IsNullOrEmpty(path)) return;

        IsLoading = true;
        
        try
        {
            var metadata = await _metadataService.LoadMetadataAsync(path);
            
            Title = metadata.Title;
            Author = metadata.Author;
            Subject = metadata.Subject;
            Keywords = metadata.Keywords;
            Comments = metadata.Comments;
            
            LoadedFilePath = path;
            
            // Blue notification for loaded successfully (Information)
            _notificationService.ShowInfo("PDF Loaded", "PDF loaded successfully.");
        }
        catch (Exception ex)
        {
            // Red notification for error
            _notificationService.ShowError("Load Error", ex.Message);
            LoadedFilePath = null;
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Applies the selected ATS Profile to the editable fields.
    /// </summary>
    [RelayCommand]
    public void AutoFill()
    {
        if (SelectedProfile == null || !IsFileLoaded) return;

        Title = SelectedProfile.Title;
        Subject = SelectedProfile.Subject;
        Keywords = SelectedProfile.Keywords;
        Comments = SelectedProfile.Comments;

        // Info notification to confirm profile application
        _notificationService.ShowInfo("ATS Auto-Fill Applied", $"Applied template: {SelectedProfile.RoleName}");
    }

    /// <summary>
    /// Saves the modified metadata back to a PDF file.
    /// </summary>
    [RelayCommand]
    public async Task SaveAsync(string? targetPath)
    {
        if (!IsFileLoaded)
        {
            _notificationService.ShowError("Save Error", "No PDF file is loaded.");
            return;
        }

        IsLoading = true;

        try
        {
            var metadata = new ResumeMetadata
            {
                Title = Title,
                Author = Author,
                Subject = Subject,
                Keywords = Keywords,
                Comments = Comments
            };

            string finalPath = IsOverwriteMode ? LoadedFilePath! : targetPath!;
            if (string.IsNullOrEmpty(finalPath))
            {
                throw new InvalidOperationException("Save path is required when exporting a new file.");
            }

            await _metadataService.SaveMetadataAsync(LoadedFilePath!, finalPath, metadata);

            if (IsOverwriteMode)
            {
                // Green notification for success
                _notificationService.ShowSuccess("File Overwritten", "PDF file overwritten successfully.");
            }
            else
            {
                // Green notification for success
                _notificationService.ShowSuccess("File Exported", $"File downloaded successfully to: {Path.GetFileName(finalPath)}");
            }
        }
        catch (Exception ex)
        {
            // Red notification for error
            _notificationService.ShowError("Save Error", ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Design-time dummy notification service to prevent previewer crashes.
    /// </summary>
    private class DesignTimeNotificationService : INotificationService
    {
        public void ShowSuccess(string title, string message) { }
        public void ShowInfo(string title, string message) { }
        public void ShowError(string title, string message) { }
    }
}
