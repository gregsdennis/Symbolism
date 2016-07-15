using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;

namespace Symbolism.Tests
{
	[TestClass]
	public class FunctionTests
	{
		[TestMethod]
		public void AndMapSimpleValues()
		{
			Assert.AreEqual(and(2, 4, 6, 8, 10, 12), new And(1, 2, 3, 4, 5, 6).Map(elt => elt * 2));
		}
		[TestMethod]
		public void AndEvenValuesToFalse()
		{
			Assert.AreEqual(false, new And(1, 2, 3, 4, 5, 6).Map(elt =>
				{
					var integer = elt as Integer;
					return integer != null && integer.Value%2 == 0 ? elt : false;
				}));
		}
		[TestMethod]
		public void OrMapSimpleValues()
		{
			Assert.AreEqual(or(2, 4, 6), new Or(1, 2, 3).Map(elt => elt * 2));
		}
		[TestMethod]
		public void OrEvenValuesToFalse()
		{
			Assert.AreEqual(or(2, 4, 6), new Or(1, 2, 3, 4, 5, 6).Map(elt =>
				{
					var integer = elt as Integer;
					return integer != null && integer.Value%2 == 0 ? elt : false;
				}));
		}
	}
}
