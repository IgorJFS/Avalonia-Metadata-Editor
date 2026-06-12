namespace Docs_Metadata_Editor.Services;

/// <summary>
/// Service interface for displaying application toast notifications.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Displays a success toast notification (typically green).
    /// </summary>
    void ShowSuccess(string title, string message);

    /// <summary>
    /// Displays an informational toast notification (typically blue).
    /// </summary>
    void ShowInfo(string title, string message);

    /// <summary>
    /// Displays an error toast notification (typically red).
    /// </summary>
    void ShowError(string title, string message);
}
