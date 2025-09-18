using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Valid;

public class QtdCaractersValidator : BaseAnnotation
{

    public QtdCaractersValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parametros");
    }

    public override FieldError? Execute()
    {
        var qtdValor = Value?.ToString()?.Length;
        if (Parameters != null)
        {
            foreach (var item in Parameters)
            {
                if (qtdValor == (int)item)
                    return null;
            }
        }

        return ReturnError(NameProperty, "Essa quantidade de caracteres não é válida!");

    }
}