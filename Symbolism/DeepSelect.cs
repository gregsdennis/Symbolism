using System;
using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject DeepSelect(this MathObject obj, Func<MathObject, MathObject> proc)
		{
			var result = proc(obj);

			var power = result as Power;
			if (power != null)
				return power.Base.DeepSelect(proc) ^ power.Exponent.DeepSelect(proc);

			var or = result as Or;
			if (or != null)
				return or.Map(elt => elt.DeepSelect(proc));

			var and = result as And;
			if (and != null)
				return and.Map(elt => elt.DeepSelect(proc));

			var equation = result as Equation;
			if (equation != null)
				return new Equation(equation.a.DeepSelect(proc), equation.b.DeepSelect(proc), equation.Operator);

			var sum = result as Sum;
			if (sum != null)
				return new Sum(sum.Elements.Select(elt => elt.DeepSelect(proc)).ToList()).Simplify();

			var product = result as Product;
			if (product != null)
				return new Product(product.Elements.Select(elt => elt.DeepSelect(proc)).ToList()).Simplify();

			return result;
		}
	}
}
