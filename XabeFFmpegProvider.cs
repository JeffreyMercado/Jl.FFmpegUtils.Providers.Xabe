using System.Diagnostics;
using Xabe.FFmpeg;

namespace Jl.FFmpegUtils.Providers.Xabe;

public class XabeFFmpegProvider : IFFmpegProvider
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
        return await Task.Run(() => FFmpeg.GetMediaInfo(filePath, cancellationToken)).ConfigureAwait(false);
    }

    public async Task<IConversionResult> ConvertAsync(string arguments, IObserver<DataReceivedEventArgs> outputDataObserver, CancellationToken cancellationToken = default)
    {
        var conversion = FFmpeg.Conversions.New();
        conversion.OnDataReceived += (_, e) => outputDataObserver.OnNext(e);
        try
        {
            // Xabe uses captured context, need to fix that
            var result = await Task.Run(() => conversion.Start(arguments, cancellationToken)).ConfigureAwait(false);
            outputDataObserver.OnCompleted();
            return new XabeConversionResult(result.StartTime, result.EndTime);
        }
        catch (Exception ex)
        {
            outputDataObserver.OnError(ex);
            throw;
        }
    }

    private record XabeConversionResult(DateTime StartTime, DateTime EndTime) : IConversionResult;
}
