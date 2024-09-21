namespace MoneyMeLoan.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IDictionary<string, string[]> validationErrors)
    {
        Succeeded = succeeded;
        Errors = Array.Empty<string>();
        ValidationErrors = validationErrors;
    }

    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }
    public bool ValidationFailed => ValidationErrors?.Any() ?? false;
    public string[] Errors { get; init; } = Array.Empty<string>();
    public bool HasErrors => Errors.Any();
    public IDictionary<string, string[]>? ValidationErrors { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }

    public static Result Failure(string error)
    {
        return new Result(false, new List<string> { error });
    }

    public static Result Failure(IDictionary<string, string[]> errors)
    {
        var errorList = errors.Select(x => $"{string.Join(",", x.Value)}");
        return new Result(false, errorList);
    }

    public static Result ValidationFailure(IDictionary<string, string[]> validationErrors)
    {
        return new Result(false, validationErrors);
    }
}

public class Result<T> : Result
{
    internal Result(bool succeeded, IEnumerable<string> errors, T data)
        : base(succeeded, errors)
    {
        Data = data;
    }

    public Result(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
    }

    public T? Data { get; init; }


    public static Result<T> Success(T data)
    {
        return new Result<T>(true, Array.Empty<string>(), data);
    }

    public static Result<T> Failure(string error, T data)
    {
        return new Result<T>(false, new List<string> { error }, data);
    }



    public static new Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, errors);
    }

    public static new Result<T> Failure(string error)
    {
        return new Result<T>(false, new List<string> { error });
    }

    public static new Result<T> Failure(IDictionary<string, string[]> errors)
    {
        var errorList = errors.Select(x => $"{string.Join(",", x.Value)}");
        return new Result<T>(false, errorList);
    }
}
