using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;

namespace Symbolism.Tests
{
	[TestClass]
	public class OrTests
	{
		[TestMethod] // TODO: how should and() work with numbers?
		public void SingleNonBoolParameter()
		{
			Assert.AreEqual(10, or(10));
		}
		[TestMethod]
		public void SingleTrueParameter()
		{
			Assert.AreEqual(true, or(true));
		}
		[TestMethod]
		public void SingleFalseParameter()
		{
			Assert.AreEqual(false, or(false));
		}
		[TestMethod]
		public void MultipleNonBoolParameters_NoSimplification()
		{
			Assert.AreEqual(or(10, 20, 30), or(10, 20, 30));
		}
		[TestMethod]
		public void AnyTrueParameters()
		{
			Assert.AreEqual(true, or(10, true, 30));
		}
		[TestMethod]
		public void RemoveFalseParameter()
		{
			Assert.AreEqual(or(10, 30), or(10, false, 30));
		}
		[TestMethod]
		public void MultipleFalseParameters()
		{
			Assert.AreEqual(false, or(false, false));
		}
		[TestMethod]
		public void NestedCombinesToSingle()
		{
			Assert.AreEqual(or(10, 20, 30, 40), or(10, or(20, 30), 40));
		}
	}
}
