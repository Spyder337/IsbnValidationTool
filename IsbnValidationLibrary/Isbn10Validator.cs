using System;
using System.Text;
using System.Text.RegularExpressions;

namespace IsbnValidationTool;

public class Isbn10Validator : IsbnValidator
{
	public override bool ValidateIsbn(int[] digits)
	{
		int value = 0;
		int checkDigit = digits[9];
		for (int i = 0; i < 9; i++)
		{
			value += digits[i] * (i + 1);
		}

		int remainder = value % 11;
		return remainder == checkDigit;
	}

	public override bool ValidateAndConvert(int[] digits, out string newIsbn)
	{
		newIsbn = "";
		int[] newIsbnDigits = new int[13] { 9, 7, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		if (ValidateIsbn(digits))
		{
			int offset = 3;     //For prepending the ISBN-13 prefix
			for (int i = 0; i < digits.Length - 1; i++)
			{
				newIsbnDigits[offset + i] = digits[i];
			}
			newIsbnDigits[12] = CalculateCheckDigit(newIsbnDigits);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 13; i++)
			{
				sb.Append(newIsbnDigits[i]);
			}
			newIsbn = sb.ToString();
			return true;
		}
		else
		{
			return false;
		}
	}

	protected override int CalculateCheckDigit(int[] digits)
	{
		int sum = 0;

		for (int i = 0; i < digits.Length - 1; i++)
			sum += (i % 2 == 0 ? 1 : 3) * digits[i];

		int rem = sum % 10;

		if (rem == 0)
			return 0;
		else
			return 10 - rem;
	}
}