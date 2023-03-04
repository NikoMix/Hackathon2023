namespace Hackathon2023.Utilities
{
    /// <summary>
    /// Since Microsoft.Skype.Bots.Media is .Net 4.6ish - create custom enum for .net core
    /// </summary>
    public enum MediaSendStatus
    {
        /// <summary>Media cannot be sent</summary>
        Inactive,
        /// <summary>Media can be sent</summary>
        Active,
    }
}
