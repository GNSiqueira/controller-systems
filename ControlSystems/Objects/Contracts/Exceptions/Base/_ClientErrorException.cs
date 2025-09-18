using Microsoft.AspNetCore.Mvc;

namespace ControlSystems.Objects.Contracts.Exceptions.Base;

public abstract class _ClientErrorException : ApiException
{
    public _ClientErrorException(int status, string message) : base(status, message)
    {
        if (!(status >= 400 && status <= 499))
            throw new Exception("Erro: O status tem que estár entre 400 e 499");
    }
}
