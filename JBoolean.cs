using System;

namespace org.lmatt
{
	public class JBoolean : JBase
	{
		public bool Value { get; set;}

		public JBoolean ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[JBoolean: Value={0}]", Value);
		}
	}
}

