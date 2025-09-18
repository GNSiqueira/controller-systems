using System.Reflection;
using ControlSystems.Objects.Contracts.Exceptions;
namespace ControlSystems.Objects.Dtos.DataAnnotations.Base;

[AttributeUsage(AttributeTargets.Property)]
public abstract class BaseAnnotation : Attribute
{
    private PropertyInfo _property;
    private object _value;

    public string ErrorMessage { get; set; } = null!;

    public object[]? Parameters { get; set; }

    protected object Value
    {
        get => GetValue();
        set => SetValue(value);
    }

    protected string NameProperty;

    public BaseAnnotation(params object[]? parameters)
    {
        Parameters = parameters;
    }

    public void Initialize(PropertyInfo property, object value)
    {
        _property = property;
        NameProperty = property.Name;
        _value = value;
    }

    public abstract FieldError? Execute();

    protected object GetValue()
    {
        return _property.GetValue(_value);
    }

    protected void SetValue(object newValue)
    {
        _property.SetValue(_value, newValue);
    }

    protected FieldError ReturnError(string field, string? mensage = null)
    {
        if (ErrorMessage is not null)
            return new FieldError { Field = char.ToLower(field[0]) + field.Substring(1), Message = ErrorMessage };

        if (mensage is not null)
            return new FieldError { Field = char.ToLower(field[0]) + field.Substring(1), Message = mensage };

        return new FieldError { Field = char.ToLower(field[0]) + field.Substring(1), Message = "Erro padrão de anotação." };
    }
}
