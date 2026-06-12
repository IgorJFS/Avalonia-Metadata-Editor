using System.Threading.Tasks;
using Docs_Metadata_Editor.Models;

namespace Docs_Metadata_Editor.Services;

/// <summary>
/// Interface for reading and writing document metadata.
/// </summary>
public interface IMetadataService
{
    /// <summary>
    /// Loads metadata from the specified PDF file.
    /// </summary>
    /// <param name="filePath">Absolute path to the PDF file.</param>
    /// <returns>A ResumeMetadata object containing the loaded properties.</returns>
    Task<ResumeMetadata> LoadMetadataAsync(string filePath);

    /// <summary>
    /// Saves the metadata to the specified PDF file.
    /// </summary>
    /// <param name="sourceFilePath">The original loaded PDF file path.</param>
    /// <param name="destinationFilePath">The file path to save to (same as source for Overwrite, different for Save Copy).</param>
    /// <param name="metadata">The metadata properties to save.</param>
    Task SaveMetadataAsync(string sourceFilePath, string destinationFilePath, ResumeMetadata metadata);
}
