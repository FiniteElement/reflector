using System.Reflection;

namespace Reflector
{
	public interface IReflector
	{
		bool AutoMap { get; set; }

		void Default (params object[] args);

		object Value { get; set; }

		object Invoke (params object[] args);

		T CastValue<T> ();

		string Key { get; }

		IReflector this [string key] { get; }

		bool Map (string key);

		bool MapReflector (IReflector reflector);

		dynamic Info { get; }
	}
}

