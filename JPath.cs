using System;
using System.Collections.Generic;
using System.Linq;

namespace org.lmatt
{
	/// <summary>
	/// Now only support basic path( //, /, *, array index, object key) , no functions. 
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
				var ret = Evaluate (jPath.ToCharArray (), 0, obj);
				if(ret != null)
					results.Add (ret);
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
				var ret = Evaluate (jPath.ToCharArray (), 0, array);
				if(ret != null)
					results.Add (ret);
			}

			return results;
		}


		private static IList<JBase> Evaluate(char[] chars, int index, JBase baseObj)
		{
			if (index >= chars.Length)
				return null;

			var ret = new List<JBase> ();

			if (chars [index] == '/') {

				int nextIndex = index + 1;
				if (nextIndex >= chars.Length)
					return null;

				if (chars [nextIndex] == '/') {
					//TODO: implement double /
				} else {
					var sret = Evaluate (chars, index + 1, baseObj);
					if(sret != null) {
						ret.Add (sret);
					}

					return ret;
				}
			} else if (chars [index] == '*') {
				var obj = baseObj as JObject;
				if (obj != null) {

					index++;
					if (index == chars.Length) {
						return obj.Values;
					} else {
						foreach (var v in obj.Values) {
							var sret = Evaluate (chars, index, v);
							if (sret != null) {
								ret.Add (sret);
							}
						}

						return ret;
					}
				}

				var array = baseObj as JArray;
				if (array != null) {

					index++;
					if (index == chars.Length) {
						return array.ToList ();
					} else {
						foreach (var v in array) {
							var sret = Evaluate (chars, index, v);
							if (sret != null) {
								ret.Add (sret);
							}
						}

						return ret;
					}
				}

				if (index + 1 != chars.Length)
					return null;

				ret.Add (baseObj);
				return ret;
			}
			// This is array index
			else if (chars [index] <= '9' && chars [index] >= '0') {
				var array = baseObj as JArray;
				if (array != null) {

					//TODO: implement array index
				} else {
					return null;
				}
			}
			// This is object key
			else {

				var obj = baseObj as JObject;
				if (obj != null) {

					//TODO: implement object key
				} else {
					return null;
				}
			}
		}
	}
}

