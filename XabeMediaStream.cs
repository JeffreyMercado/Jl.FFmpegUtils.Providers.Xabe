namespace Jl.FFmpegUtils.Providers.Xabe;

public abstract record XabeMediaStream(IMediaSource Source, MediaStreamType MediaType, int Index, string CodecName) : IMediaStream
{
    public static IMediaStream Create(IMediaSource source, global::Xabe.FFmpeg.IStream stream)
    {
        return stream switch
        {
            global::Xabe.FFmpeg.IVideoStream videoStream => new XabeVideoStream(source, videoStream),
            global::Xabe.FFmpeg.IAudioStream audioStream => new XabeAudioStream(source, audioStream),
            global::Xabe.FFmpeg.ISubtitleStream subtitleStream => new XabeSubtitleStream(source, subtitleStream),
            _ => throw new ArgumentException($"Unexpected stream type: {stream.StreamType}"),
        };
    }
}
