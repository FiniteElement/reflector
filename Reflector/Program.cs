using System;

namespace Reflector
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var root1 = Reflector.CreateRootReflector<Test> ();
			Console.WriteLine ("Key of root1: {0}", root1.Key);
			root1 ["Leaf"].Value = "Banán";
			Console.WriteLine (root1 ["Member"].Info);
			Console.WriteLine (root1 ["Member"].Value);
			Console.WriteLine (root1 ["Method"].Info);
			Console.WriteLine (root1 ["Method"].Invoke ());
			Console.WriteLine (root1 ["MethodWithParam"].Info);
			Console.WriteLine (root1 ["MethodWithParam"].Invoke (3));
			Console.WriteLine ((root1.Value as Test).Leaf);

			var obj = new Test ();
			obj.Leaf = "Körte";
			var root2 = Reflector.CreateRootReflector (obj);
			root2.AutoMap = false;
			root2.Map ("Leaf");
			Console.WriteLine ((root2.Value as Test).Leaf);

			var Property = Reflector.CreateReflector ("Property", obj);
			Property.AutoMap = false;
			Console.WriteLine ("Key of Property: {0}", Property.Key);
			var Leaf = Reflector.CreateReflector ("Leaf", obj);
			Leaf.AutoMap = false;
			Leaf.Value = "Cica";
			Property.Default ();
			Property.Map ("Property");
			Property ["Property"].Default ();
			Property ["Property"].Map ("Leaf");
			Property ["Property"] ["Leaf"].Value = "Alma";
			Property.Map ("Elements");
			Console.WriteLine (obj.Property.Property.Leaf);
			Property ["Elements"].Default (1);
			Property ["Elements"].CastValue<object[]> () [0] = obj.Property.Property;
			Console.WriteLine (obj.Property.Elements [0].Leaf);
		}
	}
}
