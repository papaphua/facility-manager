namespace FacilityManager.Domain.Core.Results;

public sealed class Result<TValue> : Result
{
    private Result(TValue value)
    {
        Value = value;
    }

    private Result(Error error) : base(error)
    {
        Value = default;
    }

    public TValue? Value { get; }

    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(error);
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(value);
    }

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(value);
    }

    public new static Result<TValue> Failure(Error error)
    {
        return new Result<TValue>(error);
    }
}