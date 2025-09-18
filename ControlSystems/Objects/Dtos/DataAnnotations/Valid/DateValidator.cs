using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Valid;

public class DateValidator : BaseAnnotation
{
    public DateValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa função precisa de parâmetros: mínimo e/ou máximo.");
    }

    public override FieldError? Execute()
    {
        if (Value is not DateTime date)
        {
            return ReturnError(NameProperty, "Valor informado não é uma data válida.");
        }

        DateTime? minDate = null;

        foreach (var param in Parameters)
        {
            if (param is DateTime dt)
            {
                minDate = dt;
                break;
            }
            else if (param is string str && DateTime.TryParse(str, out var parsed))
            {
                minDate = parsed;
                break;
            }
            else
            {
                return ReturnError(NameProperty, "Parâmetro inválido para data mínima.");
            }
        }

        if (minDate.HasValue && date < minDate.Value)
        {
            return ReturnError(NameProperty, $"A data não pode ser anterior a {minDate:dd/MM/yyyy}.");
        }
        return null;
    }
}