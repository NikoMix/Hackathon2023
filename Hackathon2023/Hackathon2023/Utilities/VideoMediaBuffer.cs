using System;

namespace Hackathon2023.Utilities
{
    /// <summary>
    /// Represents an unmanaged buffer containing video media data.
    /// </summary>
    public abstract class VideoMediaBuffer : IDisposable
    {
        /// <summary>Pointer to the unmanaged media buffer.</summary>
        /// <remarks>
        /// For the H264 video format, Data points to the start code of
        /// the first NALU of the frame and the whole buffer contains
        /// all the NALUs of the frame in sequence separated by their
        /// start codes. The frame data is an unencrypted depacketized
        /// H.264 elementary stream. B frames and SEI messages are not supported.
        /// <para>
        /// Supported NALU types are 7 (SPS), 8 (PPS), 5 (IDR), and 1 (non-IDR).
        /// NALU start code prefixes can be either 0x000001 (3 bytes)
        /// or 0x00000001 (4 bytes).
        /// Fragmentation and packetization of NALUs is automatically
        /// handled by the platform.
        /// </para>
        /// <para>
        /// Encoded H.264 frames sent by the bot must not contain more than
        /// 100 NALUs.
        /// </para>
        /// <para>
        /// When receiving video in the H264 format, the platform ensures
        /// the application always receives decodable, full frames.
        /// </para>
        /// </remarks>
        //public IntPtr Data { get; protected set; }

        /// <summary>The length of data in the media buffer.</summary>
        //public long Length { get; protected set; }

        /// <summary>VideoFormat of the video media buffer.</summary>
        //public VideoFormat VideoFormat { get; protected set; }

        /// <summary>
        /// Cropping rectangle coordinates of the video media buffer.
        /// </summary>
        /// <remarks>
        /// This property is available only on incoming video frames and must be null for video buffers sent
        /// </remarks>
        //internal CroppingRectangle CroppingRectangle { get; set; }

        /// <summary>
        /// Original VideoFormat of the buffer when it was sourced. It is only used when receiving video buffers
        /// via the IVideoSocket.VideoMediaReceived event handler, in which case the VideoMediaBuffer.VideoFormat
        /// property may have different Width and Height values than the OriginalVideoFormat property, which represents
        /// the original format of the buffer. The reason is that the buffer may have been resized before being
        /// transmitted, so the original Width and Height may have been resized. If the Width and Height
        /// properties of OriginalVideoFormat differ from the VideoFormat property, the consumer of the
        /// VideoMediaBuffer raised in the VideoMediaReceived event should resize the buffer to fit the
        /// OriginalVideoFormat size.
        /// When sending buffers via the IVideoSocket.Send API, this property should always be null.
        /// </summary>
        //public VideoFormat OriginalVideoFormat { get; protected set; }

        /// <summary>
        /// Stride of the video buffer. This property is optional when sourcing video
        /// buffers that are sent via the IVideoSocket.Send API.
        /// Stride (also called pitch) represents the number of bytes it takes to read one row
        /// of pixels in memory. It may differ from the width depending on the color format.
        /// </summary>
        //public int Stride { get; protected set; }

        /// <summary>
        /// MediaSourceId (MSI) of the video buffer. Within group or conference video calls, the MSI value
        /// identifies the video media source.
        /// This property is populated by the Real-Time Media Platform for Bots on received video buffers. When
        /// sending buffers via the IVideoSocket.Send API, this property is unused.
        /// </summary>
        public uint MediaSourceId { get; protected set; }

        /// <summary>
        /// Timestamp of when the media content was sourced, in 100-ns units.
        /// When sourcing media buffers, this property should be set using
        /// the value from the MediaPlatform.GetCurrentTimestamp() API.
        /// </summary>
        public long Timestamp { get; protected set; }

        /// <summary>Reserved for future use.</summary>
        //public AlphaMask AlphaMask { get; protected set; }

        /// <summary>Disposes the object.</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources held by the buffer object.
        /// Must be implemented in the derived class.
        /// </summary>
        /// <param name="disposing">
        /// If true, both managed and unmanaged resources can be
        /// disposed.
        /// If false, only unmanaged resources can be disposed.</param>
        protected abstract void Dispose(bool disposing);
    }
}
