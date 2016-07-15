using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject SimplifyEquation(this MathObject expr)
		{
			var equation = expr as Equation;
			if (equation != null)
			{
				// 10 * x == 0   ->   x == 0
				// 10 * x != 0   ->   x == 0

				var product = equation.a as Product;
				if (product != null && product.Elements.OfType<Number>().Any() && equation.b == 0)
					return new Equation(new Product(product.Elements.Where(elt => !(elt is Number)).ToList()).Simplify(),
										0,
										equation.Operator).Simplify();
				// x ^ 2 == 0   ->   x == 0
				// x ^ 2 != 0   ->   x == 0

				if (equation.b == 0)
				{
					var power = equation.a as Power;
					if (power != null)
					{
						var integer = power.Exponent as Integer;
						if (integer != null && integer.Value > 0)
							return power.Base == 0;
					}
				}
			}

			var and = expr as And;
			if (and != null) return and.Map(elt => elt.SimplifyEquation());

			var or = expr as Or;
			if (or != null) return or.Map(elt => elt.SimplifyEquation());

			return expr;
		}
	}
}
