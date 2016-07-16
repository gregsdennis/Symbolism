using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Constants;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class EliminateVariableTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			Assert.AreEqual((z ^ 3) == (y ^ 5),
			                and((x ^ 3) == (y ^ 5), z == x).EliminateVariable(x));
		}
		[TestMethod]
		public void TestMethod2()
		{
			Assert.AreEqual(and((x ^ 3) == (y ^ 5), z == (x ^ 7)),
			                and((x ^ 3) == (y ^ 5), z == (x ^ 7)).EliminateVariable(x));
		}
		[TestMethod]
		public void TestMethod3()
		{
			Assert.AreEqual(or(and(half*sqrt(16) + y == 0, half*sqrt(16) + z == 10),
			                   and(-half*sqrt(16) + y == 0, -half*sqrt(16) + z == 10)),
			                and((x ^ 2) - 4 == 0, y + x == 0, x + z == 10).EliminateVariable(x));
		}
		[TestMethod]
		public void TestMethod4()
		{
			Assert.AreEqual(or(z == a, z == a),
			                or(and(x == y, x == z, x == a),
			                   and(x == -y, x == z, x == a)).EliminateVariables(x, y));
		}
		[TestMethod]
		public void TestMethod5()
		{
			Assert.AreEqual(and(x != z, x == 10),
			                and(y != z, y == x, y == 10).EliminateVariable(y));
		}
	}
}
