using ControlSystems.Objects.Contracts.Exceptions.Base;

namespace ControlSystems.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionConflict : _ValidationException
{
    public ExceptionConflict(string message, List<FieldError>? errors) : base(409, message, errors) { }
    public ExceptionConflict(string message) : base(409, message, null) { }
}
