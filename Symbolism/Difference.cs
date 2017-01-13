using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	internal class Difference : MathObject, IEquatable<Difference>, IAdditiveOperation
	{
		private readonly List<MathObject> _elements;

		public IReadOnlyList<MathObject> Elements => _elements;

		public Difference(params MathObject[] ls)
			: this((IEnumerable<MathObject>) ls) {}
		public Difference(IEnumerable<MathObject> ls)
		{
			_elements = ls.ToList();
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Elements.GetCollectionHashCode() * 397 + typeof(Difference).GetHashCode();
			}
		}

		public override bool Equals(object obj) => Equals(obj as Difference);

		public bool Equals(Difference obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			// x, -x
			if (Elements.Count == 1) return Elements[0] == obj.Elements[0];
			// -x - y - z
			if (Elements[0].Coefficient() < 0) return Elements.SetEqual(obj.Elements);
			// x - y - z
			return Elements[0] == obj.Elements[0] && Elements.Skip(1).SetEqual(obj.Elements.Skip(1));
		}

		public override MathObject Simplify()
		{
			// x, -x
			if (_elements.Count == 1) return this;

			// rely on Sum.Simplify() logic
			return Expand().Simplify();
		}

		private static MathObject Negate(MathObject u)
		{
			var iu = u as Integer;
			if (iu != null) return -iu.Value;

			var du = u as DoubleFloat;
			if (du != null) return -du.Value;

			var fu = u as Fraction;
			if (fu != null) return new Fraction(-fu.Numerator.Value, fu.Denominator);

			return -1*u;
		}

		internal override MathObject Expand()
		{
			// x, -x
			if (_elements.Count == 1) return -1*_elements[0].Expand();
			// -x - y
			if (Elements[0].Coefficient() < 0)
				return new Sum(Elements.Select(elt => Negate(elt.Expand())));
			// x - y
			return Elements[0] + new Sum(Elements.Skip(1).Select(elt => Negate(elt.Expand())));
		}

		public override string ToString()
		{
			if (Elements.Count == 1) return $"-{Elements[0]}";

			var result = string.Join(" - ", Elements.Select((elt, i) => elt is IAdditiveOperation && i != 0 &&
			                                                            !(elt is Function)
				                                                            ? $"({elt})"
				                                                            : $"{elt}"));

			return result;
		}
	}
}