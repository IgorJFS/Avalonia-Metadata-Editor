using System.Collections.Generic;
using System.Threading.Tasks;
using Docs_Metadata_Editor.Models;

namespace Docs_Metadata_Editor.Services;

/// <summary>
/// Interface for loading preconfigured ATS metadata profiles.
/// </summary>
public interface IProfileService
{
    /// <summary>
    /// Retrieves all available ATS profiles.
    /// </summary>
    Task<IEnumerable<AtsProfile>> GetProfilesAsync();
}
