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
			return ParseJson (chars, ref index);
		}

		private static JBase ParseJson(char[] chars, ref int index)
		{
			var token = GetNextValidToken (chars, ref index);

			switch (token.Type) {
			case TokenType.PUNCTUATION:
				switch (token.CharValue) {
				case '{':
					var obj = ParseObject (chars, ref index);
					return obj;
				case '[':
					var array = ParseArray (chars, ref index);
					return array;
				default:
					throw new JException ();
				}
				break;
			default:
				throw new JException ();
			}
		}

		private static JBase ParseJsonValue(char[] chars, ref int index)
		{
			var token = GetNextValidToken (chars, ref index);

			switch (token.Type) {
			case TokenType.NUMBER:
				throw new NotImplementedException ();
				break;
			case TokenType.BOOLEAN:
				throw new NotImplementedException ();
				break;
			case TokenType.STRING:
				throw new NotImplementedException ();
				break;
			case TokenType.NULL:
				throw new NotImplementedException ();
				break;
			default:
				return ParseJson (chars, ref index);
			}
		}

		private static JObject ParseObject(char[] chars, ref int index)
		{
			var obj = new JObject ();

		fieldTag:
			var token = GetNextValidToken (chars, ref index);

			if (token.Type == TokenType.PUNCTUATION && token.CharValue == '}') {
				return obj;
			}

			if (token.Type != TokenType.STRING) {
				throw new JException ();
			}

			var key = token.StringValue;

			token = GetNextValidToken (chars, ref index);

			if (token.Type != TokenType.PUNCTUATION && token.CharValue != ':') 
			{
				throw new JException ();
			}

			obj [key] = ParseJsonValue (chars, ref index);

			token = GetNextValidToken (chars, ref index);
			if (token.Type == TokenType.PUNCTUATION && token.CharValue == ',')
				goto fieldTag;

			if (token.Type == TokenType.PUNCTUATION && token.CharValue == '}') {
				return obj;
			}

			throw new JException ();
		}

		private static JArray ParseArray(char[] chars, ref int index)
		{

			JArray array = new JArray ();

			int savedIndex = index;
			var token = GetNextValidToken (chars, ref index);
			while (token.Type != TokenType.PUNCTUATION || token.CharValue != ']') {

				if (token.Type == TokenType.UNEXPECTED || token.Type == TokenType.END) {
					throw new JException ();
				}

				index = savedIndex;
				array.Add(ParseJsonValue(chars, ref index));

				savedIndex = index;
				token = GetNextValidToken (chars, ref index);

				if (token.Type == TokenType.PUNCTUATION && token.CharValue == ',') {
					token = GetNextValidToken (chars, ref index);
				}
			}

			if (token.Type == TokenType.PUNCTUATION && token.CharValue == ']') {
				return array;
			}

			throw new JException ();
		}

		// jump over the ignore characters
		private static Token GetNextValidToken(char[] chars, ref int index)
		{
			var token = GetNextToken (chars, ref index);
			while (token.Type == TokenType.IGNORE) {
				token = GetNextToken (chars, ref index);
			}

			return token;
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
				if (double.TryParse (numberString, out numberValue)) 
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
						index += 4;
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
						index += 5;
						return new Token { Type = TokenType.BOOLEAN, BoolValue = false };
					}
				}

				// find a null
				if (chars [index] == 'n') {
					if (
						(index + 3 < chars.Length) &&
						chars [index + 1] == 'u' &&
						chars [index + 2] == 'l' &&
						chars [index + 3] == 'l') 
					{
						index += 4;
						return new Token { Type = TokenType.NULL};
					}
				}


				if (index < chars.Length) 
				{
					return new Token{ Type = TokenType.UNEXPECTED };
				}
			}// end for find string

			return new Token{ Type = TokenType.UNEXPECTED, CharValue = chars[index] };
		}
	}
}

