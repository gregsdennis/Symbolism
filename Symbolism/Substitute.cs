using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject Substitute(this MathObject obj, MathObject a, MathObject b)
		{
			if (obj == a) return b;

			var equation = obj as Equation;
			if (equation != null)
			{
				switch (equation.Operator)
				{
					case Equation.Operators.Equal:
						return (equation.a.Substitute(a, b) == equation.b.Substitute(a, b)).Simplify();
					case Equation.Operators.NotEqual:
						return (equation.a.Substitute(a, b) != equation.b.Substitute(a, b)).Simplify();
					case Equation.Operators.LessThan:
						return (equation.a.Substitute(a, b) < equation.b.Substitute(a, b)).Simplify();
					case Equation.Operators.GreaterThan:
						return (equation.a.Substitute(a, b) > equation.b.Substitute(a, b)).Simplify();
					default:
						throw new ArgumentOutOfRangeException(nameof(equation.Operator));
				}
			}

			var power = obj as Power;
			if (power != null) return power.Base.Substitute(a, b) ^ power.Exponent.Substitute(a, b);

			var product = obj as Product;
			if (product != null)
				return new Product(product.Elements.Select(elt => elt.Substitute(a, b))).Simplify();

			var sum = obj as Sum;
			if (sum != null)
				return new Sum(sum.Elements.Select(elt => elt.Substitute(a, b))).Simplify();

			var function = obj as Function;
			if (function != null)
				return function.Map(arg => arg.Substitute(a, b));

			return obj;
		}

		public static MathObject Substitute(this MathObject obj, Equation eq)
		{
			return obj.Substitute(eq.a, eq.b);
		}

		// TODO: create params and IEnumerable<> versions of this.
		public static MathObject Substitute(this MathObject obj, List<Equation> eqs)
		{
			return eqs.Aggregate(obj, (a, eq) => a.Substitute(eq));
		}
	}
}
