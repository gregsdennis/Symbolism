using System;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static Tuple<MathObject, int> CoefficientMonomialGpe(this MathObject u, MathObject x)
		{
			if (u == x) return Tuple.Create((MathObject) 1, 1);

			var power = u as Power;
			if (power != null)
			{
				var exp = power.Exponent as Integer;
				if (power.Base == x &&
				    exp != null &&
				    exp.Value > 1)
					return Tuple.Create((MathObject) 1, exp.Value);
			}

			var product = u as Product;
			if (product != null)
			{
				var m = 0;
				var c = u;

				foreach (var elt in product.Elements)
				{
					var f = elt.CoefficientMonomialGpe(x);

					if (f == null) return null;

					if (f.Item2 != 0)
					{
						m = f.Item2;
						c = u/(x ^ m);
					}
				}

				return Tuple.Create(c, m);
			}

			return u.FreeOf(x) ? Tuple.Create(u, 0) : null;
		}

		public static MathObject CoefficientGpe(this MathObject u, MathObject x, int j)
		{
			var sum = u as Sum;
			if (sum == null)
			{
				var f = u.CoefficientMonomialGpe(x);

				if (f == null) return null;

				if (f.Item2 == j) return f.Item1;

				return 0;
			}

			if (u == x) return j == 1 ? 1 : 0;

			var c = (MathObject) 0;

			foreach (var elt in sum.Elements)
			{
				var f = elt.CoefficientMonomialGpe(x);

				if (f == null) return null;

				if (f.Item2 == j) c = c + f.Item1;
			}

			return c;
		}
	}
}
