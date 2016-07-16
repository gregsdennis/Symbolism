using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class DegreeGpeTests
	{
		[TestMethod]
		public void SingleTerm_MultipleVars()
		{
			Assert.AreEqual(6, ((3*w*x ^ 2)*(y ^ 3)*(z ^ 4)).DegreeGpe(new List<MathObject> {x, z}));
		}
		[TestMethod]
		public void Quadratic()
		{
			Assert.AreEqual(2, ((a*x ^ 2) + b*x + c).DegreeGpe(new List<MathObject> {x}));
		}
		[TestMethod]
		public void Quadratic_SearchExpression()
		{
			Assert.AreEqual(2, (a*(sin(x) ^ 2) + b*sin(x) + c).DegreeGpe(new List<MathObject> {sin(x)}));
		}
		[TestMethod]
		public void MultipleTerms_MultipleVars()
		{
			Assert.AreEqual(7, (2*(x ^ 2)*y*(z ^ 3) + w*x*(z ^ 6)).DegreeGpe(new List<MathObject> {x, z}));
		}
	}
}
