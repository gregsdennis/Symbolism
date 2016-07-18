using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Symbolism.Constants;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	public class Sin : Function
	{
		private static MathObject SinProc(params MathObject[] ls)
		{
			var half = new Fraction(1, 2);

			var u = ls[0];

			if (u == 0) return 0;

			if (u == pi) return 0;

			var d0 = u as DoubleFloat;
			if (d0 != null)
				return new DoubleFloat(Math.Sin(d0.Value));

			if (u is Number && u < 0) return -new Sin(-u);

			var prod0 = u as Product;
			if (prod0 != null)
			{
				if (prod0.Elements[0] is Number && prod0.Elements[0] < 0) return -new Sin(-u).Simplify();

				if ((prod0.Elements[0] is Integer || prod0.Elements[0] is Fraction) &&
				    prod0.Elements[0] > half &&
				    prod0.Elements[1] == pi)
				{
					var n = prod0.Elements[0];

					if (n > 2) return new Sin(Cos.Mod(n, 2)*pi).Simplify();

					if (n > 1) return -new Sin(n*pi - pi).Simplify();

					if (n > half) return new Sin((1 - n)*pi).Simplify();

					return new Sin(n*pi).Simplify();
				}

				// sin(k/n pi)
				// n is one of 1 2 3 4 6

				if (new List<MathObject> {1, 2, 3, 4, 6}.Any(elt => elt == prod0.Elements[0].Denominator()) &&
				    prod0.Elements[0].Numerator() is Integer &&
				    prod0.Elements[1] == pi)
				{
					var k = prod0.Elements[0].Numerator();
					var n = prod0.Elements[0].Denominator();
					var mod = Cos.Mod(k, n*2);

					if (n == 1) return 0;

					if (n == 2)
					{
						if (mod == 1) return 1;
						if (mod == 3) return -1;
					}

					if (n == 3)
					{
						if (mod == 1) return (3 ^ half)/2;
						if (mod == 2) return (3 ^ half)/2;
						if (mod == 4) return -(3 ^ half)/2;
						if (mod == 5) return -(3 ^ half)/2;
					}

					if (n == 4)
					{
						if (mod == 1) return 1/(2 ^ half);
						if (mod == 3) return 1/(2 ^ half);
						if (mod == 5) return -1/(2 ^ half);
						if (mod == 7) return -1/(2 ^ half);
					}

					if (n == 6)
					{
						if (mod == 1) return half;
						if (mod == 5) return half;
						if (mod == 7) return -half;
						if (mod == 11) return -half;
					}
				}
			}

			// sin(Pi + x + y + ...)   ->   -sin(x + y + ...)

			var sum = u as Sum;
			if (sum != null)
			{
				if (sum.Elements.Any(elt => elt == pi)) return -new Sin(u - pi).Simplify();

				// sin(x + n pi)

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

						return new Sin(u - pi_elt + Cos.Mod(n, 2)*pi).Simplify();
					}
				}

				// sin(a + b + ... + n/2 * Pi)

				// NOTE: this is how pattern matching could work
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
						var mod = Cos.Mod(n, 4);

						if (mod == 1) return new Cos(other_elts).Simplify();
						if (mod == 3) return -new Cos(other_elts).Simplify();
					}
				}
			}

			return new Sin(u);
		}

		public Sin(MathObject param)
			: base("sin", SinProc, param) {}

		public override MathObject Map(Func<MathObject, MathObject> map) => new Sin(map(Parameters[0])).Simplify();
	}
}