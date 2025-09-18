using ControlSystems.Objects.Contracts.Exceptions;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;

namespace ControlSystems.Objects.Dtos.DataAnnotations.Format;

public class CpfCnpjFormat : BaseAnnotation
{
    public CpfCnpjFormat(params object[]? parameters) : base(parameters) { }

    public override FieldError? Execute()
    {
        string cpfCnpj = new string(Value.ToString().Where(char.IsDigit).ToArray());

        if (cpfCnpj.Length == 11)
        {
            if (!ValidarCpf(cpfCnpj))
                return ReturnError(NameProperty, "CPF inválido.");
            else
                SetValue(cpfCnpj);
        }
        else if (cpfCnpj.Length == 14)
        {
            if (!ValidarCnpj(cpfCnpj))
                return ReturnError(NameProperty, "CNPJ inválido.");
            else
                SetValue(cpfCnpj);
        }
        else
        {
            return ReturnError(NameProperty, "O campo deve conter um CPF (11 dígitos) ou CNPJ (14 dígitos) válido.");
        }

        return null;
    }

    private bool ValidarCpf(string cpf)
    {
        if (cpf.Distinct().Count() == 1) return false;

        var digits = cpf.Select(c => c - '0').ToArray();

        int sum1 = 0;
        for (int i = 0; i < 9; i++)
            sum1 += digits[i] * (10 - i);

        int check1 = sum1 % 11;
        check1 = check1 < 2 ? 0 : 11 - check1;

        if (digits[9] != check1) return false;

        int sum2 = 0;
        for (int i = 0; i < 10; i++)
            sum2 += digits[i] * (11 - i);

        int check2 = sum2 % 11;
        check2 = check2 < 2 ? 0 : 11 - check2;

        return digits[10] == check2;
    }

    private bool ValidarCnpj(string cnpj)
    {
        if (cnpj.Distinct().Count() == 1) return false;

        var digits = cnpj.Select(c => c - '0').ToArray();

        int[] weight1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] weight2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int sum1 = 0;
        for (int i = 0; i < 12; i++)
            sum1 += digits[i] * weight1[i];

        int check1 = sum1 % 11;
        check1 = check1 < 2 ? 0 : 11 - check1;

        if (digits[12] != check1) return false;

        int sum2 = 0;
        for (int i = 0; i < 13; i++)
            sum2 += digits[i] * weight2[i];

        int check2 = sum2 % 11;
        check2 = check2 < 2 ? 0 : 11 - check2;

        return digits[13] == check2;
    }
}