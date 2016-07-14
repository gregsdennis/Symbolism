using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class CoefficientTests
	{
		[TestMethod]
		public void Coefficient_Integer()
		{
			Assert.AreEqual(2, (2 * x * y).Coefficient());
		}

		[TestMethod]
		public void Coefficient_Fraction()
		{
			Assert.AreEqual(new Fraction(1, 2), (x * y / 2).Coefficient());
		}

		[TestMethod]
		public void Coefficient_DoubleFloat()
		{
			Assert.AreEqual(0.1, (0.1 * x * y).Coefficient());
		}

		[TestMethod]
		public void Coefficient_Integer_1()
		{
			Assert.AreEqual(1, (x * y).Coefficient());
		}
	}
}
