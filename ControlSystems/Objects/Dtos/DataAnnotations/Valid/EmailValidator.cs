using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;
using System.Text.RegularExpressions;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Valid;

public class EmailValidator : BaseAnnotation
{
    public EmailValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    private static readonly Regex _emailRegex = new Regex(
    @"^[\w\.-]+@[\w\.-]+\.\w{2,}$",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public override FieldError? Execute()
    {
        string valor = Value?.ToString();

        if (!_emailRegex.IsMatch(valor))
            return ReturnError(NameProperty, "Email inválido.");

        return null;
    }
}
