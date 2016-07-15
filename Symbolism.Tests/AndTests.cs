using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;

namespace Symbolism.Tests
{
	[TestClass]
	public class AndTests
	{
		[TestMethod]
		public void NoParameters()
		{
			Assert.AreEqual(true, and());
		}
		[TestMethod] // TODO: how should and() work with numbers?
		public void SingleNonBoolParameter()
		{
			Assert.AreEqual(10, and(10));
		}
		[TestMethod]
		public void SingleTrueParameter()
		{
			Assert.AreEqual(true, and(true));
		}
		[TestMethod]
		public void SingleFalseParameter()
		{
			Assert.AreEqual(false, and(false));
		}
		[TestMethod] // not really sure what this is testing...
		public void MultipleNonBoolParameters_NoSimplification()
		{
			Assert.AreEqual(and(10, 20, 30), and(10, 20, 30));
		}
		[TestMethod]
		public void AnyFalseParameter()
		{
			Assert.AreEqual(false, and(10, false, 30));
		}
		[TestMethod]
		public void RemoveTrueParameters()
		{
			Assert.AreEqual(and(10, 30), and(10, true, 30));
		}
		[TestMethod]
		public void NestedCombinesToSingle()
		{
			Assert.AreEqual(and(10, 20, 30, 40), and(10, and(20, 30), 40));
		}
	}
}
