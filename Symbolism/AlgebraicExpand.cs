using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject AlgebraicExpand(this MathObject u)
		{
			var equation = u as Equation;
			if (equation != null)
			{
				var eq = equation;

				return eq.a.AlgebraicExpand() == eq.b.AlgebraicExpand();
			}

			var sum = u as Sum;
			if (sum != null)
				return new Sum(sum.Elements.Select(elt => elt.AlgebraicExpand()));

			var product = u as Product;
			if (product != null)
			{
				var v = product.Elements[0];

				return v.AlgebraicExpand()
				        .ExpandProduct((u/v).AlgebraicExpand());
			}

			var power = u as Power;
			if (power != null)
			{
				var bas = power.Base;
				var exp = power.Exponent;

				var integer = exp as Integer;
				if (integer != null && integer.Value >= 2)
					return bas.AlgebraicExpand().ExpandPower(integer.Value);

				return u;
			}

			var function = u as Function;
			if (function != null)
				return function.Map(elt => elt.AlgebraicExpand());

			return u;
		}
	}
}
