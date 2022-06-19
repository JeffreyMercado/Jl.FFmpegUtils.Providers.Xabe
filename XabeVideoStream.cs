namespace Jl.FFmpegUtils.Providers.Xabe;

public record XabeVideoStream : XabeMediaStream, IVideoStream
{
    public XabeVideoStream(IMediaSource source, global::Xabe.FFmpeg.IVideoStream stream)
        : base(source, MediaStreamType.Video, stream.Index, stream.Codec)
    {
        Width = stream.Width;
        Height = stream.Height;
        Ratio = stream.Ratio;
        PixelFormat = stream.PixelFormat;
        Framerate = stream.Framerate;
        Duration = stream.Duration;
        Bitrate = stream.Bitrate;
        Default = stream. Default;
        Forced = stream. Forced;
        Rotation = stream. Rotation;
    }
    public int Width { get; }
    public int Height { get; }
    public string Ratio { get; }
    public string PixelFormat { get; }
    public double Framerate { get; }
    public TimeSpan Duration { get; }
    public long Bitrate { get; }
    public int? Default { get; }
    public int? Forced { get; }
    public int? Rotation { get; }
}
