namespace Jl.FFmpegUtils.Providers.Xabe;

public abstract record XabeMediaStream(IMediaSource Source, MediaStreamType Type, int Index, string Codec) : IMediaStream
{
    public static IMediaStream Create(IMediaSource source, global::Xabe.FFmpeg.IStream stream)
    {
        return stream switch
        {
            global::Xabe.FFmpeg.IVideoStream videoStream => new XabeVideoStream(source, videoStream),
            global::Xabe.FFmpeg.IAudioStream audioStream => new XabeAudioStream(source, audioStream),
            global::Xabe.FFmpeg.ISubtitleStream subtitleStream => new XabeSubtitleStream(source, subtitleStream),
            //global::Xabe.FFmpeg.IDataStream dataStream => new XabeDataStream(source, dataStream),
            _ => throw new ArgumentException($"Unexpected stream type: {stream.StreamType}"),
        };
    }
}
