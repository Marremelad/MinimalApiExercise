using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MinimalApiExercise.ValidationAttributes;

public class SwedishPhoneNumber : ValidationAttribute
{
    private static readonly Regex PhoneRegex = new Regex(@"^\+46\d{9}$|^0\d{9}$");

    public override bool IsValid(object? value)
    {
        if (value == null) return true;
        
        var phone = value.ToString();
        return phone != null && PhoneRegex.IsMatch(phone);
    }
}