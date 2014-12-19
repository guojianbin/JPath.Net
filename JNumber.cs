using System;

namespace org.lmatt
{
	public class JNumber : JBase
	{

		public double Value { get; set; }

		public JNumber ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[JNumber: Value={0}]", Value);
		}
	}
}

