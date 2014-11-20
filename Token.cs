using System;

namespace org.lmatt
{
	public enum TokenType {
		STRING,
		NUMBER,
		BOOLEAN,
		OBJECTNAME,
		PUNCTUATION,
		IGNORE,
		END,
		UNEXPECTED
	}

	public class Token
	{
		public string StringValue { get; set; }
		public bool BoolValue { get; set; }
		public double NumberValue { get; set; }
		public char CharValue { get; set; }
		public TokenType Type { get; set; }
	}
}

