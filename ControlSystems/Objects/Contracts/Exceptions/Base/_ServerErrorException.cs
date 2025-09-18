using Microsoft.AspNetCore.Mvc;

namespace ControlSystems.Objects.Contracts.Exceptions.Base;

public abstract class _ServerErrorException : ApiException
{
    public _ServerErrorException(int status, string message) : base(status, message)
    {
        if (!(status >= 500 && status <= 599))
            throw new Exception("Erro: O status tem que estár entre 500 e 599");
    }

}
