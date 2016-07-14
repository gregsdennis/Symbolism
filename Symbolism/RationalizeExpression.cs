using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		private static MathObject RationalizeSum(MathObject u, MathObject v)
		{
			var m = u.Numerator();
			var r = u.Denominator();
			var n = v.Numerator();
			var s = v.Denominator();

			if (r == 1 && s == 1) return u + v;

			return RationalizeSum(m*s, n*r)/(r*s);
		}

		public static MathObject RationalizeExpression(this MathObject u)
		{
			var equation = u as Equation;
			if (equation != null)
				return new Equation(equation.a.RationalizeExpression(),
				                    equation.b.RationalizeExpression(),
				                    equation.Operator);

			var power = u as Power;
			if (power != null)
				return power.Base.RationalizeExpression() ^ power.Exponent;

			var product = u as Product;
			if (product != null)
				return
					new Product(product.Elements.Select(elt => elt.RationalizeExpression())).Simplify();

			var sum = u as Sum;
			if (sum != null)
			{
				var f = sum.Elements[0];

				var g = f.RationalizeExpression();
				var r = (u - f).RationalizeExpression();

				return RationalizeSum(g, r);
			}

			return u;
		}
	}
}
