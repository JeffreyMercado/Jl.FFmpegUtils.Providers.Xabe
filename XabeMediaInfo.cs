using System.Collections.Immutable;

namespace Jl.FFmpegUtils.Providers.Xabe;

public record XabeMediaInfo : IMediaInfo
{
    public XabeMediaInfo(IMediaSource source, global::Xabe.FFmpeg.IMediaInfo mediaInfo)
    {
        (Source, Duration, Size, CreationTime) = (source, mediaInfo.Duration, mediaInfo.Size, mediaInfo.CreationTime);
        var streams = ImmutableArray.CreateBuilder<IMediaStream>();
        var videoStreams = ImmutableArray.CreateBuilder<IVideoStream>();
        var audioStreams = ImmutableArray.CreateBuilder<IAudioStream>();
        var subtitleStreams = ImmutableArray.CreateBuilder<ISubtitleStream>();

        foreach (var stream in mediaInfo.Streams)
        {
            var m = XabeMediaStream.Create(source, stream);
            streams.Add(m);
            switch (m)
            {
                case IVideoStream v:
                    videoStreams.Add(v);
                    break;
                case IAudioStream a:
                    audioStreams.Add(a);
                    break;
                case ISubtitleStream s:
                    subtitleStreams.Add(s);
                    break;
            }
        }

        Streams = streams.ToImmutable();
        VideoStreams = videoStreams.ToImmutable();
        AudioStreams = audioStreams.ToImmutable();
        SubtitleStreams = subtitleStreams.ToImmutable();
    }
    public IMediaSource Source { get; }
    public TimeSpan Duration { get; }
    public long Size { get; }
    public DateTime? CreationTime { get; }
    public IReadOnlyList<IMediaStream> Streams { get; }
    public IReadOnlyList<IVideoStream> VideoStreams { get; }
    public IReadOnlyList<IAudioStream> AudioStreams { get; }
    public IReadOnlyList<ISubtitleStream> SubtitleStreams { get; }
}
