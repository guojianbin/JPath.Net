using System;

namespace org.lmatt
{
	public class JString : JBase
	{
		public string Value { get; set;}

		public JString ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[JString: Value={0}]", Value);
		}
	}
}

