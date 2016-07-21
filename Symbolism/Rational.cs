using System;

namespace Symbolism
{
	internal static class Rational
	{
		private static int Div(int a, int b)
		{
			int rem;
			return Math.DivRem(a, b, out rem);
		}

		private static int Rem(int a, int b)
		{
			int rem;
			Math.DivRem(a, b, out rem);
			return rem;
		}

		private static int Gcd(int a, int b)
		{
			int r;
			while (b != 0)
			{
				r = Rem(a, b);
				a = b;
				b = r;
			}
			return Math.Abs(a);
		}

		public static MathObject SimplifyRationalNumber(MathObject u)
		{
			if (u is Integer) return u;

			var fraction = u as Fraction;
			if (fraction != null)
			{
				var u_ = fraction;
				var n = u_.Numerator.Value;
				var d = u_.Denominator.Value;

				if (Rem(n, d) == 0) return new Integer(Div(n, d));

				var g = Gcd(n, d);

				if (d > 0) return new Fraction(new Integer(Div(n, g)), new Integer(Div(d, g)));

				if (d < 0) return new Fraction(new Integer(Div(-n, g)), new Integer(Div(-d, g)));
			}

			throw new Exception();
		}

		public static Integer Numerator(MathObject u)
		{
			// (a / b) / (c / d)
			// (a / b) * (d / c)
			// (a * d) / (b * c)

			var integer = u as Integer;
			if (integer != null) return integer;

			// if (u is Fraction) return Numerator(((Fraction)u).numerator);

			var fraction = u as Fraction;
			if (fraction != null)
				return new Integer(Numerator(fraction.Numerator).Value*Denominator(fraction.Denominator).Value);

			throw new Exception();
		}

		public static Integer Denominator(MathObject u)
		{
			// (a / b) / (c / d)
			// (a / b) * (d / c)
			// (a * d) / (b * c)

			if (u is Integer) return new Integer(1);

			// if (u is Fraction) return Denominator(((Fraction)u).denominator);

			var fraction = u as Fraction;
			if (fraction != null)
				return new Integer(Denominator(fraction.Numerator).Value*Numerator(fraction.Denominator).Value);

			throw new Exception();
		}

		public static Fraction EvaluateSum(MathObject v, MathObject w)
		{
			// a / b + c / d
			// a d / b d + c b / b d
			// (a d + c b) / (b d)
			return new Fraction(new Integer(Numerator(v).Value*Denominator(w).Value + Numerator(w).Value*Denominator(v).Value),
			                    new Integer(Denominator(v).Value*Denominator(w).Value));
		}

		public static Fraction EvaluateDifference(MathObject v, MathObject w)
		{
			return new Fraction(new Integer(Numerator(v).Value*Denominator(w).Value - Numerator(w).Value*Denominator(v).Value),
			                    new Integer(Denominator(v).Value*Denominator(w).Value));
		}

		public static Fraction EvaluateProduct(MathObject v, MathObject w)
		{
			return new Fraction(new Integer(Numerator(v).Value*Numerator(w).Value),
			                    new Integer(Denominator(v).Value*Denominator(w).Value));
		}

		public static MathObject EvaluateQuotient(MathObject v, MathObject w)
		{
			if (Numerator(w).Value == 0) return Undefined.Instance;

			return new Fraction(new Integer(Numerator(v).Value*Denominator(w).Value),
			                    new Integer(Numerator(w).Value*Denominator(v).Value));
		}

		public static MathObject EvaluatePower(MathObject v, int n)
		{
			if (Numerator(v).Value != 0)
			{
				if (n > 0) return EvaluateProduct(EvaluatePower(v, n - 1), v);
				if (n == 0) return new Integer(1);
				if (n == -1)
					return new Fraction(new Integer(Denominator(v).Value), new Integer(Numerator(v).Value));
				if (n < -1)
				{
					var s = new Fraction(new Integer(Denominator(v).Value), new Integer(Numerator(v).Value));
					return EvaluatePower(s, -n);
				}
			}

			if (n >= 1) return new Integer(0);
			if (n <= 0) return Undefined.Instance;

			throw new Exception();
		}

		private static MathObject SimplifyRNERec(MathObject u)
		{
			if (u is Integer) return u;

			var fraction = u as Fraction;
			if (fraction != null)
				if (Denominator(fraction).Value == 0) return Undefined.Instance;
				else return u;

			// NOTE: This code reduces the fraction to a double, if possible.
			//		 It appears that it assumes a fraction can be a quotient of
			//		 of expressions rather than integers (as it is now).
			//if (u is Fraction)
			//{
			//	var v = SimplifyRNERec(((Fraction)u).numerator);
			//	var w = SimplifyRNERec(((Fraction)u).denominator);

			//	if (v == Undefined.Instance || w == Undefined.Instance)
			//		return Undefined.Instance;

			//	return EvaluateQuotient(v, w);
			//}

			var sum = u as Sum;
			if (sum != null && sum.Elements.Count == 1)
			{ return SimplifyRNERec(sum.Elements[0]); }

			var difference = u as Difference;
			if (difference != null && difference.Elements.Count == 1)
			{
				var v = SimplifyRNERec(difference.Elements[0]);

				if (v == Undefined.Instance) return v;

				return EvaluateProduct(new Integer(-1), v);
			}

			if (sum != null && sum.Elements.Count == 2)
			{
				var v = SimplifyRNERec(sum.Elements[0]);
				var w = SimplifyRNERec(sum.Elements[1]);

				if (v == Undefined.Instance || w == Undefined.Instance)
					return Undefined.Instance;

				return EvaluateSum(v, w);
			}

			var product = u as Product;
			if (product != null && product.Elements.Count == 2)
			{
				var v = SimplifyRNERec(product.Elements[0]);
				var w = SimplifyRNERec(product.Elements[1]);

				if (v == Undefined.Instance || w == Undefined.Instance)
					return Undefined.Instance;

				return EvaluateProduct(v, w);
			}

			if (difference != null && difference.Elements.Count == 2)
			{
				var v = SimplifyRNERec(difference.Elements[0]);
				var w = SimplifyRNERec(difference.Elements[1]);

				if (v == Undefined.Instance || w == Undefined.Instance)
					return Undefined.Instance;

				return EvaluateDifference(v, w);
			}

			var power = u as Power;
			if (power != null)
			{
				var v = SimplifyRNERec(power.Base);

				if (v == Undefined.Instance) return v;

				// TODO: .Exponent may not be an Integer.
				return EvaluatePower(v, ((Integer)power.Exponent).Value);
			}

			throw new Exception();
		}

		public static MathObject SimplifyRNE(MathObject u)
		{
			var v = SimplifyRNERec(u);
			if (v is Undefined) return v;
			return SimplifyRationalNumber(v);
		}
	}
}