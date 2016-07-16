using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class LogicalExpandTests
	{
		[TestMethod]
		public void DistributiveProperty1()
		{
			Assert.AreEqual(or(and(a, c), and(b, c)),
			                and(or(a, b), c).LogicalExpand());
		}
		[TestMethod]
		public void DistributiveProperty2()
		{
			Assert.AreEqual(or(and(a, b), and(a, c)),
							and(a, or(b, c)).LogicalExpand());
		}
		[TestMethod]
		public void DistributiveProperty3()
		{
			Assert.AreEqual(or(and(a, b, d), and(a, c, d)),
			                and(a, or(b, c), d).LogicalExpand());
		}
		[TestMethod]
		public void DistributiveProperty4()
		{
			Assert.AreEqual(or(and(a == b, x == y), and(b == c, x == y)),
			                and(or(a == b, b == c), x == y).LogicalExpand());
		}
		[TestMethod]
		public void DistributiveProperty5()
		{
			Assert.AreEqual(or(and(a == b, c == d, x == y), and(a == b, d == a, x == y), and(b == c, c == d, x == y), and(b == c, d == a, x == y)),
			                and(or(a == b, b == c), or(c == d, d == a), x == y).LogicalExpand());
		}
	}
}
