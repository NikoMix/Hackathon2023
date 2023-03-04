namespace Hackathon2023.Models;

using System;
using System.Collections.Generic;

/// <summary>
/// The participants request data.
/// </summary>
public class ParticipantsCallingRequestData
{
    /// <summary>
    /// Gets or sets the notified user object ids.
    /// </summary>
    public IEnumerable<string> ObjectIds { get; set; }

    /// <summary>
    /// Gets or sets the tenant id.
    /// </summary>
    public string TenantId { get; set; }
}