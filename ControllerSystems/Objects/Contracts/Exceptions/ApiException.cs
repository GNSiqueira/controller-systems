using Microsoft.AspNetCore.Mvc;

namespace ControlSystems.Objects.Contracts.Exceptions;

public abstract class ApiException : Exception
{
    public int StatusCode { get; private set; }
    public List<FieldError>? Errors { get; private set; }

    protected ApiException(int status, string message, List<FieldError>? errors = null) : base(message)
    {
        StatusCode = status;
        Errors = errors;
    }

}