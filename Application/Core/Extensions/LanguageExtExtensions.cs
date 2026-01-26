using LanguageExt;
using LanguageExt.Common;
using static Serilog.Log;

namespace Application.Core.Extensions;

public static class LanguageExtExtensions
{
    public static Either<Error, Unit> MapToUnit<T>(this Either<Error, T> either)
    {
        return either.Map(_ => Unit.Default);
    }
    
    public static async Task<Either<Error, Unit>> MapToUnitAsync<T>(this Task<Either<Error, T>> either)
    {
        return await either.MapAsync(_ => Unit.Default);
    }

    public static Either<Error, T> Log<T>(this Either<Error, T> either, string prefix)
    {
        if (either.IsLeft)
        {
            var error = either.LeftToSeq().First();
            Error("[{Prefix}] {Message}", prefix, error.Message);
        }
        
        return either;
    }

    public static async Task<Either<Error, T>> LogAsync<T>(this Task<Either<Error, T>> either, string prefix)
    {
        var eitherResult = await either;
        if (eitherResult.IsLeft)
        {
            var error = eitherResult.LeftToSeq().First();
            Error("[{Prefix}] {Message}", prefix, error.Message);
        }

        return eitherResult;
    }
}