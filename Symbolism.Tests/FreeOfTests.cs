using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class FreeOfTests
	{
		[TestMethod]
		public void ExpressionNotFreeOfSymbol()
		{
			Assert.IsFalse((a + b).FreeOf(b));
		}
		[TestMethod]
		public void ExpressionFreeOfSymbol()
		{
			Assert.IsTrue((a + b).FreeOf(c));
		}
		[TestMethod]
		public void ExpressionNotFreeOfExpression()
		{
			Assert.IsFalse(((a + b) ^ c).FreeOf(a + b));
		}
		[TestMethod]
		public void ExpressionNotFreeOfFunction()
		{
			Assert.IsFalse((sin(x) + 2*x).FreeOf(sin(x)));
		}
		[TestMethod] // TODO: should FreeOf() return true here?
		public void ExpressionFreeOfExpression()
		{
			Assert.IsTrue(((a + b + c)*d).FreeOf(a + b));
		}
		[TestMethod] // TODO: should FreeOf() return true here?
		public void ExpressionFreeOfSymbol_Complex()
		{
			Assert.IsTrue(((y + 2*x - y)/x).FreeOf(x));
		}
		[TestMethod] // TODO: should FreeOf() return true here?
		public void ExpressionFreeOfExpression_Complex()
		{
			Assert.IsTrue(((x*y) ^ 2).FreeOf(x*y));
		}
	}
}
