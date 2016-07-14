using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class SubstituteTests
	{
		[TestMethod]
		public void Replace_Integer() // TODO: why would you want to do this?
		{
			Assert.AreEqual(20, new Integer(10).Substitute(new Integer(10), 20));
		}
		[TestMethod]
		public void Replace_Integer_NoMatch() // TODO: why would you want to do this?
		{
			Assert.AreEqual(10, new Integer(10).Substitute(new Integer(15), 20));
		}
		[TestMethod]
		public void Replace_Double() // TODO: why would you want to do this?
		{
			Assert.AreEqual(2.0, new DoubleFloat(1.0).Substitute(new DoubleFloat(1.0), 2.0));
		}
		[TestMethod]
		public void Replace_Double_NoMatch() // TODO: why would you want to do this?
		{
			Assert.AreEqual(1.0, new DoubleFloat(1.0).Substitute(new DoubleFloat(1.5), 2.0));
		}
		[TestMethod]
		public void Replace_Expression() // TODO: why would you want to do this?
		{
			Assert.AreEqual(new Integer(3)/4, (new Integer(1)/2).Substitute(new Integer(1)/2, new Integer(3)/4));
		}
		[TestMethod]
		public void Replace_Expression_NoMatch() // TODO: why would you want to do this?
		{
			Assert.AreEqual(new Integer(1)/2, (new Integer(1)/2).Substitute(new Integer(1)/3, new Integer(3)/4));
		}
		[TestMethod]
		public void Replace_Symbol()
		{
			Assert.AreEqual(y, x.Substitute(x, y));
		}
		[TestMethod]
		public void Replace_Symbol_NoMatch()
		{
			Assert.AreEqual(x, x.Substitute(y, y));
		}
		[TestMethod]
		public void Replace_SymbolWithNumber()
		{
			Assert.AreEqual(10 ^ y, (x ^ y).Substitute(x, 10));
		}
		[TestMethod]
		public void Replace_SymbolWithNumber_NoMatch()
		{
			Assert.AreEqual(x ^ 10, (x ^ y).Substitute(y, 10));
		}
		[TestMethod]
		public void Replace_ExpressionWithNumber()
		{
			Assert.AreEqual(10, (x ^ y).Substitute(x ^ y, 10));
		}
		[TestMethod]
		public void Replace_Symbol_Complex()
		{
			Assert.AreEqual((y ^ 2)*z, (x*y*z).Substitute(x, y));
		}
		[TestMethod]
		public void Replace_ExpressionWithSymbol_Multiply()
		{
			Assert.AreEqual(x, (x*y*z).Substitute(x*y*z, x));
		}
		[TestMethod]
		public void Replace_Symbol_AndCombine()
		{
			Assert.AreEqual(y*2 + z, (x + y + z).Substitute(x, y));
		}
		[TestMethod]
		public void Replace_ExpressionWithSymbol_Add()
		{
			Assert.AreEqual(x, (x + y + z).Substitute(x + y + z, x));
		}
		[TestMethod]
		public void Replace_MultipleSubstitutions()
		{
			Assert.AreEqual(16200, ((((x*y) ^ (new Integer(1)/2))*(z ^ 2)) ^ 2).Substitute(x, 10).Substitute(y, 20).Substitute(z, 3));
		}
		[TestMethod]
		public void Replace_Symbol_Equation()
		{
			Assert.AreEqual(x == z, (x == y).Substitute(y, z));
		}
		[TestMethod]
		public void Replace_Symbol_Inequality()
		{
			Assert.AreEqual(x != z, (x != y).Substitute(y, z));
		}
		[TestMethod]
		public void Replace_Symbol_Equation_EvalToTrue()
		{
			Assert.AreEqual(true, (x == 0).Substitute(x, 0));
		}
		[TestMethod]
		public void Replace_Symbol_Equation_EvalToFalse()
		{
			Assert.AreEqual(false, (x == 0).Substitute(x, 1));
		}
		[TestMethod]
		public void Replace_Symbol_Inequality_EvalToFalse()
		{
			Assert.AreEqual(false, (x != 0).Substitute(x, 0));
		}
		[TestMethod]
		public void Replace_Symbol_Inequality_EvalToTrue()
		{
			Assert.AreEqual(true, (x != 0).Substitute(x, 1));
		}
		[TestMethod]
		public void Replace_Symbol_InsideFunction()
		{
			Assert.AreEqual(sin(2), sin(x + x).Substitute(x, 1));
		}
		[TestMethod]
		public void Replace_Symbol_InsideFunctionWithEval()
		{
			Assert.AreEqual(0.90929742682568171, sin(x + x).Substitute(x, 1.0));
		}
		[TestMethod]
		public void Replace_SymbolWithSymbol_InsideFunction()
		{
			Assert.AreEqual(sin(2 * y), sin(2 * x).Substitute(x, y));
		}
	}
}
