using System.Collections.Generic;

namespace System{
	public interface IDataService  {
		void Save<T>(ref T data, bool overwrite = true);
		T Load<T>(string name);
		void Delete (string name);
		void DeleteAll ();
		IEnumerable<string> ListSave ();
	}

}