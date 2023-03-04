namespace Hackathon2023.Services.Graph;

using System;
using Hackathon2023.Options;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

public class AudioRecordingConstants
{
    public AudioRecordingConstants(IOptions<BotOptions> botOptions)
    {
        Speech = new MediaInfo
        {
            Uri = new Uri(botOptions.Value.BotBaseUrl, "audio/speech.wav").ToString(),
            ResourceId = Guid.NewGuid().ToString(),
        };

        PleaseRecordYourMessage = new MediaInfo
        {
            Uri = new Uri(botOptions.Value.BotBaseUrl, "audio/please-record-your-message.wav").ToString(),
            ResourceId = Guid.NewGuid().ToString(),
        };
    }

    public readonly MediaInfo Speech;
    public readonly MediaInfo PleaseRecordYourMessage;
}
