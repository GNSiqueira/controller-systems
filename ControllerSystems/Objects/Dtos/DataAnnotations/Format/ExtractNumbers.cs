using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Format;

public class ExtractNumbers : BaseAnnotation
{
    public ExtractNumbers(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }

    public override FieldError? Execute()
    {
        string valor = new string(Value.ToString()?.Where(char.IsDigit).ToArray());

        if (string.IsNullOrWhiteSpace(valor))
            return ReturnError(NameProperty, "O campo não pode ser nulo ou vazio.");
        SetValue(valor);
        return null;
    }
}