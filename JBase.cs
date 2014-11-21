/// <summary>
/// Json Base Class.
/// </summary>
using System.Text;
 

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
			int startIndex = -1;
			if (chars [index] == '-') 
			{
				startIndex = index;
				index++;
			}

			if (index < chars.Length && (chars[index] >= '0' || chars[index] <= '9'))
			{
				while (index < chars.Length) 
				{
					if (
						chars [index] >= '0' ||
						chars [index] <= '9' ||
						chars [index] == '.' ||
						chars [index] == 'e' ||
						chars [index] == 'E' ||
						chars [index] == '+' ||
						chars [index] == '-') 
					{
						index++;
					} else 
					{
						break;
					}
				}

				double numberValue;
				var numberString = new string (chars, startIndex, index - startIndex);
				if (double.TryParse (numberString, out doubleValue)) 
				{
					return new Token { Type = TokenType.NUMBER, NumberValue = numberValue };
				} else 
				{
					return new Token { Type = TokenType.UNEXPECTED, StringValue = numberString };
				}
			}

			// found -, but not a number
			if (startIndex >= 0) 
			{
				return new Token { Type = TokenType.UNEXPECTED, CharValue = chars [index - 1] };
			}

			//TODO: support single quote string
			// find string
			if (chars [index] == '"') 
			{
				StringBuilder builder = new StringBuilder ();
				index++;
				while (index < chars.Length) 
				{
					if (chars [index] == '\\') {
						if (index + 1 >= chars.Length) {
							return new Token { Type = TokenType.UNEXPECTED, CharValue = chars [index] };
						}

						switch (chars [index + 1]) {
						case '\'':
							builder.Append ('\'');
							break;
						case '"':
							builder.Append ('"');
							break;
						case '\\':
							builder.Append ('\\');
							break;
						case '/':
							builder.Append ('/');
							break;
						case 'b':
							builder.Append ('\b');
							break;
						case 'f':
							builder.Append ('\f');
							break;
						case 'n':
							builder.Append ('\n');
							break;
						case 'r':
							builder.Append ('\r');
							break;
						case 't':
							builder.Append ('\t');
							break;
						case 'u':
							// TODO: handle 4 hexadecimal digits
							return new Token{ Type = TokenType.UNEXPECTED, CharValue = 'u' };
						default:
							return new Token{ Type = TokenType.UNEXPECTED, CharValue = chars [index + 1] };
						}
						index++;
					} // end for escapse
					else if (chars [index] == '"') 
					{
						index++;
						return new Token{ Type = TokenType.STRING, StringValue = builder.ToString () };
					} else 
					{
						builder.Append (chars [index]);
					}
					index++;
				}//end while

				if (index >= chars.Length) 
				{
					return new Token{ Type = TokenType.UNEXPECTED };
				}
			}// end for find string

			return new Token{ Type = TokenType.UNEXPECTED, CharValue = chars[index] };
		}
	}
}

