using Xabe.FFmpeg;

namespace Jl.FFmpegUtils.Providers.Xabe;

public class XabeMediaInfoProvider : IMediaInfoProvider
{
    public async Task<IMediaInfo> GetMediaInfoAsync(IMediaSource source, CancellationToken cancellationToken = default)
    {
        switch (source)
        {
            case IFileMediaSource fileSource:
                var mediaInfo = await GetMediaInfoAsync(fileSource.FilePath, cancellationToken).ConfigureAwait(false);
                return new XabeMediaInfo(source, mediaInfo);
            default:
                throw new NotSupportedException($"Unrecognized media source");
        }
    }

    private async Task<global::Xabe.FFmpeg.IMediaInfo> GetMediaInfoAsync(string filePath, CancellationToken cancellationToken)
    {
        return await FFmpeg.GetMediaInfo(filePath, cancellationToken).ConfigureAwait(false);
    }
}
