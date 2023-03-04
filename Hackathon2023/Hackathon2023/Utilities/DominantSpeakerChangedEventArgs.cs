using System.Collections.Generic;
using System;

namespace Hackathon2023.Utilities
{
    /// <summary>Event arguments of the DominantSpeakerChanged event. - Port from Microsoft.Skype.Bots.Media due to .Net Core</summary>
    public class DominantSpeakerChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Constant value which indicates there is no dominant speaker in the conference.
        /// </summary>
        public const uint None = 4294967295;

        /// <summary>
        /// History of the dominant speakers.
        /// However, DominantSpeakerHistory won't contain any element to indicate the absence of dominant speaker in the conference.
        /// </summary>
        public uint[] DominantSpeakerHistory { get; set; }

        /// <summary>
        /// Current dominant speaker in the conference.
        /// The value is the MediaSourceId (MSI) of the dominant speaker in the conference.
        /// If there is no dominant speaker in the conference this value will be None (0xFFFFFFFF).
        /// </summary>
        public uint CurrentDominantSpeaker { get; set; }

        /// <summary>
        /// Provides EventArgs details by overriding the default ToString().
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("DominantSpeakerChangedEventArgs: CurrentDominantSpeaker: {0},", this.CurrentDominantSpeaker) + " DominantSpeakerHistory: " + (this.DominantSpeakerHistory == null ? "null" : string.Join<uint>(",", (IEnumerable<uint>)this.DominantSpeakerHistory));
    }
}
