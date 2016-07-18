using System.Diagnostics;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	public class Fraction : Number
	{
		// TODO: encapsulate these (conflict with MathObject.Numerator() and MathObject.Denominator())
		public readonly Integer numerator;
		public readonly Integer denominator;

		public Fraction(Integer a, Integer b)
		{
			numerator = a; denominator = b;
		}

		public override string FullForm() => numerator + "/" + denominator;

		public override DoubleFloat ToDouble() => new DoubleFloat((double)numerator.Value / denominator.Value);
		//////////////////////////////////////////////////////////////////////
		
		public override bool Equals(object obj) =>
			numerator == (obj as Fraction)?.numerator
			&&
			denominator == (obj as Fraction)?.denominator;            
		
		public override int GetHashCode() => new { numerator, denominator }.GetHashCode();
		
		public override MathObject Numerator() => numerator;

		public override MathObject Denominator() => denominator;
	}
}