namespace Jl.FFmpegUtils.Providers.Xabe;

public static class XabeFFmpeg
{
    public const string FFmpegExecutableName = "ffmpeg";
    public const string FFprobeExecutableName = "ffprobe";
    private static readonly Lazy<IFFmpegProvider> providerFactory = new Lazy<IFFmpegProvider>(() => new XabeFFmpegProvider());

    public static void Configure(Action<XabeSettings> config)
    {
        var settings = new XabeSettings();
        config?.Invoke(settings);
        global::Xabe.FFmpeg.FFmpeg.SetExecutablesPath(
            settings.FFmpegDirectory,
            settings.FFmpegExecutableName ?? FFmpegExecutableName,
            settings.FFprobeExecutableName ?? FFprobeExecutableName
        );
    }

    public static IFFmpegClArgumentsBuilder CreateArgumentsBuilder() => FFmpegClArguments.CreateBuilder(providerFactory.Value);

    public static async Task<IFFmpegConversionResult> ConvertAsync(IFFmpegConversion conversion, CancellationToken cancellationToken = default) =>
        await conversion.ConvertAsync(providerFactory.Value, cancellationToken).ConfigureAwait(false);
}

public class XabeSettings
{
    public string? FFmpegDirectory { get; set; }
    public string? FFmpegExecutableName { get; set; } = XabeFFmpeg.FFmpegExecutableName;
    public string? FFprobeExecutableName { get; set; } = XabeFFmpeg.FFprobeExecutableName;
}
