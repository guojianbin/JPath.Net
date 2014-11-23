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
			int index = 0;
			ParseJson (chars, index);
		}

		private static JBase ParseJson(char[] chars, ref int index)
		{
			var token = GetNextToken (chars, ref index);
			while (token.Type != TokenType.END || token.Type != TokenType.UNEXPECTED) 
			{
				switch (token.Type) {
				case TokenType.BOOLEAN:
					break;
				case TokenType.STRING:
					break;
				case TokenType.IGNORE:
					break;
				case TokenType.NUMBER:
					break;
				case TokenType.PUNCTUATION:
					switch (token.CharValue) {
					case '{':
						var obj = ParseObject (chars, ref index);
						break;
					case '[':
						var array = ParseArray (chars, ref index);
						break;
					default:
						throw new JException ();
					}
					break;
				default:
					throw new JException ();
				}

				token = GetNextToken (chars, ref index);
			}
		}

		private static JObject ParseObject(char[] chars, ref int index)
		{
			var obj = new JObject ();

			var token = GetNextToken (chars, ref index);
			while (token.Type == TokenType.IGNORE) {
				token = GetNextToken (chars, ref index);
			}

			if (token.Type != TokenType.STRING) {
				throw new JException ();
			}

			var key = token.StringValue;

			token = GetNextToken (chars, ref index);
			while (token.Type == TokenType.IGNORE) {
				token = GetNextToken (chars, ref index);
			}

			if (token.Type != TokenType.PUNCTUATION && token.CharValue != ':') 
			{
				throw new JException ();
			}

			obj [key] = ParseJson (chars, ref index);

			//TODO: parse more key-value pairs
		}

		private static JArray ParseArray(char[] chars, ref int index)
		{

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

				// find a bool
				if (chars [index] == 't') 
				{
					if (
						(index + 3 < chars.Length) &&
						chars [index + 1] == 'r' &&
						chars [index + 2] == 'u' &&
						chars [index + 3] == 'e') 
					{
						return new Token { Type = TokenType.BOOLEAN, BoolValue = true };
					}
				}

				if (chars [index] == 'f') 
				{
					if (
						(index + 4 < chars.Length) &&
						chars [index + 1] == 'a' &&
						chars [index + 2] == 'l' &&
						chars [index + 3] == 's' &&
						chars [index + 4] == 'e') 
					{
						return new Token { Type = TokenType.BOOLEAN, BoolValue = false };
					}
				}

				if (index >= chars.Length) 
				{
					return new Token{ Type = TokenType.UNEXPECTED };
				}
			}// end for find string

			return new Token{ Type = TokenType.UNEXPECTED, CharValue = chars[index] };
		}
	}
}

