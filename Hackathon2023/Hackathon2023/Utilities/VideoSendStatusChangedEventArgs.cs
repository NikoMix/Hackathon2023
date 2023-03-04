using Microsoft.AspNetCore.Mvc.Formatters;
using System.Collections.Generic;
using System;

namespace Hackathon2023.Utilities
{
    /// <summary>Event arguments of a VideoSendStatusChanged event. Port from the Microsoft.Skype.Bots.Media Nuget - for .Net Core</summary>
    public class VideoSendStatusChangedEventArgs : EventArgs
    {
        /// <summary>The media send status.</summary>
        public MediaSendStatus MediaSendStatus { get; set; }

        /// <summary>
        /// MediaType of the video socket raising the event. This could be Video or Vbss.
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// The preferred video source format if raw video is sent.
        /// </summary>
        //public VideoFormat PreferredVideoSourceFormat { get; set; }

        /// <summary>
        /// The preferred video source format if encoded video is sent.
        /// </summary>
        //public VideoFormat[] PreferredEncodedVideoSourceFormats { get; set; }

        /// <summary>
        /// The 0-based ID of the socket that is raising this event. This socket ID can be used in multiview
        /// (ie. more than 1 video socket) to determine which video socket is raising this event. The socket ID
        /// property will be present in both single view and multiview cases. The ID maps to the order in which
        /// the video sockets are provided to the Microsoft.Skype.Bots.Media.MediaPlatform (or IMediaPlatform) API
        /// CreateMediaConfiguration. Eg. If the collection of IVideoSocket objects in the CreateMediaConfiguration API
        /// contains { socketA, socketB, socketC }, the sockets will have the ID mapping of: 0 for socketA, 1
        /// for socketB and 2 for socketC.
        /// </summary>
        public int SocketId { get; set; }

        /// <summary>
        /// Provides EventArgs details by overriding the default ToString().
        /// </summary>
        /// <returns></returns>
        //public override string ToString() => string.Format("VideoSendStatusChangedEventArgs: SocketId: {0}, MediaType: {1}, MediaSendStatus: {2},", this.SocketId, this.MediaType, this.MediaSendStatus) + string.Format(" PreferredVideoSourceFormat: {0},", this.PreferredVideoSourceFormat) + " VideoFormats: " + (this.PreferredEncodedVideoSourceFormats == null ? "null" : string.Join("|", ((IEnumerable<VideoFormat>)this.PreferredEncodedVideoSourceFormats).Select<VideoFormat, string>((Func<VideoFormat, string>)(f => f.ToString()))));
    }
}
