namespace Jl.FFmpegUtils.Providers.Xabe;

public static class XabeFFmpeg
{
    private static readonly Lazy<IFFmpegProvider> providerFactory = new Lazy<IFFmpegProvider>(() => new XabeFFmpegProvider());

    public static IFFmpegClArgumentsBuilder CreateArgumentsBuilder() => FFmpegClArguments.CreateBuilder(providerFactory.Value);

    public static async Task<IFFmpegConversionResult> ConvertAsync(IFFmpegConversion conversion, CancellationToken cancellationToken = default) =>
        await conversion.ConvertAsync(providerFactory.Value, cancellationToken).ConfigureAwait(false);
}
