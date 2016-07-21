using System;
using System.Collections.Generic;
using System.Linq;

using static Symbolism.Constants;

namespace Symbolism
{
	internal class Sum : MathObject, IAdditiveOperation
	{
		private readonly List<MathObject> _elements;

		public IReadOnlyList<MathObject> Elements => _elements;

		public Sum(params MathObject[] ls)
			: this((IEnumerable<MathObject>) ls) {}
		public Sum(IEnumerable<MathObject> ls)
		{
			_elements = GetAllTerms(ls).ToList();
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Elements.GetCollectionHashCode() * 397 + typeof(Sum).GetHashCode();
			}
		}

		public override bool Equals(object obj) => Equals(obj as Sum);

		public bool Equals(Sum obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return Elements.SetEqual(obj.Elements);
		}

		// guaranteed:  all numbers are definable.  i.e. no fractions with 0 denominator
		private static Number CombineTwoNumbers(Number p, Number q)
		{
			if (p == 0) return q;
			if (q == 0) return p;

			DoubleFloat dp = p as DoubleFloat, dq = q as DoubleFloat;

			if (dp != null || dq != null)
				return new DoubleFloat(p.ToDouble().Value + q.ToDouble().Value);

			return Rational.SimplifyRNE(new Sum(p, q)) as Number;
		}

		private static MathObject CombineNumbers(IReadOnlyList<Number> numbers)
		{
			if (numbers.OfType<Fraction>().Any(f => f.Denominator == 0)) return undef;

			return numbers.Aggregate<Number, Number>(new Integer(0), CombineTwoNumbers);
		}

		public override MathObject Simplify()
		{
			if (Elements.Count == 1) return Elements[0].Simplify();

			var expanded = Expand();
			var sExpanded = expanded as Sum;

			if (sExpanded == null) return expanded.Simplify();

			if (sExpanded.Elements.Count == 1) return sExpanded.Elements[0].Simplify();

			var numbers = sExpanded.Elements.OfType<Number>().ToList();
			if (numbers.Count != 0)
			{
				var constant = CombineNumbers(numbers);
				if (constant == undef) return undef;

				// .Except() applies .Distinct(); we want duplicates, if any.
				var rest = sExpanded.Elements.Where(elt => !(elt is Number)).ToList();

				if (rest.Count == 0) return constant;

				rest.Insert(0, constant);

				sExpanded = new Sum(rest);
			}

			var previous = sExpanded.Elements;
			List<MathObject> combined;
			do
			{
				combined = previous.GroupBy(elt => elt.Term())
				                   .Select(g => (new Sum(g.Select(elt => elt.Coefficient())).Simplify()*g.Key).Simplify())
				                   .Where(elt => elt.Coefficient() != 0)
				                   .ToList();
				previous = GetAllTerms(combined).ToList();
			} while (!combined.SetEqual(previous));
			var sum = combined.Where(elt => elt.Coefficient() > 0).ToList();
			var difference = combined.Except(sum)
			                         .Select(elt => (-1*elt).Simplify())
									 .ToList();

			if (!sum.Any() && !difference.Any()) return 0;
			if (!sum.Any()) return new Difference(difference);
			if (!difference.Any())
			{
				if (sum.Count == 1) return sum[0];

				return new Sum(sum);
			}

			if (sum.Count == 1)
			{
				difference.Insert(0, sum[0]);

				return new Difference(difference);
			}

			sum.Add(new Difference(difference));

			return new Sum(sum);
		}

		private static IEnumerable<MathObject> GetAllTerms(IEnumerable<MathObject> elts, bool expand = false)
		{
			var elements = new List<MathObject>();

			foreach (var elt in elts)
			{
				var sum = (expand ? elt.Expand() : elt) as Sum;
				if (sum != null)
					elements.AddRange(GetAllTerms(sum.Elements.Select(e => e.Expand())));
				else
					elements.Add(elt);
			}

			return elements;
		}

		internal override MathObject Expand()
		{
			var res = GetAllTerms(Elements, true).ToList();

			if (res.Count == 0) return 0;
			if (res.Count == 1) return res[0];

			return new Sum(res);
		}

		public override string ToString()
		{
			var result = string.Join(" + ", Elements.Select((elt, i) => elt is IAdditiveOperation && i != 0 &&
			                                                            !(elt is Function)
				                                                            ? $"({elt})"
				                                                            : $"{elt}"));
			
			return result;
		}
	}
}