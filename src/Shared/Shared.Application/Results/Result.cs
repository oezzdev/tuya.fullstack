using System.Diagnostics.CodeAnalysis;

namespace Shared.Application.Results;

public record Result
{
    [MemberNotNullWhen(false, nameof(Error))]
    public virtual bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, default);
    public static Result Failure(Error error) => new(default, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public record Result<T> : Result
{
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess => base.IsSuccess;
    public T? Value { get; }
    private Result(T value) : base(true, default) => Value = value;
    private Result(Error error) : base(default, error) => Value = default;

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
}