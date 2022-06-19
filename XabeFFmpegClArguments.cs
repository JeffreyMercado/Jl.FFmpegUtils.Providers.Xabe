namespace Jl.FFmpegUtils.Providers.Xabe;

public static class XabeFFmpegClArguments
{
    private static readonly Lazy<IMediaInfoProvider> providerFactory = new Lazy<IMediaInfoProvider>(() => new XabeMediaInfoProvider());

    public static IFFmpegClArgumentsBuilder CreateBuilder() => FFmpegClArguments.Builder.Create(providerFactory.Value);
}
