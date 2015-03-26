using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;

namespace Reflector
{
	public sealed class Reflector : IReflector
	{
		private readonly object _obj;
		private readonly Dictionary<string, IReflector> _reflectors;

		private Reflector (string key, object value, dynamic info)
		{
			AutoMap = true;
			Info = info;
			Key = key;
			_obj = value;
			_reflectors = new Dictionary<string, IReflector> ();
		}

		public bool AutoMap { get; set; }

		public string Key { get; private set; }

		public dynamic Info { get; private set; }

		public T CastValue<T> ()
		{
			return (T)Info.GetValue (_obj);
		}

		public object Value {
			get { return Info.GetValue (_obj); }
			set { Info.SetValue (_obj, value); }
		}

		public void Default (params object[] args)
		{
			Info.SetValue (_obj, Activator.CreateInstance (Info.PropertyType, args));
		}

		public object Invoke (params object[] args)
		{
			return Info.Invoke (_obj, args);
		}

		public bool Map (string key)
		{
			var reflector = CreateReflector (key, Value);
			return MapReflector (reflector);
		}

		public IReflector this [string key] {
			get {
				IReflector obj;
				_reflectors.TryGetValue (key, out obj);
				if (AutoMap && obj == null) {
					this.Map (key);
					_reflectors.TryGetValue (key, out obj);
				}
				return obj;
			}
		}

		public static IReflector CreateRootReflector<T> () where T : new()
		{
			var root = new RootWrapper<T> ();
			return CreateReflector (root.ToString (), root);
		}

		public static IReflector CreateRootReflector<T> (T value) where T : new()
		{
			var root = new RootWrapper<T> (value);
			return CreateReflector (root.ToString (), root);
		}

		public static IReflector CreateRootReflector (object value)
		{
			var root = new RootWrapper<object> (value);
			return CreateReflector (root.ToString (), root);
		}

		public static IReflector CreateReflector<T> (string key) where T : new()
		{
			return CreateReflector(key, new T () );
		}

		public static IReflector CreateReflector<T> (string key, T value) where T : new()
		{
			return CreateReflector (key, value as object);
		}

		public static IReflector CreateReflector (string key, object value)
		{
			if (value == null || key == null)
				return null;
			dynamic info = value.GetType ().GetProperty (key);
			if (info == null)
				info = value.GetType ().GetMethod (key);
			if (info == null)
				info = value.GetType ().GetField (key);
			return info == null ? null : new Reflector (key, value, info);
		}

		public bool MapReflector (IReflector reflector)
		{
			if (reflector == null)
				return false;
			_reflectors.Add (reflector.Key, reflector);
			return true;
		}

		public void RemoveProperty (string key)
		{
			_reflectors.Remove (key);
		}

		public void Clear ()
		{
			_reflectors.Clear ();
		}

		private class RootWrapper<T> where T : new()
		{
			public T Root { private get; set; }

			public RootWrapper ()
			{
				Root = new T ();
			}

			public RootWrapper (T obj)
			{
				Root = obj;
			}

			public override String ToString ()
			{
				return "Root";
			}
		}
	}
}
