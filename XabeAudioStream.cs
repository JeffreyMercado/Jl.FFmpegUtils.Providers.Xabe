namespace Jl.FFmpegUtils.Providers.Xabe;

public record XabeAudioStream : XabeMediaStream, IAudioStream
{
    public XabeAudioStream(IMediaSource source, global::Xabe.FFmpeg.IAudioStream stream)
        : base(source, MediaStreamType.Audio, stream.Index, stream.Codec)
    {
        SampleRate = stream.SampleRate;
        Channels = stream.Channels;
        Duration = stream.Duration;
        Bitrate = stream.Bitrate;
        Default = stream.Default;
        Forced = stream.Forced;
        Language = stream.Language;
        Title = stream.Title;
    }

    public int SampleRate { get; }
    public int Channels { get; }
    public TimeSpan Duration { get; }
    public long Bitrate { get; }
    public int? Default { get; }
    public int? Forced { get; }
    public string? Language { get; }
    public string? Title { get; }
}
