using System;
using System.Collections.Generic;

namespace org.lmatt
{
	public class JObject : JBase, IDictionary<string, JBase>
	{
		#region IDictionary implementation

		public void Add (string key, JBase value)
		{
			throw new NotImplementedException ();
		}

		public bool ContainsKey (string key)
		{
			throw new NotImplementedException ();
		}

		public bool Remove (string key)
		{
			throw new NotImplementedException ();
		}

		public bool TryGetValue (string key, out JBase value)
		{
			throw new NotImplementedException ();
		}

		public JBase this [string index] {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public ICollection<string> Keys {
			get {
				throw new NotImplementedException ();
			}
		}

		public ICollection<JBase> Values {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion

		#region ICollection implementation

		public void Add (KeyValuePair<string, JBase> item)
		{
			throw new NotImplementedException ();
		}

		public void Clear ()
		{
			throw new NotImplementedException ();
		}

		public bool Contains (KeyValuePair<string, JBase> item)
		{
			throw new NotImplementedException ();
		}

		public void CopyTo (KeyValuePair<string, JBase>[] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}

		public bool Remove (KeyValuePair<string, JBase> item)
		{
			throw new NotImplementedException ();
		}

		public int Count {
			get {
				throw new NotImplementedException ();
			}
		}

		public bool IsReadOnly {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion

		#region IEnumerable implementation

		public IEnumerator<KeyValuePair<string, JBase>> GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IEnumerable implementation

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		public JObject ()
		{
		}
	}
}

