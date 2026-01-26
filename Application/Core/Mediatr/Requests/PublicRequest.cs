using System.Text.Json.Serialization;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace Application.Core.Mediatr.Requests;

public class PublicRequest<T> : IRequest<Either<Error, T>>
{
    [JsonIgnore]
    public bool IsSystemRequest { get; set; }
    
    [JsonIgnore]
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}