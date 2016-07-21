using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	internal class Quotient : MathObject, IEquatable<Quotient>, IMultiplicativeOperation
	{
		private readonly List<MathObject> _elements;

		public IReadOnlyList<MathObject> Elements => _elements;

		public Quotient(params MathObject[] ls)
			: this((IEnumerable<MathObject>) ls) { }
		public Quotient(IEnumerable<MathObject> ls)
		{
			_elements = ls.ToList();
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Elements.GetCollectionHashCode() * 397 + typeof(Quotient).GetHashCode();
			}
		}

		public override bool Equals(object obj) => Equals(obj as Quotient);

		public bool Equals(Quotient obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			// 1/x, 1/(x*y*z), 1/x/y/z
			// The above never happens with just a Quotient object.  It's always an expression.
			// The first element is always a numerator.
			// x/(y*z), x/y/z
			return Elements[0] == obj.Elements[0] && Elements.Skip(1).SetEqual(obj.Elements.Skip(1));
		}

		public override MathObject Simplify()
		{
			// rely on Product.Simplify() logic
			return Expand().Simplify();
		}

		private static MathObject Invert(MathObject u)
		{
			var iu = u as Integer;
			if (iu != null) return new Fraction(1, iu.Value);

			var du = u as DoubleFloat;
			if (du != null) return 1.0/du.Value;

			var fu = u as Fraction;
			if (fu != null) return fu.Denominator * fu.Numerator;

			return u ^ -1;
		}

		internal override MathObject Expand()
		{
			return Elements[0] * new Product(Elements.Skip(1).Select(elt => Invert(elt.Expand())));
		}

		public override string ToString()
		{
			var result = string.Join(" / ", Elements.Select((elt, i) =>
				{
					var additive = elt as IAdditiveOperation;
					return (additive != null && additive.Elements.Count != 1 ||
					        elt is IMultiplicativeOperation && i != 0) &&
					       !(elt is Function)
						       ? $"({elt})"
						       : $"{elt}";
				}));

			return result;
		}
	}
}