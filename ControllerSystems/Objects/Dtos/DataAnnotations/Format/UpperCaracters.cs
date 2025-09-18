using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Format;

public class UpperCaracters : BaseAnnotation
{
    public UpperCaracters(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override FieldError? Execute()
    {
        string valor = Value?.ToString().ToUpper();

        SetValue(valor);

        return null;
    }
}