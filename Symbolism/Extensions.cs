using System;
using System.Collections.Generic;
using System.Linq;

using static Symbolism.Constants;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject ToRadians(this MathObject n) { return n * pi / 180; }

		public static MathObject ToDegrees(this MathObject n) { return 180 * n / pi; }

		public static MathObject ToRadians(this int n) { return new Integer(n) * pi / 180; }

		public static MathObject ToDegrees(this int n) { return 180 * new Integer(n) / pi; }

		internal static MathObject Coefficient(this MathObject u)
		{
			var number = u as Number;
			if (number != null) return number;

			var product = u as Product;
			if (product != null && product.Elements.OfType<Number>().Any())
				return new Product(product.Elements.OfType<Number>()).Simplify();

			var difference = u as Difference;
			if (difference != null && difference.Elements.Count == 1)
				return -1;

			return 1;
		}

		public static MathObject Term(this MathObject u)
		{
			var number = u as Number;
			if (number != null) return 1;

			var product = u as Product;
			if (product != null)
				return new Product(product.Elements.Where(elt => !(elt is Number))).Simplify();

			var difference = u as Difference;
			if (difference != null && difference.Elements.Count == 1)
				return difference.Elements[0];

			return u;
		}

		internal static MathObject Base(this MathObject u)
		{
			var power = u as Power;
			return power != null ? power.Base : u;
		}

		internal static MathObject Exponent(this MathObject u)
		{
			var power = u as Power;
			return power != null ? power.Exponent : 1;
		}

		internal static MathObject Numerator(this MathObject u)
		{
			var product = u as Product;
			if (product != null) return new Product(product.Elements.Select(elt => elt.Numerator()));

			var quotient = u as Quotient;
			if (quotient != null) return new Product(product.Elements.Select(elt => elt.Numerator()));

			var fraction = u as Fraction;
			if (fraction != null) return fraction.Numerator;

			var power = u as Power;
			if (power != null &&
			    (power.Exponent is Integer || power.Exponent is Fraction) &&
			    power.Exponent < 0) return 1;

			return u;
		}

		internal static MathObject Denominator(this MathObject u)
		{
			var product = u as Product;
			if (product != null) return new Product(product.Elements.Select(elt => elt.Denominator()));

			var fraction = u as Fraction;
			if (fraction != null) return fraction.Denominator;

			var power = u as Power;
			if (power != null &&
			    (power.Exponent is Integer || power.Exponent is Fraction) &&
			    power.Exponent < 0)
			{
				return (u ^ -1).Simplify();
			}

			return 1;
		}

		internal static bool SetEqual<T>(this IEnumerable<T> a, IEnumerable<T> b)
		{
			if (ReferenceEquals(null, a)) throw new ArgumentNullException(nameof(a));
			if (ReferenceEquals(null, b)) throw new ArgumentNullException(nameof(b));
			if (ReferenceEquals(a, b)) return true;

			var list_a = a.ToList();
			var list_b = b.ToList();

			return list_a.Count == list_b.Count &&
			       list_a.OrderBy(item => item.GetHashCode()).SequenceEqual(list_b.OrderBy(item => item.GetHashCode()));
		}
	}
}
