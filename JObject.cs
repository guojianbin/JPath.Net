using System;
using System.Collections.Generic;
using System.Linq;

namespace org.lmatt
{
	public class JObject : JBase, IDictionary<string, JBase>
	{

		IDictionary<string, JBase> _data = new Dictionary<string, JBase>();

		#region IDictionary implementation

		public void Add (string key, JBase value)
		{
			_data.Add (key, value);
		}

		public bool ContainsKey (string key)
		{
			return _data.ContainsKey (key);
		}

		public bool Remove (string key)
		{
			return _data.Remove (key);
		}

		public bool TryGetValue (string key, out JBase value)
		{
			return _data.TryGetValue (key, out value);
		}

		public JBase this [string index] {
			get {
				return _data [index];
			}
			set {
				_data [index] = value;
			}
		}

		public ICollection<string> Keys {
			get {
				return _data.Keys;
			}
		}

		public ICollection<JBase> Values {
			get {
				return _data.Values;
			}
		}

		#endregion

		#region ICollection implementation

		public void Add (KeyValuePair<string, JBase> item)
		{
			_data.Add (item);
		}

		public void Clear ()
		{
			_data.Clear ();
		}

		public bool Contains (KeyValuePair<string, JBase> item)
		{
			return _data.Contains (item);
		}

		public void CopyTo (KeyValuePair<string, JBase>[] array, int arrayIndex)
		{
			_data.CopyTo (array, arrayIndex);
		}

		public bool Remove (KeyValuePair<string, JBase> item)
		{
			return _data.Remove (item);
		}

		public int Count {
			get {
				return _data.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return _data.IsReadOnly;
			}
		}

		#endregion

		#region IEnumerable implementation

		public IEnumerator<KeyValuePair<string, JBase>> GetEnumerator ()
		{
			return _data.GetEnumerator ();
		}

		#endregion

		#region IEnumerable implementation

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _data.GetEnumerator ();
		}

		#endregion

		public JObject ()
		{
		}

		public static JObject Parse(string json)
		{
			return JBase.ParseJson (json) as JObject;
		}
	}
}

