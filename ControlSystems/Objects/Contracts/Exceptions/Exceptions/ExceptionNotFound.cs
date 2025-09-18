using ControlSystems.Objects.Contracts.Exceptions.Base;

namespace ControlSystems.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionNotFound : _ClientErrorException
{
    public ExceptionNotFound(string message) : base(404, message) { }
}
