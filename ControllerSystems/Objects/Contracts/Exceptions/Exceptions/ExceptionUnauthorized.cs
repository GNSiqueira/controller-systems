using ControlSystems.Objects.Contracts.Exceptions.Base;

namespace ControlSystems.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionUnauthorized : _ClientErrorException
{
    public ExceptionUnauthorized(string message) : base(401, message) { }
}
