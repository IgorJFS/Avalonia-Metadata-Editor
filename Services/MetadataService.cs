using System;
using System.IO;
using System.Threading.Tasks;
using Docs_Metadata_Editor.Models;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Docs_Metadata_Editor.Services;

/// <summary>
/// Service implementation for managing PDF metadata using PDFsharp.
/// </summary>
public class MetadataService : IMetadataService
{
    public Task<ResumeMetadata> LoadMetadataAsync(string filePath)
    {
        return Task.Run(() =>
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified PDF file does not exist.", filePath);
            }

            try
            {
                // Open in Modify mode so we can read and potentially write back
                using var document = PdfReader.Open(filePath, PdfDocumentOpenMode.Modify);
                
                var metadata = new ResumeMetadata
                {
                    Title = document.Info.Title ?? string.Empty,
                    Author = document.Info.Author ?? string.Empty,
                    Subject = document.Info.Subject ?? string.Empty,
                    Keywords = document.Info.Keywords ?? string.Empty
                };

                // Read custom Comments key if present
                if (document.Info.Elements.ContainsKey("/Comments"))
                {
                    metadata.Comments = document.Info.Elements.GetString("/Comments") ?? string.Empty;
                }
                else if (document.Info.Elements.ContainsKey("/Description"))
                {
                    metadata.Comments = document.Info.Elements.GetString("/Description") ?? string.Empty;
                }

                return metadata;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to read PDF metadata. The file may be corrupted or password-protected. Error: {ex.Message}", ex);
            }
        });
    }

    public Task SaveMetadataAsync(string sourceFilePath, string destinationFilePath, ResumeMetadata metadata)
    {
        return Task.Run(() =>
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException("The source PDF file does not exist.", sourceFilePath);
            }

            try
            {
                // Open the source PDF in Modify mode
                using var document = PdfReader.Open(sourceFilePath, PdfDocumentOpenMode.Modify);

                // Update standard metadata fields
                document.Info.Title = metadata.Title ?? string.Empty;
                document.Info.Author = metadata.Author ?? string.Empty;
                document.Info.Subject = metadata.Subject ?? string.Empty;
                document.Info.Keywords = metadata.Keywords ?? string.Empty;

                // Update custom Comments metadata field
                document.Info.Elements.SetString("/Comments", metadata.Comments ?? string.Empty);

                // Ensure target directory exists (for Save As/Download mode)
                var targetDir = Path.GetDirectoryName(destinationFilePath);
                if (!string.IsNullOrEmpty(targetDir) && !Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                // If destination is different and already exists, PdfSharp will overwrite it or we can delete it first
                // to prevent write sharing violations.
                if (!string.Equals(sourceFilePath, destinationFilePath, StringComparison.OrdinalIgnoreCase))
                {
                    if (File.Exists(destinationFilePath))
                    {
                        File.Delete(destinationFilePath);
                    }
                }

                // Save the PDF document
                document.Save(destinationFilePath);
            }
            catch (IOException ioEx)
            {
                throw new InvalidOperationException($"File access error. The file might be open in another application or write-protected. Error: {ioEx.Message}", ioEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save PDF metadata: {ex.Message}", ex);
            }
        });
    }
}
