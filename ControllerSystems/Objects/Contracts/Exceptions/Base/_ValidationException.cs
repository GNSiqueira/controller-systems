namespace ControlSystems.Objects.Contracts.Exceptions.Base;

public abstract class _ValidationException : ApiException
{
    public _ValidationException(int status, string message, List<FieldError>? errors) : base(status, message, errors) { }
}
