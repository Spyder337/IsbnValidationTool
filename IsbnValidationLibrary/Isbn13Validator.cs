using System.Text;
using System.Text.RegularExpressions;

namespace IsbnValidationTool;

public class Isbn13Validator : IsbnValidator
{
	public override bool ValidateIsbn(int[] digits)
	{
		int value = 0;
		int checkDigit = digits[12];
		for (int i = 0; i < 12; i++)
		{
			if (i % 2 != 0)
			{
				value += digits[i] * 3;
			}
			else
			{
				value += digits[i];
			}
		}

		int remainder = value % 10;
		if (remainder == 0 && checkDigit == 0)
			return true;
		else
			return 10 - remainder == checkDigit;
	}

	protected override int CalculateCheckDigit(int[] digits)
	{
		int sum = 0;
		for (int i = 0; i < digits.Length; i++)
		{
			if (i % 2 == 0)
			{
				sum += digits[i];
			}
			else
			{
				sum += digits[i] * 3;
			}
		}
		int remainder = sum % 10;
		return 10 - remainder;
	}

	public override bool ValidateAndConvert(int[] digits, out string newIsbnDigits)
	{
		throw new Exception("Invalid option for ISBN-13");
	}
}