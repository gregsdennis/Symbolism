using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class SimplifyTests
	{
		private void Run(MathObject a, MathObject b)
		{
			Console.WriteLine($"{a} => {b}");
			Assert.AreEqual(b, a.Simplify());
		}
		private void RunNot(MathObject a, MathObject b)
		{
			Console.WriteLine($"{a} != {b}");
			Assert.AreNotEqual(b, a.Simplify());
		}

		[TestMethod]
		public void CombineCoefficients_Add2()
		{
			Run(x + x, 2 * x);
		}
		[TestMethod]
		public void SubtractSelf()
		{
			Run(x - x, 0);
		}
		[TestMethod]
		public void CombineCoefficients_Add3()
		{
			Run(x + x + x, 3*x);
		}
		[TestMethod]
		public void CombineConstants_Add()
		{
			Run(5 + x + 2, 7 + x);
		}
		[TestMethod]
		public void CombineConstants_Add_Complex()
		{
			Run(3 + x + 5 + x, 8 + 2*x);
		}
		[TestMethod]
		public void CombineCoefficients_Complex()
		{
			Run(4*x + 3*x, 7*x);
		}
		[TestMethod]
		public void CombineTerms()
		{
			Run(x + y + z + x + y + z, 2*x + 2*y + 2*z);
		}
		[TestMethod]
		public void ConvertNegativeToMinus()
		{
			Run(10 + x*-1, 10 - x);
		}
		[TestMethod]
		public void ConvertNegativePowerToDivide()
		{
			Run(x*(y ^ -1), x/y);
		}
		[TestMethod]
		public void RemoveExtra0()
		{
			Run(x + (new Integer(0) - 3), x - 3);
		}
		[TestMethod]
		public void RemoveExtra1()
		{
			Run(x*(new Integer(1)/3), new Fraction(1, 3)*x);
		}
		[TestMethod]
		public void CombineConstants_Divide()
		{
			Run(6*x*y/3, 2*x*y);
		}
		[TestMethod]
		public void CombineConstants_Power_Complex()
		{
			// ReSharper disable ArrangeRedundantParentheses
			Run(((x ^ new Integer(1) / 2) ^ new Integer(1) / 2) ^ 8, x ^ 2);
			// ReSharper restore ArrangeRedundantParentheses
		}
		[TestMethod]
		public void CombineConstants_sqrt()
		{
			// ReSharper disable ArrangeRedundantParentheses
			Run(sqrt(sqrt(x)) ^ 8, x ^ 2);
			// ReSharper restore ArrangeRedundantParentheses
		}
		[TestMethod]
		public void Combine_Exponents()
		{
			Run(x*x*y*(y ^ 2), (x ^ 2)*(y ^ 3));
		}
		[TestMethod]
		public void Combine_Exponents_Complex()
		{
			Run((((x * y) ^ (new Integer(1) / 2)) * (z ^ 2)) ^ 2, x * y * (z ^ 4));
		}
		[TestMethod]
		public void DivideBySelf()
		{
			Run(x/x, 1);
		}
		[TestMethod]
		public void MultiplicativeInverses()
		{
			Run(x/y*y/x, 1);
		}
		[TestMethod]
		public void CombineConstants_Power()
		{
			Run((x ^ 2)*(x ^ 3), x ^ 5);
		}
		[TestMethod]
		public void CombineTermsAndSort()
		{
			Run(x + y + x + z + 5 + z, 5 + 2*x + y + 2*z);
		}
		[TestMethod]
		public void CombineTerms_MultipleCoefficients()
		{
			Run(1*x + 2*x + 3*x, 6*x);
		}
		[TestMethod]
		public void CombineTerms_MultipleCoefficientsWithDifference()
		{
			Run(1 * x - 2 * x + 5 * x, 4 * x);
		}
		[TestMethod]
		//[Ignore]
		public void CombineTerms_FractionalCoefficients()
		{
			Run(new Integer(1)/2*x + new Integer(3)/4*x, new Fraction(5, 4)*x);
		}
		[TestMethod]
		public void CombineCoefficients_DoubleAndInt()
		{
			Run(1.2*x + 3*x, 4.2*x);
		}
		[TestMethod]
		public void CombineCoefficients_IntAndDouble()
		{
			Run(3*x + 1.2*x, 4.2*x);
		}
		[TestMethod]
		public void CombineTerms_MultiplyDoubleAndInt_Toleranced()
		{
			DoubleFloat.Tolerance = 0.00001;
			Run(1.2*x*3*y, 3.5999999999999996*x*y);
			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void CombineTerms_MultiplyIntAndDouble_Toleranced()
		{
			DoubleFloat.Tolerance = 0.00001;
			Run(3*x*1.2*y, 3.5999999999999996*x*y);
			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void CombineTermsAndSort_Double()
		{
			Run(3.4*x*1.2*y, 4.08*x*y);
		}
		[TestMethod]
		public void Commutative_Sum()
		{
			Run(a + b, b + a);
		}
		[TestMethod]
		public void Commutative_Product()
		{
			Run(a * b, b * a);
		}
		[TestMethod]
		public void Associative_Sum()
		{
			Run((a + b) + c, a + (b + c));
		}
		[TestMethod]
		public void Associative_Product()
		{
			Run((a*b)*c, a*(b*c));
		}
		[TestMethod]
		public void ZeroToPower()
		{
			Run(0 ^ x, 0);
		}
		[TestMethod]
		public void OneToPower()
		{
			Run(1 ^ x, 1);
		}
		[TestMethod]
		public void BaseToZeroPower()
		{
			Run(x ^ 0, 1);
		}
		[TestMethod]
		public void BaseToOnePower()
		{
			Run(x ^ 1, x);
		}
		[TestMethod]
		public void MultiplyByZero()
		{
			Run(x*0, 0);
		}
		[TestMethod]
		public void AddZero()
		{
			Run(x + 0, x);
		}
		[TestMethod]
		public void MultiplyByNegativeOne()
		{
			Run(-1*x, -x);
		}
		[TestMethod]
		public void MultiplyByNegativeOne_Complex()
		{
			Run(x + -1* y, x - y);
		}
		[TestMethod]
		public void MultiplyByOne()
		{
			Run(x*1, x);
		}
		[TestMethod]
		public void MultiplyByOne_OtherWay()
		{
			Run(1*x, x);
		}
		[TestMethod]
		public void CombineCoefficients_Function()
		{
			Run(sin(x + y) + sin(x + y), 2*sin(x + y));
		}
		[TestMethod]
		public void CombineCoefficients_InsideFunction()
		{
			Run(sin(x + x), sin(2*x));
		}
		[TestMethod]
		public void CombineCoefficients_FunctionEvaluation()
		{
			Run(sin(new DoubleFloat(3.14159/2)), 0.99999999999911982);
		}
		[TestMethod]
		public void DifferentEquationsNotEqual()
		{
			RunNot(a == b, a != b);
		}
		[TestMethod]
		public void DoubleEval()
		{
			Run(new DoubleFloat(3.0) - 2.0, 1.0);
		}
		[TestMethod]
		public void SqrtTimesSelf()
		{
			Run(sqrt(a*b)*sqrt(a*b), a*b);
		}
		[TestMethod]
		public void SqrtTimesSelf_Complex()
		{
			Run(sqrt(a*b)*(sqrt(a*b)/a)/c, b/c);
		}
		[TestMethod]
		public void NestedAnd()
		{
			Run(and(and(x, y), and(x, z)).SimplifyLogical(), and(x, y, z));
		}
		[TestMethod]
		public void AdditionIsNotMultiplication()
		{
			RunNot(x + y, x * y);
		}
		[TestMethod]
		public void LogicalRemoveDuplicates()
		{
			Run(and(a, b, c, a), and(a, b, c));
		}
		[TestMethod]
		public void TermSequencing_Sum()
		{
			Run(x + z - y, x - y + z);
		}
		[TestMethod]
		public void TermSequencing_Product()
		{
			Run(z/y*x, x*z/y);
		}
		[TestMethod]
		public void QuotientGrouping()
		{
			Run(x*y/z/a, x*y/(z*a));
		}
		[TestMethod]
		public void NegativeProductMinusSomething()
		{
			Run(-x*cos(y) - z, -(cos(y)*x) - z);
		}
		[TestMethod]
		public void NegativeProduct()
		{
			Run(-x*y, -(x*y));
		}
		[TestMethod]
		public void SortingWithNegative()
		{
			Run(-x*cos(y), -(x*cos(y)));
		}

	    [TestMethod]
	    public void DifferenceAllCoefficientNegativeOne()
	    {
	        Run(-y - x, -x - y);
	    }
	}
}