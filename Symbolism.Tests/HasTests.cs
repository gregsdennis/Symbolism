using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class HasTests
	{
		[TestMethod]
		public void SymbolHasSelf()
		{
			Assert.IsTrue(a.Has(a));
			Assert.IsTrue(a.Has(elt => elt == a));
		}
		[TestMethod]
		public void SymbolDoesNotHaveOtherSymbol()
		{
			Assert.IsFalse(a.Has(b));
			Assert.IsFalse(a.Has(elt => elt == b));
		}
		[TestMethod]
		public void EquationHasSymbol()
		{
			Assert.IsTrue((a == b).Has(a));
			Assert.IsTrue((a == b).Has(elt => elt == a));
		}
		[TestMethod]
		public void EquationDoesNotHaveOtherSymbol()
		{
			Assert.IsFalse((a == b).Has(c));
			Assert.IsFalse((a == b).Has(elt => elt == c));
		}
		[TestMethod]
		public void ExpressionHasSubExpression()
		{
			Assert.IsTrue(((a + b) ^ c).Has(a + b));
			Assert.IsTrue(((a + b) ^ c).Has(elt => elt == a + b));
		}
		[TestMethod]
		public void ExpressionHasExponentC()
		{
			Assert.IsTrue(((a + b) ^ c).Has(elt =>
				{
					var power = elt as Power;
					return power != null && power.Exponent == c;
				}));
		}
		[TestMethod]
		public void ExpressionHasSumUsingB()
		{
			Assert.IsTrue((x*(a + b + c)).Has(elt =>
				{
					var sum = elt as Sum;
					return sum != null && sum.Has(b);
				}));
		}
		[TestMethod] // TODO: encapsulate this logic (.HasDirect()).  I think it could be useful.
		public void ExpressionHasSumWithB()
		{
			Assert.IsTrue((x * (a + b + c)).Has(elt =>
				{
					var sum = elt as Sum;
					return sum != null && sum.Elements.Any(obj => obj == b);
				}));
		}
		[TestMethod]
		public void ExpressionHasProductUsingB()
		{
			Assert.IsTrue((x * (a + b + c)).Has(elt =>
				{
					var product = elt as Product;
					return product != null && product.Has(b);
				}));
		}
		[TestMethod]
		public void ExpressionDoesNotHaveProductWithB()
		{
			Assert.IsFalse((x * (a + b + c)).Has(elt =>
				{
					var product = elt as Product;
					return product != null && product.Elements.Any(obj => obj == b);
				}));
		}
	}
}
