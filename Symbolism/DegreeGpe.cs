using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		private static int DegreeMonomialGpe(this MathObject u, List<MathObject> v)
		{
			if (v.All(u.FreeOf)) return 0;

			if (v.Contains(u)) return 1;

			var power = u as Power;
			if (power != null)
			{
				var exp = power.Exponent as Integer;
				if (exp != null && exp.Value > 1)
					return exp.Value;
			}

			var product = u as Product;
			if (product != null)
				return product.Elements.Select(elt => elt.DegreeMonomialGpe(v)).Sum();

			return 0;
		}

		// NOTE: what does GPE stand for?
		public static int DegreeGpe(this MathObject u, List<MathObject> v)
		{
			var sum = u as Sum;
			if (sum != null)
				return sum.Elements.Select(elt => elt.DegreeMonomialGpe(v)).Max();

			return u.DegreeMonomialGpe(v);
		}
	}
}