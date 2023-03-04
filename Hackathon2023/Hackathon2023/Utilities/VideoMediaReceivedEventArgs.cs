using Microsoft.AspNetCore.Mvc.Formatters;
using System;

namespace Hackathon2023.Utilities
{
    /// <summary>Event arguments of a VideoMediaReceived event.</summary>
    public class VideoMediaReceivedEventArgs : EventArgs
    {
        /// <summary>The received video media buffer.</summary>
        public VideoMediaBuffer Buffer { get; set; }

        /// <summary>
        /// The 0-based ID of the socket that is raising this event. This socket ID can be used in multiview
        /// (ie. more than 1 video socket) to determine which video socket is raising this event. The socket ID
        /// property will be present in both single view and multiview cases. The ID maps to the order in which
        /// the video sockets are provided to the Microsoft.Skype.Bots.Media.MediaPlatform (or IMediaPlatform) API
        /// CreateMediaConfiguration. Eg. If the collection of IVideoSocket objects in the CreateMediaConfiguration
        /// API contains { socketA, socketB, socketC }, the sockets will have the ID mapping of: 0 for socketA,
        /// 1 for socketB and 2 for socketC.
        /// </summary>
        public int SocketId { get; set; }

        /// <summary>
        /// MediaType of the video socket. This could be Video or Vbss. The MediaType is set after
        /// the socket is passed to the CreateMediaConfiguration API. It may also be set via the
        /// VideoSocketSettings during socket creation.
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Provides EventArgs details by overriding the default ToString().
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("VideoMediaReceivedEventArgs: SocketId: {0}, MediaType: {1}", (object)this.SocketId, (object)this.MediaType);
    }
}
