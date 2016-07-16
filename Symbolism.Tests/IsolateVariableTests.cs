using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Constants;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class IsolateVariableTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			Assert.AreEqual(x + y + z == 0, (x + y + z == 0).IsolateVariable(a));
		}
		[TestMethod]
		public void TestMethod2()
		{
			Assert.AreEqual(x == c, (x * (a + b) - x * a - x * b + x == c).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod3()
		{
			Assert.AreEqual(and(x == y, b == a), and(x == y, a == b).IsolateVariable(b));
		}
		[TestMethod]
		public void TestMethod4()
		{
			Assert.AreEqual(or(and(x == y, x == z), and(x == b, x == c)),
			                or(and(y == x, z == x), and(b == x, c == x)).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod5()
		{
			Assert.AreEqual(x == y, (0 == x - y).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod6()
		{
			Assert.AreEqual(or(and(x == (-b + sqrt((b ^ 2) + -4*a*c))/(2*a), a != 0),
			                   and(x == (-b - sqrt((b ^ 2) + -4*a*c))/(2*a), a != 0),
			                   and(x == -c/b, a == 0, b != 0),
			                   and(a == 0, b == 0, c == 0)),
			                (a*(x ^ 2) + b*x + c == 0).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod7()
		{
			Assert.AreEqual(or(and(x == sqrt(-4*a*c)/(2*a), a != 0),
			                   and(x == -sqrt(-4*a*c)/(2*a), a != 0),
			                   and(a == 0, c == 0)),
			                (a*(x ^ 2) + c == 0).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod8()
		{
			Assert.AreEqual(or(and(x == (-b + sqrt((b ^ 2) + -4*a*c))/(2*a), a != 0),
			                   and(x == (-b - sqrt((b ^ 2) + -4*a*c))/(2*a), a != 0),
			                   and(x == -c/b, a == 0, b != 0),
			                   and(a == 0, b == 0, c == 0)),
			                ((a*(x ^ 2) + c)/x == -b).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod9()
		{
			Assert.AreEqual(x == (z ^ 2) - y, (sqrt(x + y) == z).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod10()
		{
			Assert.AreEqual(a == c / (b + 1), (a * b + a == c).IsolateVariable(a));
		}
		[TestMethod]
		public void TestMethod11()
		{
			Assert.AreEqual(a == d / (b + c), (a * b + a * c == d).IsolateVariable(a));
		}
		[TestMethod]
		public void TestMethod12()
		{
			Assert.AreEqual(x == (y ^ -2), (1 / sqrt(x) == y).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod13()
		{
			Assert.AreEqual(x == (y ^ -2), (y == sqrt(x) / x).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod14()
		{
			Assert.AreEqual(-sqrt(x) + z * x == y, (-sqrt(x) + z * x == y).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod15()
		{
			Assert.AreEqual(sqrt(a + x) - z * x == -y, (sqrt(a + x) - z * x == -y).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod16()
		{
			Assert.AreEqual(sqrt(2 + x) * sqrt(3 + x) == y, (sqrt(2 + x) * sqrt(3 + x) == y).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod17()
		{
			Assert.AreEqual(x == -new Integer(5) / 2, ((x + 1) / (x + 2) == 3).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod18()
		{
			Assert.AreEqual(x == new Integer(21) / 13, ((1 + 2 * x) / (3 * x - 4) == 5).IsolateVariable(x));
		}
		[TestMethod]
		public void TestMethod19()
		{
			Assert.AreEqual(or(x == half * sqrt(16), x == -half * sqrt(16)),
							((x ^ 2) - 4 == 0).IsolateVariable(x));
		}
	}
}
