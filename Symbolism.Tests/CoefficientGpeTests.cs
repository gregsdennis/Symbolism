using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class CoefficientGpeTests
	{
		[TestMethod]
		public void Quadratic()
		{
			Assert.AreEqual(a, (a * (x ^ 2) + b * x + c).CoefficientGpe(x, 2));
		}
		[TestMethod]
		public void Complex1()
		{
			Assert.AreEqual(3 * (y ^ 2) + 7, (3 * x * (y ^ 2) + 5 * (x ^ 2) * y + 7 * x + 9).CoefficientGpe(x, 1));
		}
		[TestMethod]
		public void Complex2()
		{
			Assert.AreEqual(0, (3 * x * (y ^ 2) + 5 * (x ^ 2) * y + 7 * x + 9).CoefficientGpe(x, 3));
		}
		[TestMethod]
		public void Complex3()
		{
			Assert.AreEqual(null, (3 * sin(x) * (x ^ 2) + 2 * x + 4).CoefficientGpe(x, 2));
		}
	}
}
