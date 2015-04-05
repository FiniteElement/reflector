using System;

namespace Reflector
{
	public class Test
	{
		public String Member = "Simple member!";

		public Test[] Elements { get; set; }

		public Test Property { get; set; }

		public String Leaf { get; set; }

		public String Method ()
		{
			return "Method!";
		}

		public String MethodWithParam (int a)
		{
			return a.ToString ();
		}
	}
}

