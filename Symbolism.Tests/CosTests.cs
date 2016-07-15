using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Constants;
using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class CosTests
	{
		[TestMethod]
		public void Of0()
		{
			Assert.AreEqual(1, cos(0));
		}
		[TestMethod]
		public void OfPi()
		{
			Assert.AreEqual(-1, cos(pi));
		}
		[TestMethod]
		public void OfNeg10()
		{
			Assert.AreEqual(cos(10), cos(-10));
		}
		[TestMethod]
		public void OfNeg10x()
		{
			Assert.AreEqual(cos(10*x), cos(-10*x));
		}
		[TestMethod]
		public void Of3Pi()
		{
			Assert.AreEqual(-1, cos(3*pi));
		}
		[TestMethod]
		public void Of2PiTimes3Over4()
		{
			Assert.AreEqual(0, cos(2*pi*3/4));
		}
		[TestMethod]
		public void OfxMinus3Pi()
		{
			Assert.AreEqual(cos(x + pi), cos(x - 3 * pi));
		}
		[TestMethod]
		public void OfxPlus3Pi()
		{
			Assert.AreEqual(cos(x + pi), cos(x - 3 * pi));
		}
		[TestMethod]
		public void OfxMinus2Pi()
		{
			Assert.AreEqual(cos(x), cos(x - 2 * pi));
		}
		[TestMethod]
		public void OfxPlus2Pi()
		{
			Assert.AreEqual(cos(x), cos(x - 2 * pi));
		}
		[TestMethod]
		public void OfxPlus7PiOver2()
		{
			Assert.AreEqual(cos(x + 3*pi/2), cos(x + 7*pi/2));
		}
		[TestMethod]
		public void OfxMinus3PiOver2()
		{
			Assert.AreEqual(-sin(x), cos(x - 3 * pi / 2));
		}
		[TestMethod]
		public void OfxMinusPiOver2()
		{
			Assert.AreEqual(sin(x), cos(x - 1 * pi / 2));
		}
		[TestMethod]
		public void OfxPlusPiOver2()
		{
			Assert.AreEqual(-sin(x), cos(x + 1 * pi / 2));
		}
		[TestMethod]
		public void OfxPlus3PiOver2()
		{
			Assert.AreEqual(sin(x), cos(x + 3 * pi / 2));
		}
		[TestMethod]
		public void OfPiPlusX()
		{
			Assert.AreEqual(-cos(x), cos(pi+x));
		}
		[TestMethod]
		public void OfPiPlusXPlusY()
		{
			Assert.AreEqual(-cos(x+y), cos(pi + x+y));
		}
	}
}
