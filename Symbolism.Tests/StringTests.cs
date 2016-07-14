using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class StringTests
	{
		private void AssertToString(MathObject obj, string str) => Assert.AreEqual(str, obj.ToString());

		[TestMethod]
		[Ignore] // this should work when the extraneous .Simplify() calls are removed.
		public void Full_Numbers()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(new Integer(4) + 5, "4 + 5");
		}
		[TestMethod]
		public void Full_Add3()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(x + y + z, "x + y + z");
		}
		[TestMethod]
		public void Full_AddAndMultiply()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(x + y * z, "x + y * z");
		}
		[TestMethod]
		public void Full_AddThenMultiply()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString((x + y) * z, "(x + y) * z");
		}
		[TestMethod]
		public void Full_MultiplyFunctions()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(sin(x)*cos(y), "cos(y) * sin(x)");
		}
		[TestMethod]
		public void Full_MultiParameterFunction()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(and(x, y, z), "and(x, y, z)");
		}
		[TestMethod]
		public void Full_Exponent()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(x ^ y, "x ^ y");
		}
		[TestMethod]
		public void Full_Precedence()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString((x * y) ^ (x + z), "(x * y) ^ (x + z)");
		}
		[TestMethod]
		public void Full_ExpandSubstraction()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(x - y , "x + -1 * y");
		}
		[TestMethod]
		public void Full_ExpandAllSubstraction()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(x - y - z , "x + -1 * y + -1 * z");
		}
		[TestMethod]
		public void Full_ExpandDivision()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(x / y , "x * y ^ -1");
		}
		[TestMethod]
		public void Full_ExpandSubstraction2()
		{
			MathObject.ToStringForm = ToStringForms.Full;
			AssertToString(x - (y - z), "x + -1 * (y + -1 * z)");
		}
		[TestMethod]
		public void Standard_Add2()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x + y, "x + y");
		}
		[TestMethod]
		public void Standard_Subtract2()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x - y, "x - y");
		}
		[TestMethod]
		public void Standard_Subtract3()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x - y - z, "x - y - z");
		}
		[TestMethod]
		public void Standard_Subtract3_Again()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(-x - y - z, "-x - y - z");
		}
		[TestMethod]
		public void Standard_MultipleTerms()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(2 * x - 3 * y - 4 * z, "2 * x - 3 * y - 4 * z");
		}
		[TestMethod]
		public void Standard_MaintainGrouping()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x - (y - z), "x - (y - z)");
		}
		[TestMethod]
		public void Standard_AddAndSubtract()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x - y + z, "x - y + z");
		}
		[TestMethod]
		public void Standard_Negative()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(-x, "-x");
		}
		[TestMethod]
		public void Standard_Division()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x / y, "x / y");
		}
		[TestMethod]
		public void Standard_Division_Complex()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x / (y + z), "x / (y + z)");
		}
		[TestMethod]
		public void Standard_Division_MoreComplex()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString((x + y) / (x + z), "(x + y) / (x + z)");
		}
		[TestMethod]
		public void Standard_MultiplyWithNegative()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(-x * y, "-x * y");
		}
		[TestMethod]
		public void Standard_MultiplyByNegativeTransfersNegative()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x * -y, "-x * y");
		}
		[TestMethod]
		public void Standard_FunctionWithDivision()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(sin(x / y), "sin(x / y)");
		}
		[TestMethod]
		public void Standard_VeryComplex()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x == -sqrt(2*y*(-z*a + y*(b ^ 2)/2 - c*y*d + c*y*z*sin(x)))/y,
			               "x == -sqrt(2 * y * ((b ^ 2) * y / 2 - c * d * y - a * z + c * sin(x) * y * z)) / y");
		}
		[TestMethod]
		public void Standard_MultiplyByPower()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x * (y ^ z), "x * (y ^ z)");
		}
		[TestMethod]
		public void Standard_AddByPower()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x + (y ^ z), "x + (y ^ z)");
		}
		[TestMethod]
		public void Standard_Function()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(sqrt(x), "sqrt(x)");
		}
		[TestMethod]
		public void Standard_SqrtFullForm()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			Assert.AreEqual("x ^ 1/2", sqrt(x).FullForm());
		}
		[TestMethod]
		public void Standard_PowerOfFraction()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x ^ (new Integer(1)/3), "x ^ 1/3");
		}
		[TestMethod]
		[Ignore] // should And automatically simplify?
		public void Standard_NestedAnd()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(and(and(x, y), and(x, z)), "and(and(x, y), and(x, z))");
		}
		[TestMethod]
		public void Standard_VeryComplex2()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x == sqrt(2 * (y * z - cos(a) * y * z)), "x == sqrt(2 * (y * z - cos(a) * y * z))");
		}
		[TestMethod]
		public void Standard_VeryComplex3()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(a == (-c * cos(d) - b * c * sin(d) + x * y + b * x * z) / (-y - z),
			               "a == (-c * cos(d) - b * c * sin(d) + x * y + b * x * z) / (-y - z)");
		}
		[TestMethod]
		public void Standard_VeryComplex4()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x == -(sin(y) / cos(y) + sqrt((sin(y) ^ 2) / (cos(y) ^ 2))) * (z ^ 2) / a,
			               "x == -(sin(y) / cos(y) + sqrt((sin(y) ^ 2) / (cos(y) ^ 2))) * (z ^ 2) / a");
		}
		[TestMethod]
		public void Standard_MultiplyByFunction()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x * sqrt(y), "x * sqrt(y)");
		}
		[TestMethod]
		public void Standard_DivideByFunction()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x / sqrt(y), "x / sqrt(y)");
		}
		[TestMethod]
		public void Standard_FunctionDivideByX()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(sqrt(y) / x, "sqrt(y) / x");
		}
		[TestMethod]
		public void Standard_ExpressionRatio()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString((x ^ 2) / (y ^ 3), "(x ^ 2) / (y ^ 3)");
		}
		[TestMethod]
		public void Standard_VeryComplex5()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x == y*sqrt(-8*a/(y*(z ^ 2)))*(z ^ 2)/(4*a),
			               "x == y * sqrt(-8 * a / (y * (z ^ 2))) * (z ^ 2) / (4 * a)");
		}
		[TestMethod]
		public void Standard_NegativeExpression()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(-(-1 + x), "-(-1 + x)");
		}
		[TestMethod]
		public void Standard_Equation()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x == y, "x == y");
		}
		[TestMethod]
		public void Standard_Inequality()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(x != y, "x != y");
		}
		[TestMethod]
		public void Standard_EmptyFunction()
		{
			MathObject.ToStringForm = ToStringForms.Standard;
			AssertToString(new And(), "and()");
		}
	}
}
