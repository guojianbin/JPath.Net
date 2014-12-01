using System;
using System.Collections.Generic;

namespace org.lmatt
{
	public class JArray : JBase, IList<JBase>, ICloneable
	{
		private IList<JBase> _data = new List<JBase>();

		public JArray()
		{
		}

		public static JArray Parse(string json)
		{
			return JBase.ParseJson (json) as JArray;
		}

		#region ICloneable implementation

		public object Clone ()
		{
			JArray clone = new JArray ();
			for (int i = 0; i < _data.Count; i++) {
				clone.Add (this [i]);
			}

			return clone;
		}

		#endregion

		#region IList implementation
		public int IndexOf (JBase item)
		{
			return _data.IndexOf (item);
		}
		public void Insert (int index, JBase item)
		{
			_data.Insert (index, item);
		}
		public void RemoveAt (int index)
		{
			_data.RemoveAt (index);
		}
		public JBase this [int index] {
			get {
				return _data [index];
			}
			set {
				_data [index] = value;
			}
		}
		#endregion
		#region ICollection implementation
		public void Add (JBase item)
		{
			_data.Add (item);
		}
		public void Clear ()
		{
			_data.Clear ();
		}
		public bool Contains (JBase item)
		{
			return _data.Contains (item);
		}
		public void CopyTo (JBase[] array, int arrayIndex)
		{
			_data.CopyTo (array, arrayIndex);
		}
		public bool Remove (JBase item)
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
		public IEnumerator<JBase> GetEnumerator ()
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
	}
}

