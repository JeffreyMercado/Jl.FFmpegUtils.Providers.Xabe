namespace Jl.FFmpegUtils.Providers.Xabe;

public record XabeSubtitleStream : XabeMediaStream, ISubtitleStream
{
    public XabeSubtitleStream(IMediaSource source, global::Xabe.FFmpeg.ISubtitleStream stream)
        : base(source, MediaStreamType.Subtitle, stream.Index, stream.Codec)
    {
        Default = stream.Default;
        Forced = stream.Forced;
        Language = stream.Language;
        Title = stream.Title;
    }
    public int? Default { get; }
    public int? Forced { get; }
    public string? Language { get; }
    public string? Title { get; }
}
