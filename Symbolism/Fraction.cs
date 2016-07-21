using System;

namespace Symbolism
{
	public class Fraction : Number, IEquatable<Fraction>
	{
		public Integer Numerator { get; }
		public Integer Denominator { get; }

		public Fraction(Integer a, Integer b)
		{
			Numerator = a;
			Denominator = b;
		}

		public override string ToString() => Numerator + "/" + Denominator;

		public override DoubleFloat ToDouble() => new DoubleFloat((double)Numerator.Value / Denominator.Value);
		
		public override bool Equals(object obj) => Equals(obj as Fraction);

		public bool Equals(Fraction obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			// TODO: this doesn't consider equivalent fractions (1/2 vs 2/4)
			return Numerator == obj.Numerator && Denominator == obj.Denominator;
		}

		public override int GetHashCode() => new { Numerator, Denominator }.GetHashCode();
	}
}