/// <summary>
/// Json Base Class.
/// </summary>
 

namespace org.lmatt
{
	using System;

	public class JBase
	{
		public JBase ()
		{
		}

		/// <summary>
		/// Parses the json.
		/// </summary>
		/// <returns>The JBase object</returns>
		/// <param name="Json">Json string</param>
		public static JBase ParseJson(string Json)
		{
			var chars = Json.ToCharArray ();

		}

		private static Token GetNextToken(char[] chars, ref int index)
		{
			if (index >= chars.Length) 
			{
				return new Token{ Type = TokenType.END };
			}

			// find the token can be ignored
			bool findIgnoreChar = false;
			while (
				index < chars.Length && 
				(
					chars [index] == ' ' || 
					chars[index] == '\t' || 
					chars[index] == '\r' || 
					chars[index] == '\n'
				) 
			)
			{
				index++;
				findIgnoreChar = true;
			}

			if (findIgnoreChar) 
			{
				return new Token { Type = TokenType.IGNORE };
			}

			// find the punctuation token
			if (
				chars[index] == ':' || 
				chars[index] == ',' || 
				chars[index] == '[' || 
				chars[index] == ']' ||
				chars[index] == '{' || 
				chars[index] == '}'
			) 
			{
				return new Token { Type = TokenType.PUNCTUATION, CharValue = chars [index++] };
			}

			// find the number
			bool negativeNumber = false;
			if (chars [index] == '-') 
			{
				negativeNumber = true;
				index++;
			}

			if (index < chars.Length && (chars[index] >= '0' || chars[index] <= '9'))
			{

			}

			if (negativeNumber) 
			{
				return new Token { Type = TokenType.UNEXPECTED, CharValue = chars [index - 1] };
			}
		}
	}
}

