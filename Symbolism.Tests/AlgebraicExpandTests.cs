using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class AlgebraicExpandTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			Assert.AreEqual(24 + 26*x + 9*(x ^ 2) + (x ^ 3),
			                ((x + 2)*(x + 3)*(x + 4)).AlgebraicExpand());
		}
		[TestMethod]
		public void TestMethod2()
		{
			Assert.AreEqual((x ^ 3) + (y ^ 3) + (z ^ 3) + 3*(x ^ 2)*y + 3*(y ^ 2)*x + 3*(x ^ 2)*z + 3*(y ^ 2)*z + 3*(z ^ 2)*x + 3*(z ^ 2)*y + 6*x*y*z,
			                ((x + y + z) ^ 3).AlgebraicExpand());
		}
		[TestMethod]
		public void TestMethod3()
		{
			Assert.AreEqual(2 + 2*x + (x ^ 2) + 2*y + (y ^ 2),
			                (((x + 1) ^ 2) + ((y + 1) ^ 2)).AlgebraicExpand());
		}
		[TestMethod]
		public void TestMethod4()
		{
			Assert.AreEqual(49 + 56*x + 30*(x ^ 2) + 8*(x ^ 3) + (x ^ 4),
			                ((((x + 2) ^ 2) + 3) ^ 2).AlgebraicExpand());
		}
		[TestMethod]
		public void TestMethod5()
		{
			Assert.AreEqual(sin(x*y + x*z),
			                sin(x*(y + z)).AlgebraicExpand());
		}
		[TestMethod]
		public void TestMethod6()
		{
			Assert.AreEqual(a*b + a*c == x*y + x*z,
			                (a*(b + c) == x*(y + z)).AlgebraicExpand());
		}
		[TestMethod]
		public void TestMethod7()
		{
			Assert.AreEqual(1082.5317547305483/x + 5*x + 2.8660254037844384*(x ^ 2),
			                (5*x*(500/(x ^ 2)*(sqrt(3.0)/4) + 1) + 2*(x ^ 2) + (sqrt(3.0)/2)*(x ^ 2)).AlgebraicExpand());
		}
	}
}
