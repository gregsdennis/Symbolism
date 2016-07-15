using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Symbolism.Tests
{
	[TestClass]
	public class DoubleFloatTests
	{
		[TestInitialize]
		public void Initialize()
		{
			DoubleFloat.Tolerance = 0.000000001;
		}
		[TestCleanup]
		public void CleanUp()
		{
			DoubleFloat.Tolerance = null;
		}

		[TestMethod]
		public void ExactMatch()
		{
			Assert.AreEqual<DoubleFloat>(1.2, 1.2);
		}
		[TestMethod]
		public void BarelyOutsideTolerance()
		{
			Assert.AreNotEqual<DoubleFloat>(1.20000001, 1.20000002);
		}
		[TestMethod]
		public void WithinTolerance()
		{
			Assert.AreEqual<DoubleFloat>(1.2000000000001, 1.200000000002);
		}
		[TestMethod]
		public void WayOutsideTolerance()
		{
			Assert.AreNotEqual<DoubleFloat>(1.2, 1.23);
		}
	}
}
