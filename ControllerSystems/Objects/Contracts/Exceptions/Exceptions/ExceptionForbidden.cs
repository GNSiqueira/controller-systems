using ControlSystems.Objects.Contracts.Exceptions.Base;

namespace ControlSystems.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionForbidden : _ClientErrorException
{
    public ExceptionForbidden(string message) : base(403, message) { }
}
