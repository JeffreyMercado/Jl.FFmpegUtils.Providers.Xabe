using System.Collections.Immutable;

namespace Jl.FFmpegUtils.Providers.Xabe;

public record XabeMediaInfo : IMediaInfo
{
    public XabeMediaInfo(IMediaSource source, global::Xabe.FFmpeg.IMediaInfo mediaInfo)
    {
        (Source, Duration, Size, CreationTime) = (source, mediaInfo.Duration, mediaInfo.Size, mediaInfo.CreationTime);
        Streams = mediaInfo.Streams.Select(x => XabeMediaStream.Create(source, x)).ToImmutableArray();
        VideoStreams = Streams.OfType<IVideoStream>().ToImmutableArray();
        AudioStreams = Streams.OfType<IAudioStream>().ToImmutableArray();
        SubtitleStreams = Streams.OfType<ISubtitleStream>().ToImmutableArray();
        DataStreams = Streams.OfType<IDataStream>().ToImmutableArray();
    }
    public IMediaSource Source { get; }
    public TimeSpan Duration { get; }
    public long Size { get; }
    public DateTime? CreationTime { get; }
    public IReadOnlyList<IMediaStream> Streams { get; }
    public IReadOnlyList<IVideoStream> VideoStreams { get; }
    public IReadOnlyList<IAudioStream> AudioStreams { get; }
    public IReadOnlyList<ISubtitleStream> SubtitleStreams { get; }
    public IReadOnlyList<IDataStream> DataStreams { get; }
}
