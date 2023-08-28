using System.Text.RegularExpressions;

namespace IsbnValidationTool;

public abstract class IsbnValidator
{
	protected string _regexPattern = @"(\b978(?:-?\d){10}\b)|(\b978(?:-?\d){9}(?:-?X|x))|(\b(?:-?\d){10})\b|(\b(?:-?\d){9}(?:-?X|x)\b)";
	public abstract bool ValidateIsbn(int[] digits);
	public abstract bool ValidateAndConvert(int[] digits, out string newIsbn);
	protected abstract int CalculateCheckDigit(int[] digits);

	public bool Validate(string isbnString)
	{
		ConvertStringToDigits(isbnString, out int[]? digits);
		if (digits == null)
		{
			return false;
		}
		return ValidateIsbn(digits);
	}

	protected bool ConvertStringToDigits(string isbnString, out int[]? digits)
	{
		var match = Regex.Match(isbnString, _regexPattern);
		if (!match.Success)
		{
			digits = null;
			return false;
		}
		isbnString = isbnString.Replace("-", "");
		digits = new int[isbnString.Length];
		for (int i = 0; i < digits.Length; i++)
		{
			if (isbnString[i] == 'X')
				digits[i] = 10;
			else
				digits[i] = (int)char.GetNumericValue(isbnString[i]);
		}
		return true;
	}

}