using System;
using System.Collections.Generic;
using System.Linq;

namespace org.lmatt
{
	/// <summary>
	/// Now only support basic path( //, /, array index, object key) , no functions. 
	/// Example:
	/// 	Data: {"key1" : [{"key2": "value2"}]} 
	/// 	JPath: //key1/0/key2
	/// 	Return: JString("value2")
	/// </summary>
	public class JPath
	{
		private string[] jPaths;

		public JPath(string jPath)
		{
			jPath = jPath.Trim ();
			jPaths = jPath.Split (new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries).Select (t => t.Trim ()).ToArray ();
		}

		/// <summary>
		/// Evaluate the specified JBase object.
		/// </summary>
		public IList<JBase> Evaluate(JBase obj)
		{
			var results = new List<JBase> ();
			foreach (var jPath in jPaths) {
				//TODO: unique the result
				results.AddRange(Evaluate (jPath.ToCharArray (), 0, obj));
			}

			return results;
		}

		private static string NextToken(char[] chars, ref int index)
		{
			int saveIndex = index;
			while (index < chars.Length && chars [index] != '/')
				index++;
			return new string (chars, saveIndex, index - saveIndex);
		}

		private static IList<JBase> EvaluateValue(char[] chars, int index, JBase baseObj) {


			var ret = new List<JBase> ();

			if (index >= chars.Length || baseObj == null)
				return ret;

			// This is array index
			if (chars [index] <= '9' && chars [index] >= '0') {
				var array = baseObj as JArray;
				if (array != null) {

					try {

						var arrayIndex = int.Parse (NextToken (chars, ref index));
						if (index >= chars.Length) {
							ret.Add (array [arrayIndex]);
						} else {
							ret.AddRange(Evaluate(chars, index, array[arrayIndex]));
						}
					} catch (Exception ex) {
						//ignore the exception here.
					}
				} 
			}
			// This is object key
			else {

				var obj = baseObj as JObject;
				if (obj != null) {
					try {

						var keyStr = NextToken (chars, ref index);
						if (index >= chars.Length) {
							ret.Add (obj[keyStr]);
						} else {
							ret.AddRange(Evaluate(chars, index, obj[keyStr]));
						}
					} catch (Exception ex) {
						//ignore the exception here.
					}
				}
			}

			return ret;
		}

		private static IList<JBase> GetAllSubNodes(JBase baseObj) {

			var ret = new List<JBase> ();

			if (baseObj == null)
				return ret;

			ret.Add (baseObj);

			var array = baseObj as JArray;
			if (array != null) {

				foreach (var item in array) {
					ret.AddRange (GetAllSubNodes (item));
				}
			} else {
				var obj = baseObj as JObject;
				if (obj != null) {

					foreach (var item in obj) {
						ret.AddRange (GetAllSubNodes (item.Value));
					}
				}
			}

			return ret;
		}

		private static IList<JBase> Evaluate(char[] chars, int index, JBase baseObj)
		{
			var ret = new List<JBase> ();

			if (index >= chars.Length || baseObj == null)
				return ret;

			if (chars [index] == '/') {

				int nextIndex = index + 1;
				if (nextIndex >= chars.Length) {

					ret.Add (baseObj);
					return ret;
				}

				if (chars [nextIndex] == '/') {
				
					var nodes = GetAllSubNodes (baseObj);
					foreach (var node in nodes) {
						ret.AddRange(EvaluateValue(chars, nextIndex + 1, node));
					}
				} else {
					ret.AddRange(EvaluateValue(chars, index + 1, baseObj));
				}
				return ret;

			}

			return ret;
		}
	}
}

