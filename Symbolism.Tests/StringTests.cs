using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class StringTests
	{
		private void Run(MathObject obj, string normal, string simplified)
		{
			Assert.AreEqual(normal, obj.ToString());
			Assert.AreEqual(simplified, obj.Simplify().ToString());
			Console.Write($"{normal} => {simplified}");
		}

		[TestMethod]
		public void Numbers()
		{
			Run(new Integer(4) + 5, "4 + 5", "9");
		}
		[TestMethod]
		public void Add3()
		{
			Run(x + y + z, "x + y + z", "x + y + z");
		}
		[TestMethod]
		public void AddAndMultiply()
		{
			Run(x + y*z, "x + y * z", "x + y * z");
		}
		[TestMethod]
		public void AddThenMultiply()
		{
			Run((x + y)*z, "(x + y) * z", "(x + y) * z");
		}
		[TestMethod]
		public void MultiplyFunctions()
		{
			Run(sin(x)*cos(y), "sin(x) * cos(y)", "cos(y) * sin(x)");
		}
		[TestMethod]
		public void MultiParameterFunction()
		{
			Run(and(x, y, z), "and(x, y, z)", "and(x, y, z)");
		}
		[TestMethod]
		public void Exponent()
		{
			Run(x ^ y, "x ^ y", "x ^ y");
		}
		[TestMethod]
		public void Precedence()
		{
			Run((x*y) ^ (x + z), "(x * y) ^ (x + z)", "(x * y) ^ (x + z)");
		}
		[TestMethod]
		public void ExpandSubtraction()
		{
			Run(x - y, "x + -1 * y", "x + -1 * y");
		}
		[TestMethod]
		public void ExpandAllSubtraction()
		{
			Run(x - y - z, "x - y - z", "x - y - z");
		}
		[TestMethod]
		public void ExpandDivision()
		{
			Run(x/y, "x * y ^ -1", "x * y ^ -1");
		}
		[TestMethod]
		public void ExpandSubtraction2()
		{
			Run(x - (y - z), "x + -1 * (y + -1 * z)", "x + -1 * (y + -1 * z)");
		}
		[TestMethod]
		public void Add2()
		{
			Run(x + y, "x + y", "x + y");
		}
		[TestMethod]
		public void Subtract2()
		{
			Run(x - y, "x - y", "x - y");
		}
		[TestMethod]
		public void Subtract3()
		{
			Run(x - y - z, "x - y - z", "x - y - z");
		}
		[TestMethod]
		public void Subtract3_Again()
		{
			Run(-x - y - z, "-x - y - z", "-x - y - z");
		}
		[TestMethod]
		public void MultipleTerms()
		{
			Run(2*x - 3*y - 4*z, "2 * x - 3 * y - 4 * z", "2 * x + -1 * 3 * y + -1 * 4 * z");
		}
		[TestMethod]
		public void MaintainGrouping()
		{
			Run(x - (y - z), "x - (y - z)", "x - (y - z)");
		}
		[TestMethod]
		public void AddAndSubtract()
		{
			Run(x - y + z, "x - y + z", "x - y + z");
		}
		[TestMethod]
		public void Negative()
		{
			Run(-x, "-x", "-x");
		}
		[TestMethod]
		public void Division()
		{
			Run(x/y, "x / y", "x / y");
		}
		[TestMethod]
		public void Division_Complex()
		{
			Run(x/(y + z), "x / (y + z)", "x / (y + z)");
		}
		[TestMethod]
		public void Division_MoreComplex()
		{
			Run((x + y)/(x + z), "(x + y) / (x + z)", "(x + y) / (x + z)");
		}
		[TestMethod]
		public void MultiplyWithNegative()
		{
			Run(-x*y, "-x * y", "-x * y");
		}
		[TestMethod]
		public void MultiplyByNegative()
		{
			Run(x*-y, "x * -y", "-x * y");
		}
		[TestMethod]
		public void FunctionWithDivision()
		{
			Run(sin(x/y), "sin(x / y)", "sin(x / y)");
		}
		[TestMethod]
		public void VeryComplex()
		{
			Run(x == -sqrt(2*y*(-z*a + y*(b ^ 2)/2 - c*y*d + c*y*z*sin(x)))/y,
			               "x == -sqrt(2 * y * (-z * a + y * b ^ 2 / 2 - c * y * d + c * y * z * sin(x))) / y",
			                       "x == -sqrt(2 * y * (b ^ 2 * y / 2 - c * d * y - a * z + c * sin(x) * y * z)) / y");
		}
		[TestMethod]
		public void MultiplyByPower()
		{
			Run(x*(y ^ z), "x * y ^ z", "x * y ^ z");
		}
		[TestMethod]
		public void AddByPower()
		{
			Run(x + (y ^ z), "x + y ^ z", "x + y ^ z");
		}
		[TestMethod]
		public void Function()
		{
			Run(sqrt(x), "sqrt(x)", "sqrt(x)");
		}
		[TestMethod]
		public void Sqrt()
		{
			Run(sqrt(x), "x ^ 1/2", "x ^ 1/2");
		}
		[TestMethod]
		public void PowerOfFraction()
		{
			Run(x ^ (new Integer(1)/3), "x ^ 1/3", "x ^ 1/3");
		}
		[TestMethod]
		public void NestedAnd()
		{
			Run(and(and(x, y), and(x, z)), "and(and(x, y), and(x, z))", "and(x, y, z)");
		}
		[TestMethod]
		public void VeryComplex2()
		{
			Run(x == sqrt(2*(y*z - cos(a)*y*z)), "x == sqrt(2 * (y * z - cos(a) * y * z))", "x == sqrt(2 * (y * z - cos(a) * y * z))");
		}
		[TestMethod]
		public void VeryComplex3()
		{
			Run(a == (-c*cos(d) - b*c*sin(d) + x*y + b*x*z)/(-y - z),
			               "a == (-c * cos(d) - b * c * sin(d) + x * y + b * x * z) / (-y - z)",
			                       "a == (-c * cos(d) - b * c * sin(d) + x * y + b * x * z) / (-y - z)");
		}
		[TestMethod]
		public void VeryComplex4()
		{
			Run(x == -(sin(y)/cos(y) + sqrt((sin(y) ^ 2)/(cos(y) ^ 2)))*(z ^ 2)/a,
			               "x == -(sin(y) / cos(y) + sqrt((sin(y) ^ 2) / (cos(y) ^ 2))) * (z ^ 2) / a",
			                       "x == -(sin(y) / cos(y) + sqrt((sin(y) ^ 2) / (cos(y) ^ 2))) * (z ^ 2) / a");
		}
		[TestMethod]
		public void MultiplyByFunction()
		{
			Run(x*sqrt(y), "x * sqrt(y)", "x * sqrt(y)");
		}
		[TestMethod]
		public void DivideByFunction()
		{
			Run(x/sqrt(y), "x / sqrt(y)", "x / sqrt(y)");
		}
		[TestMethod]
		public void FunctionDivideByX()
		{
			Run(sqrt(y)/x, "sqrt(y) / x", "sqrt(y) / x");
		}
		[TestMethod]
		public void ExpressionRatio()
		{
			Run((x ^ 2)/(y ^ 3), "(x ^ 2) / (y ^ 3)", "(x ^ 2) / (y ^ 3)");
		}
		[TestMethod]
		public void VeryComplex5()
		{
			Run(x == y*sqrt(-8*a/(y*(z ^ 2)))*(z ^ 2)/(4*a),
			               "x == y * sqrt(-8 * a / (y * (z ^ 2))) * (z ^ 2) / (4 * a)",
			                       "x == y * sqrt(-8 * a / (y * (z ^ 2))) * (z ^ 2) / (4 * a)");
		}
		[TestMethod]
		public void NegativeExpression()
		{
			Run(-(-1 + x), "-(-1 + x)", "1 + x");
		}
		[TestMethod]
		public void Equation()
		{
			Run(x == y, "x == y", "x == y");
		}
		[TestMethod]
		public void Inequality()
		{
			Run(x != y, "x != y", "x != y");
		}
		[TestMethod]
		public void EmptyFunction()
		{
			Run(new And(), "and()", "True");
		}
		[TestMethod]
		public void CombineTerms()
		{
			Run(x + x, "x + x", "2 * x");
		}
	}
}
