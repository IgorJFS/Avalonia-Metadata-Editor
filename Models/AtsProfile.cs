namespace Docs_Metadata_Editor.Models;

/// <summary>
/// Represents a preconfigured ATS profile for auto-filling metadata.
/// </summary>
public class AtsProfile
{
    /// <summary>
    /// Unique identifier for the profile.
    /// </summary>
    public string ProfileId { get; set; } = string.Empty;

    /// <summary>
    /// The language of the profile (e.g., "EN" or "PT-BR").
    /// </summary>
    public string Language { get; set; } = "EN";

    /// <summary>
    /// User-friendly display name (e.g., "Frontend Developer - Pleno").
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// Recommended document title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Recommended subject/expertise summary.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Comma-separated list of ATS keywords/skills.
    /// </summary>
    public string Keywords { get; set; } = string.Empty;

    /// <summary>
    /// Recommended comments/profile details.
    /// </summary>
    public string Comments { get; set; } = string.Empty;
}
