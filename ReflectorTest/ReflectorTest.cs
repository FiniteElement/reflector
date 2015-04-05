using NUnit.Framework;
using Reflector;

namespace ReflectorTest
{
	[TestFixture]
	public class ReflectorTest
	{
		[Test]
		public void CreateTypedParameterlessRoot()
		{
			var root = Reflector.Reflector.CreateRootReflector<Test> ();
			Assert.IsNotNull (root);
		}

		[Test]
		public void CreateTypedParametrizedRoot()
		{
			var root = Reflector.Reflector.CreateRootReflector<Test> (new Test());
			Assert.IsNotNull (root);
		}

		[Test]
		public void CreateParametrizedRoot()
		{
			var root = Reflector.Reflector.CreateRootReflector (new Test());
			Assert.IsNotNull (root);
		}


		[Test]
		public void CreateTypedNullParametrizedRoot()
		{
			var root = Reflector.Reflector.CreateRootReflector<Test> (null);
			Assert.IsNull (root);
		}

		[Test]
		public void CreateNullParametrizedRoot()
		{
			var root = Reflector.Reflector.CreateRootReflector (null);
			Assert.IsNull (root);
		}
	}
}

