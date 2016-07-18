using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Symbolism.Constants;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	public class Cos : Function
	{
		// TODO: this should be a Function subclass
		// NOTE: don't use % operator.  see https://ericlippert.com/2013/11/12/math-from-scratch-part-thirteen-multiplicative-inverses/
		public static MathObject Mod(MathObject x, MathObject y)
		{
			if (x is Number && y is Number)
			{
				if (y == 0) return Undefined.Instance;

				var number = x / y as Number;
				if (number != null)
				{
					var result = Convert.ToInt32(Math.Floor(number.ToDouble().Value));

					return x - y*result;
				}
			}

			throw new Exception();
		}

		// TODO: combine logic with Sin.SinProc()
		private static MathObject CosProc(params MathObject[] ls)
		{
			var half = new Fraction(1,2);

			var u = ls[0];

			if (u == 0) return 1;

			if (u == pi) return -1;

			var d0 = u as DoubleFloat;
			if (d0 != null)
				return new DoubleFloat(Math.Cos(d0.Value));

			var n0 = u as Number;
			if (n0 != null && u < 0) return new Cos(-u).Simplify();

			var prod0 = u as Product;
			if (prod0 != null)
			{
				var prod0n0 = prod0.Elements[0] as Number;
				if (prod0n0 != null && prod0n0 < 0)
					return new Cos(-u).Simplify();

				// cos(a/b * Pi)
				// a/b > 1/2         

				var prod0i0 = prod0.Elements[0] as Integer;
				var prod0f0 = prod0.Elements[0] as Fraction;
				if ((prod0i0 != null || prod0f0 != null) && prod0.Elements[0] > new Fraction(1,2) && prod0.Elements[1] == pi)
				{
					var n = prod0.Elements[0];

					if (n > 2) return new Cos(Mod(n, 2)*pi).Simplify();

					if (n > 1) return -new Cos(n*pi - pi).Simplify();

					if (n > half) return -new Cos(pi - n*pi).Simplify();

					return new Cos(n*pi).Simplify();
				}

				// cos(k/n Pi)
				// n is one of 1 2 3 4 6

				if (new List<MathObject> { 1, 2, 3, 4, 6 }.Any(elt => elt == prod0.Elements[0].Denominator()) &&
					prod0.Elements[0].Numerator() is Integer &&
					prod0.Elements[1] == pi)
				{
					var k = prod0.Elements[0].Numerator();
					var n = prod0.Elements[0].Denominator();
					var mod = Mod(k, n*2);

					if (n == 1)
					{
						if (mod == 1) return -1;
						if (mod == 0) return 1;
					}

					if (n == 2)
					{
						// TODO: this was originally Mod(k, 2) == 1, need to verify. See https://github.com/dharmatech/Symbolism/issues/8
						if (mod == 1) return 0;
					}

					if (n == 3)
					{
						if (mod == 1) return half;
						if (mod == 5) return half;
						if (mod == 2) return -half;
						if (mod == 4) return -half;
					}

					if (n == 4)
					{
						if (mod == 1) return 1 / (2 ^ half);
						if (mod == 7) return 1 / (2 ^ half);
						if (mod == 3) return -1 / (2 ^ half);
						if (mod == 5) return -1 / (2 ^ half);
					}

					if (n == 6)
					{
						if (mod == 1) return (3 ^ half) / 2;
						if (mod == 11) return (3 ^ half) / 2;
						if (mod == 5) return -(3 ^ half) / 2;
						if (mod == 7) return -(3 ^ half) / 2;
					}
				}
			}

			// cos(Pi + x + y + ...)   ->   -cos(x + y + ...)

			var sum = u as Sum;
			if (sum != null)
			{
				if (sum.Elements.Any(elt => elt == pi))
					return -new Cos(u - pi).Simplify();

				// cos(n Pi + x + y)

				// n * Pi where n is Exact && abs(n) >= 2
				Func<MathObject, bool> Product_n_Pi = elt =>
					{
						var product = elt as Product;
						return (product != null) &&
						       (product.Elements[0] is Integer || product.Elements[0] is Fraction) &&
						       Math.Abs(((Number) product.Elements[0]).ToDouble().Value) >= 2.0 &&
						       product.Elements[1] == pi;
					};

				if (sum.Elements.Any(Product_n_Pi))
				{
					var pi_elt = sum.Elements.First(Product_n_Pi);

					var piProd = pi_elt as Product;
					if (piProd != null)
					{
						var n = piProd.Elements[0];

						return new Cos(u - pi_elt + Mod(n, 2)*pi).Simplify();
					}
				}

				// cos(a + b + ... + n/2 * Pi) -> sin(a + b + ...)
				Func<MathObject, bool> Product_n_div_2_Pi = elt =>
					{
						var product = elt as Product;
						return product != null &&
						       (product.Elements[0] is Integer || product.Elements[0] is Fraction) &&
						       product.Elements[0].Denominator() == 2 &&
						       product.Elements[1] == pi;
					};

				if (sum.Elements.Any(Product_n_div_2_Pi))
				{
					var n_div_2_Pi = sum.Elements.First(Product_n_div_2_Pi);

					var other_elts = u - n_div_2_Pi;

					var divPi = n_div_2_Pi as Product;
					if (divPi != null)
					{
						var n = divPi.Elements[0].Numerator();
						var mod = Mod(n, 4);

						if (mod == 1) return -new Sin(other_elts);
						if (mod == 3) return new Sin(other_elts);
					}
				}
			}

			return new Cos(u);
		}

		public Cos(MathObject param)
			: base("cos", CosProc, param) {}

		public override MathObject Map(Func<MathObject, MathObject> map) => new Cos(map(Parameters[0])).Simplify();
	}
}