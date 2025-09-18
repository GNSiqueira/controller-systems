using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Format;

public class PhoneFormat : BaseAnnotation
{
    public PhoneFormat(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override FieldError? Execute()
    {
        string valor = new string(Value.ToString()?.Where(char.IsDigit).ToArray());

        if (valor.Length != 10 && valor.Length != 11)
            return ReturnError(NameProperty, "Telefone inválido.");
        if (valor.Length == 11 && valor[2] != '9')
            return ReturnError(NameProperty, "Número de celular inválido.");
        SetValue(valor);
        return null;
    }
}