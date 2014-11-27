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
			JArray clone = new JArray ();
			for (int i = 0; i < this._count; i++) {
				clone.Add (this [i]);
			}

			return clone;
		}

		#endregion

		#region IList implementation
		public int IndexOf (JBase item)
		{
			for (int i = 0; i < _count; i++) {
				if (_contents [i] == item)
					return i;
			}

			return -1;
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
				return _contents [index];
			}
			set {
				_contents [index] = value;
			}
		}
		#endregion
		#region ICollection implementation
		public void Add (JBase item)
		{
			if (_count >= (_contents.Length - 1)) {
				var newContent = new JBase[_contents.Length << 1];
				for (int i = 0; i < _count; i++) {
					newContent [i] = _contents [i];
				}
				_contents = newContent;
			}

			_contents [_count++] = item;
		}
		public void Clear ()
		{
			_count = 0;
		}
		public bool Contains (JBase item)
		{
			for (int i = 0; i < _count; i++) {
				if (_contents [i] == item)
					return true;
			}

			return false;
		}
		public void CopyTo (JBase[] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}
		public bool Remove (JBase item)
		{
			int fIndex = -1;
			for (int i = 0; i < _count; i++) {
				if (_contents [i] == item) {
					fIndex = i;
					break;
				}
			}
			if (fIndex > -1) {
				for (int i = fIndex + 1; i < _count; i++) {
					_contents [i - 1] = _contents [i];
				}
				_count--;
				return true;
			}
			return false;
		}
		public int Count {
			get {
				return _count;
			}
		}
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region IEnumerable implementation
		public IEnumerator<JBase> GetEnumerator ()
		{
			return ((IEnumerable<JBase>)_contents).GetEnumerator();
		}
		#endregion
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _contents.GetEnumerator ();
		}
		#endregion
	}
}

