using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class SimplifyTests
	{
		[TestMethod]
		public void CombineCoefficients_Add2()
		{
			Assert.AreEqual(x + x, 2*x);
		}
		[TestMethod]
		public void CombineCoefficients_Add3()
		{
			Assert.AreEqual(x + x + x, 3*x);
		}
		[TestMethod]
		public void CombineConstants_Add()
		{
			Assert.AreEqual(5 + x + 2, 7 + x);
		}
		[TestMethod]
		public void CombineConstants_Add_Complex()
		{
			Assert.AreEqual(3 + x + 5 + x, 8 + 2*x);
		}
		[TestMethod]
		public void CombineCoefficients_Complex()
		{
			Assert.AreEqual(4*x + 3*x, 7*x);
		}
		[TestMethod]
		public void CombineTerms()
		{
			Assert.AreEqual(x + y + z + x + y + z, 2*x + 2*y + 2*z);
		}
		[TestMethod]
		public void ConvertNegativeToMinus()
		{
			Assert.AreEqual(10 - x, 10 + x*-1);
		}
		[TestMethod]
		public void HideMultiplyBy1AndSort()
		{
			Assert.AreEqual(x*y/3, new Integer(1)/3*x*y);
		}
		[TestMethod]
		public void ConvertNegativePowerToDivide()
		{
			Assert.AreEqual(x/y, x*(y ^ -1));
		}
		[TestMethod]
		public void HideMultiplyBy1()
		{
			Assert.AreEqual(x/3, x*(new Integer(1)/3));
		}
		[TestMethod]
		public void CombineConstants_Divide()
		{
			Assert.AreEqual(6*x*y/3, 2*x*y);
		}
		[TestMethod]
		public void CombineConstants_Power_Complex()
		{
			// ReSharper disable ArrangeRedundantParentheses
			Assert.AreEqual(((x ^ new Integer(1)/2) ^ new Integer(1)/2) ^ 8, x ^ 2);
			// ReSharper restore ArrangeRedundantParentheses
		}
		[TestMethod]
		public void Combine_Coefficients()
		{
			Assert.AreEqual((((x*y) ^ (new Integer(1)/2))*(z ^ 2)) ^ 2, x*y*(z ^ 4));
		}
		[TestMethod]
		public void DivideBySelf()
		{
			Assert.AreEqual(x/x, 1);
		}
		[TestMethod]
		public void MultiplicativeInverses()
		{
			Assert.AreEqual(x/y*y/x, 1);
		}
		[TestMethod]
		public void CombineConstants_Power()
		{
			Assert.AreEqual((x ^ 2)*(x ^ 3), x ^ 5);
		}
		[TestMethod]
		public void CombineTermsAndSort()
		{
			Assert.AreEqual(x + y + x + z + 5 + z, 5 + 2*x + y + 2*z);
		}
		[TestMethod]
		public void CombineTerms_MultipleCoefficients()
		{
			Assert.AreEqual((new Integer(1)/2)*x + (new Integer(3)/4)*x, new Integer(5)/4*x);
		}
		[TestMethod]
		public void CombineCoefficients_DoubleAndInt()
		{
			Assert.AreEqual(1.2*x + 3*x, 4.2*x);
		}
		[TestMethod]
		public void CombineCoefficients_IntAndDouble()
		{
			Assert.AreEqual(3*x + 1.2*x, 4.2*x);
		}
		[TestMethod]
		public void CombineTerms_MultiplyDoubleAndInt_Toleranced()
		{
			Assert.AreEqual(1.2*x*3*y, 3.5999999999999996*x*y);
		}
		[TestMethod]
		public void CombineTerms_MultiplyIntAndDouble_Toleranced()
		{
			Assert.AreEqual(3*x*1.2*y, 3.5999999999999996*x*y);
		}
		[TestMethod]
		public void CombineTermsAndSort_Double()
		{
			Assert.AreEqual(3.4*x*1.2*y, 4.08*x*y);
		}
		[TestMethod]
		public void Reflexive()
		{
			Assert.AreEqual(a == b, a == b);
		}
		[TestMethod]
		[Ignore] // maybe need an ".Equivalent(MathObject)
		public void Symmetric()
		{
			Assert.AreEqual(a == b, b == a);
		}
		[TestMethod]
		public void ZeroToPower()
		{
			Assert.AreEqual(0 ^ x, 0);
		}
		[TestMethod]
		public void OneToPower()
		{
			Assert.AreEqual(1 ^ x, 1);
		}
		[TestMethod]
		public void BaseToZeroPower()
		{
			Assert.AreEqual(x ^ 0, 1);
		}
		[TestMethod]
		public void BaseToOnePower()
		{
			Assert.AreEqual(x ^ 1, x);
		}
		[TestMethod]
		public void MultiplyByZero()
		{
			Assert.AreEqual(x*0, 0);
		}
		[TestMethod]
		public void AddZero()
		{
			Assert.AreEqual(x + 0, x);
		}
		[TestMethod]
		public void MultiplyByNegativeOne()
		{
			Assert.AreEqual(-x, -1*x);
		}
		[TestMethod]
		public void MultiplyByNegativeOne_Complex()
		{
			Assert.AreEqual(x - y, x + -1*y);
		}
		[TestMethod]
		public void MultiplyByOne()
		{
			Assert.AreEqual(x, x*1);
		}
		[TestMethod]
		public void MultiplyByOne_OtherWay()
		{
			Assert.AreEqual(x, 1*x);
		}
		[TestMethod]
		public void NotEqual_Symbols()
		{
			Assert.AreEqual(true, x != y);
		}
		[TestMethod]
		public void NotEqual_SymbolAndNumber()
		{
			Assert.AreEqual(true, x != 10);
		}
		[TestMethod]
		public void CombineCoefficients_Function()
		{
			Assert.AreEqual(sin(x + y) + sin(x + y), 2*sin(x + y));
		}
		[TestMethod]
		public void CombineCoefficients_InsideFunction()
		{
			Assert.AreEqual(sin(x + x), sin(2*x));
		}
		[TestMethod]
		public void CombineCoefficients_FunctionEvaluation()
		{
			Assert.AreEqual(sin(new DoubleFloat(3.14159/2)), 0.99999999999911982);
		}
		[TestMethod]
		public void DifferentEquationsNotEqual()
		{
			Assert.AreNotEqual(a == b, a != b);
		}
		[TestMethod]
		public void DoubleEval()
		{
			Assert.AreEqual(1.0, new DoubleFloat(3.0) - 2.0);
		}
		[TestMethod]
		public void SqrtTimesSelf_Complex()
		{
			Assert.AreEqual(b/c, sqrt(a*b)*(sqrt(a*b)/a)/c);
		}
		[TestMethod]
		public void NestedAnd()
		{
			Assert.AreEqual(and(x, y, z), and(and(x, y), and(x, z)).SimplifyLogical());
		}
		[TestMethod]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void EqualityTrue()
		{
			Assert.AreEqual(true, (new Integer(0) == new Integer(0)).Simplify());
		}
		[TestMethod]
		public void EqualityFalse()
		{
			Assert.AreEqual(false, (new Integer(0) == new Integer(1)).Simplify());
		}
		[TestMethod]
		public void InequalityTrue()
		{
			Assert.AreEqual(true, (new Integer(0) != new Integer(1)).Simplify());
		}
		[TestMethod]
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void InequalityFalse()
		{
			Assert.AreEqual(false, (new Integer(0) != new Integer(0)).Simplify());
		}
		[TestMethod]
		public void AdditionIsNotMultiplication()
		{
			Assert.AreNotEqual(x + y, x * y);
		}
	}
}