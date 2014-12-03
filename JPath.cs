using System;
using System.Collections.Generic;
using System.Linq;

namespace org.lmatt
{
	public class JPath
	{
		private string[] jPaths;

		public JPath(string jPath)
		{
			jPath = jPath.Trim ();
			jPaths = jPath.Split ('|', StringSplitOptions.RemoveEmptyEntries).Select (t => t.Trim ()).ToArray ();
		}

		/// <summary>
		/// Evaluate the specified obj.
		/// </summary>
		/// <param name="obj">Object.</param>
		public IList<JBase> Evaluate(JObject obj)
		{
			IList<JBase> results = new List<JBase> ();
			foreach (var jPath in jPaths) {
				//TODO: unique the result
				results.Add (Evaluate (jPath.ToCharArray (), 0, obj));
			}

			return results;
		}

		/// <summary>
		/// Evaluate the specified array.
		/// </summary>
		/// <param name="array">Array.</param>
		public IList<JBase> Evaluate(JArray array)
		{
			IList<JBase> results = new List<JBase> ();
			foreach (var jPath in jPaths) {
				//TODO: unique the result
				results.Add (Evaluate (jPath.ToCharArray (), 0, array));
			}

			return results;
		}


		private IList<JBase> Evaluate(char[] chars, int index, JBase baseObj)
		{
			if (index >= chars.Length)
				return null;

			if (chars [index] == '/') {

				int nextIndex = index + 1;
				if (chars [nextIndex] == '/') {

				} else {
				}
			}
		}
	}
}

