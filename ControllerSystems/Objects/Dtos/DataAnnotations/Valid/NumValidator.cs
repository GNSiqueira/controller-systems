using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Valid;

public class NumValidator : BaseAnnotation
{
    public NumValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null || parameters.Length == 0)
            throw new ArgumentNullException("Essa função precisa de parâmetros.");
    }

    public override FieldError? Execute()
    {
        if (!decimal.TryParse(Value.ToString(), out var valor))
        {
            return ReturnError(NameProperty, "O valor informado não é numérico.");
        }

        if (!decimal.TryParse(Parameters[0].ToString(), out var minValue))
        {
            return ReturnError(NameProperty, "O parâmetro mínimo é inválido.");
        }

        if (valor < minValue)
        {
            return ReturnError(NameProperty, $"O valor {valor} é menor que o mínimo permitido ({minValue}).");
        }

        return null;

    }
}