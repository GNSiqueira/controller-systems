using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Valid;

public class NullOrEmpty : BaseAnnotation
{
    public NullOrEmpty(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override FieldError? Execute()
    {
        string? valor = Value?.ToString();

        if (string.IsNullOrWhiteSpace(valor))
        {
            if (NameProperty == null)
                throw new ArgumentNullException(nameof(NameProperty), "O nome da propriedade não pode ser nulo.");
            return ReturnError(NameProperty, "O campo não pode ser nulo ou vazio.");
        }

        return null;
    }
}