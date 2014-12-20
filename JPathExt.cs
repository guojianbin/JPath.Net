using System;
using System.Collections.Generic;

namespace org.lmatt
{
	public static class JPathExt
	{
		public static JBase JPathSelect(this JBase baseObj, string jPath)
		{
			var items = baseObj.JPathSelects (jPath);
			if (items.Count > 0)
				return items[0];
			return null;
		}

		public static IList<JBase> JPathSelects(this JBase baseObj, string jPath)
		{
			JPath path = new JPath (jPath);
			return path.Evaluate (baseObj);
		}
	}
}

