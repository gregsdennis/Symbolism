using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class CheckVariableTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			Assert.AreEqual(false, and(x + y == z, x / y == 0, x != 0).CheckVariable(x));
		}
	}
}
