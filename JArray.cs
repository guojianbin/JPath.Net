using System;
using System.Collections.Generic;

namespace org.lmatt
{
	public class JArray : JBase, IList<JBase>, ICloneable
	{
		private JBase[] _contents = new JBase[8];
		private int _count;

		public JArray()
		{
			_count = 0;
		}

		#region ICloneable implementation

		public object Clone ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IList implementation
		public int IndexOf (JBase item)
		{
			throw new NotImplementedException ();
		}
		public void Insert (int index, JBase item)
		{
			throw new NotImplementedException ();
		}
		public void RemoveAt (int index)
		{
			throw new NotImplementedException ();
		}
		public JBase this [int index] {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}
		#endregion
		#region ICollection implementation
		public void Add (JBase item)
		{
			throw new NotImplementedException ();
		}
		public void Clear ()
		{
			throw new NotImplementedException ();
		}
		public bool Contains (JBase item)
		{
			throw new NotImplementedException ();
		}
		public void CopyTo (JBase[] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}
		public bool Remove (JBase item)
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
		public IEnumerator<JBase> GetEnumerator ()
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
	}
}

