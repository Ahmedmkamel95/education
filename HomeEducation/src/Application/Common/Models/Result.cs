using Newtonsoft.Json;

namespace HomeEducation.Application.Common.Models;

public class Result<T> where T : class
{
    internal Result(bool succeeded,T? data, string[] errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Data = data;
    }

    public bool Succeeded { get; init; }
    
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public T? Data { get; init; }
    public string[] Errors { get; init; }

    public static Result<T> Success(T? data)
    {
        return new Result<T>(true, data, Array.Empty<string>());
    }

    public static Result<T> Failure(string[] errors)
    {
        return new Result<T>(false, null, errors);
    }
}
