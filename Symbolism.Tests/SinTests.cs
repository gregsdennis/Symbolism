using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Constants;
using static Symbolism.Functions;
using static Symbolism.Tests.Constants;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class SinTests
	{
		[TestMethod]
		public void Of0()
		{
			Assert.AreEqual(0, sin(0));
		}
		[TestMethod]
		public void OfPi()
		{
			Assert.AreEqual(0, sin(pi));
		}
		[TestMethod]
		public void OfNegativeNumber()
		{
			Assert.AreEqual(-sin(10), sin(-10));
		}
		[TestMethod]
		public void OfNegativeSymbol()
		{
			Assert.AreEqual(-sin(x), sin(-x));
		}
		[TestMethod]
		public void OfNegativeExpression()
		{
			Assert.AreEqual(-sin(5*x), sin(-5*x));
		}
		[TestMethod]
		public void OfNeg2Pi()
		{
			Assert.AreEqual(0, sin(-2*pi));
		}
		[TestMethod]
		public void OfNegPi()
		{
			Assert.AreEqual(0, sin(-1*pi));
		}
		[TestMethod]
		public void Of2Pi()
		{
			Assert.AreEqual(0, sin(2*pi));
		}
		[TestMethod]
		public void Of3Pi()
		{
			Assert.AreEqual(0, sin(3*pi));
		}
		[TestMethod]
		public void OfNeg7PiOver2()
		{
			Assert.AreEqual(1, sin(-7*pi/2));
		}
		[TestMethod]
		public void OfNeg5PiOver2()
		{
			Assert.AreEqual(-1, sin(-5*pi/2));
		}
		[TestMethod]
		public void OfNeg3PiOver2()
		{
			Assert.AreEqual(1, sin(-3*pi/2));
		}
		[TestMethod]
		public void OfNegPiOver2()
		{
			Assert.AreEqual(-1, sin(-1*pi/2));
		}
		[TestMethod]
		public void OfPiOver2()
		{
			Assert.AreEqual(1, sin(1*pi/2));
		}
		[TestMethod]
		public void Of4PiOver2()
		{
			Assert.AreEqual(-1, sin(3*pi/2));
		}
		[TestMethod]
		public void Of5PiOver2()
		{
			Assert.AreEqual(1, sin(5*pi/2));
		}
		[TestMethod]
		public void Of7PiOver2()
		{
			Assert.AreEqual(-1, sin(7*pi/2));
		}
		[TestMethod]
		public void OfNeg4PiOver3()
		{
			Assert.AreEqual(sqrt(3)/2, sin(-4*pi/3));
		}
		[TestMethod]
		public void OfNeg2PiOver3()
		{
			Assert.AreEqual(-sqrt(3)/2, sin(-2*pi/3));
		}
		[TestMethod]
		public void OfNegPiOver3()
		{
			Assert.AreEqual(-sqrt(3)/2, sin(-1*pi/3));
		}
		[TestMethod]
		public void OfPiOver3()
		{
			Assert.AreEqual(sqrt(3)/2, sin(1*pi/3));
		}
		[TestMethod]
		public void Of2PiOver3()
		{
			Assert.AreEqual(sqrt(3)/2, sin(2*pi/3));
		}
		[TestMethod]
		public void Of4PiOver3()
		{
			Assert.AreEqual(-sqrt(3)/2, sin(4*pi/3));
		}
		[TestMethod]
		public void Of5PiOver3()
		{
			Assert.AreEqual(-sqrt(3)/2, sin(5*pi/3));
		}
		[TestMethod]
		public void Of7PiOver3()
		{
			Assert.AreEqual(sqrt(3)/2, sin(7*pi/3));
		}
		[TestMethod]
		public void OfNeg3PiOver4()
		{
			Assert.AreEqual(-1/sqrt(2), sin(-3*pi/4));
		}
		[TestMethod]
		public void OfNegPiOver4()
		{
			Assert.AreEqual(-1/sqrt(2), sin(-1*pi/4));
		}
		[TestMethod]
		public void OfPiOver4()
		{
			Assert.AreEqual(1/sqrt(2), sin(1*pi/4));
		}
		[TestMethod]
		public void Of3PiOver4()
		{
			Assert.AreEqual(1/sqrt(2), sin(3*pi/4));
		}
		[TestMethod]
		public void Of5PiOver4()
		{
			Assert.AreEqual(-1/sqrt(2), sin(5*pi/4));
		}
		[TestMethod]
		public void Of7PiOver4()
		{
			Assert.AreEqual(-1/sqrt(2), sin(7*pi/4));
		}
		[TestMethod]
		public void Of9PiOver4()
		{
			Assert.AreEqual(1/sqrt(2), sin(9*pi/4));
		}
		[TestMethod]
		public void Of11PiOver4()
		{
			Assert.AreEqual(1/sqrt(2), sin(11*pi/4));
		}
		[TestMethod]
		public void OfNeg5PiOver6()
		{
			Assert.AreEqual(-half, sin(-5*pi/6));
		}
		[TestMethod]
		public void OfNegPiOver6()
		{
			Assert.AreEqual(-half, sin(-1*pi/6));
		}
		[TestMethod]
		public void OfPiOver6()
		{
			Assert.AreEqual(half, sin(1*pi/6));
		}
		[TestMethod]
		public void Of5PiOver6()
		{
			Assert.AreEqual(half, sin(5*pi/6));
		}
		[TestMethod]
		public void Of7PiOver6()
		{
			Assert.AreEqual(-half, sin(7*pi/6));
		}
		[TestMethod]
		public void Of11PiOver6()
		{
			Assert.AreEqual(-half, sin(11*pi/6));
		}
		[TestMethod]
		public void Of13PiOver6()
		{
			Assert.AreEqual(half, sin(13*pi/6));
		}
		[TestMethod]
		public void Of17PiOver6()
		{
			Assert.AreEqual(half, sin(17*pi/6));
		}
		[TestMethod]
		public void Of15PiOver7()
		{
			Assert.AreEqual(sin(pi/7), sin(15*pi/7));
		}
		[TestMethod]
		public void Of8PiOver7()
		{
			Assert.AreEqual(-sin(pi/7), sin(8*pi/7));
		}
		[TestMethod]
		public void Of4PiOver7()
		{
			Assert.AreEqual(sin(3*pi/7), sin(4*pi/7));
		}
		[TestMethod]
		public void OfxMinus3Pi()
		{
			Assert.AreEqual(sin(x + pi), sin(x - 3*pi));
		}
		[TestMethod]
		public void OfxMinus2Pi()
		{
			Assert.AreEqual(sin(x), sin(x - 2*pi));
		}
		[TestMethod]
		public void OfxPlus2Pi()
		{
			Assert.AreEqual(sin(x), sin(x + 2*pi));
		}
		[TestMethod]
		public void OfxPlus3Pi()
		{
			Assert.AreEqual(sin(x + pi), sin(x + 3*pi));
		}
		[TestMethod]
		public void OfxPlus7PiOver2()
		{
			Assert.AreEqual(sin(x + 3 * pi / 2), sin(x + 7 * pi / 2));
		}
		[TestMethod]
		public void OfxMinus3PiOver2()
		{
			Assert.AreEqual(cos(x), sin(x - 3 * pi / 2));
		}
		[TestMethod]
		public void OfxMinusPiOver2()
		{
			Assert.AreEqual(-cos(x), sin(x - 1 * pi / 2));
		}
		[TestMethod]
		public void OfxPlusPiOver2()
		{
			Assert.AreEqual(cos(x), sin(x +1 * pi / 2));
		}
		[TestMethod]
		public void OfxPlus3PiOver2()
		{
			Assert.AreEqual(-cos(x), sin(x + 3 * pi / 2));
		}
		[TestMethod]
		public void OfPiPlusX()
		{
			Assert.AreEqual(-sin(x), sin(pi + x));
		}
		[TestMethod]
		public void OfPiPlusXPlusY()
		{
			Assert.AreEqual(-sin(x+y), sin(pi + x + y));
		}
	}
}
