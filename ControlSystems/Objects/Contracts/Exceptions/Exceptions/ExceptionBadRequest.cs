using ControlSystems.Objects.Contracts.Exceptions.Base;

namespace ControlSystems.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionBadRequest : _ValidationException
{
    public ExceptionBadRequest(string message, List<FieldError>? errors) : base(400, message, errors) { }

    public ExceptionBadRequest(string message) : base(400, message, null)
    {
    }
}
