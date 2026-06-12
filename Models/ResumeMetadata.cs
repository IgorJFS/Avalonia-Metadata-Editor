namespace Docs_Metadata_Editor.Models;

/// <summary>
/// Represents the ATS-relevant metadata fields of a resume.
/// </summary>
public class ResumeMetadata
{
    /// <summary>
    /// The document title (typically the candidate's name or target role).
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The document author (candidate's name).
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// The document subject (core expertise or executive summary).
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Comma-separated list of hard/soft skills (crucial for ATS filtering).
    /// </summary>
    public string Keywords { get; set; } = string.Empty;

    /// <summary>
    /// Additional comments or profile description.
    /// </summary>
    public string Comments { get; set; } = string.Empty;
}
