using System;
using System.Collections.Generic;
using System.Linq;

using static Symbolism.Constants;

namespace Symbolism
{
	internal class Product : MathObject, IEquatable<Product>, IMultiplicativeOperation
	{
		public IReadOnlyList<MathObject> Elements { get; }

		public Product(params MathObject[] ls)
			: this((IEnumerable<MathObject>) ls) {}
		public Product(IEnumerable<MathObject> ls)
		{
			Elements = GetAllElements(ls).ToList();
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Elements.GetCollectionHashCode()*397 + typeof(Product).GetHashCode();
			}
		}

		public override bool Equals(object obj) => Equals(obj as Product);

		public bool Equals(Product obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return Elements.SetEqual(obj.Elements);
		}

		// guaranteed:  all numbers are definable.  i.e. no fractions with 0 denominator
		private static Number CombineTwoNumbers(Number p, Number q)
		{
			if (p == 1) return q;
			if (q == 1) return p;

			DoubleFloat dp = p as DoubleFloat, dq = q as DoubleFloat;

			if (dp != null || dq != null)
				return new DoubleFloat(p.ToDouble().Value * q.ToDouble().Value);

			return Rational.SimplifyRNE(new Product(p, q)) as Number;
		}

		private static MathObject CombineNumbers(IReadOnlyList<Number> numbers)
		{
			if (numbers.OfType<Fraction>().Any(f => f.Denominator == 0)) return undef;
			if (numbers.Any(n => n == 0)) return 0;

			return numbers.Aggregate<Number, Number>(new Integer(1), CombineTwoNumbers);
		}

		private static MathObject NegateIfNecessary(MathObject u, bool negate)
		{
			return negate ? new Difference(u) : u;
		}

		public override MathObject Simplify()
		{
			if (Elements.Count == 1) return Elements[0].Simplify();

			var expanded = Expand();
			var pExpanded = expanded as Product;

			if (pExpanded == null) return expanded.Simplify();

			if (pExpanded.Elements.Count == 1) return pExpanded.Elements[0].Simplify();

			var numbers = pExpanded.Elements.OfType<Number>().ToList();
			var negate = false;
			if (numbers.Count != 0)
			{
				var constant = CombineNumbers(numbers);
				if (constant == undef) return undef;

				// .Except() applies .Distinct(); we want duplicates, if any.
				var rest = pExpanded.Elements.Where(elt => !(elt is Number)).ToList();

				if (rest.Count == 0) return constant;

				if (constant == 0) return 0;
				if (constant != 1)
				{
					if (constant == -1)
						negate = true;
					else
						rest.Insert(0, constant);
				}

				pExpanded = new Product(rest);
			}

			var previous = pExpanded.Elements;
			List<MathObject> combined;
			do
			{
				combined = previous.Select(elt => elt.Simplify())
								   .GroupBy(elt => elt.Base())
				                   .Select(g => (g.Key ^ new Sum(g.Select(elt => elt.Exponent())).Simplify()).Simplify())
				                   .Where(elt => elt != 1 && elt.Exponent() != 0)
								   .OrderBy(elt => elt.Term(), TermComparer.Instance)
								   .ToList();
				previous = GetAllElements(combined).ToList();
			} while (!combined.SetEqual(previous));

			var product = combined.Where(elt => elt.Exponent() > 0).ToList();
			var quotient = combined.Except(product)
			                       .Select(elt => (elt ^ -1).Simplify())
			                       .ToList();

			// 1
			if (!product.Any() && !quotient.Any()) return negate ? -1 : 1;
			// 1/(x * y)
			if (!product.Any()) return new Quotient(negate ? -1 : 1, new Product(quotient));
			if (!quotient.Any())
			{
				// x
				if (product.Count == 1) return NegateIfNecessary(product[0], negate);
				// x * y
				return NegateIfNecessary(new Product(product), negate);
			}
			// x * y / z
			if (quotient.Count == 1) return new Quotient(NegateIfNecessary(new Product(product), negate), quotient[0]);
			// x * y / (z * t)
			return new Quotient(NegateIfNecessary(new Product(product), negate), new Product(quotient));
		}

		private static IEnumerable<MathObject> GetAllElements(IEnumerable<MathObject> elts, bool expand = false)
		{
			var elements = new List<MathObject>();

			foreach (var elt in elts)
			{
				var product = (expand ? elt.Expand() : elt) as Product;
				if (product != null)
					elements.AddRange(GetAllElements(product.Elements.Select(e => e.Expand())));
				else
					elements.Add(elt);
			}

			return elements;
		}

		internal override MathObject Expand()
		{
			var res = GetAllElements(Elements, true).ToList();

			if (res.Count == 0) return 0;
			if (res.Count == 1) return res[0];

			return new Product(res);
		}

		public override string ToString()
		{
			var result = string.Join(" * ", Elements.Select(elt =>
			{
				var operation = elt as IAdditiveOperation;
				return operation != null && operation.Elements.Count != 1 &&
					   !(elt is Function)
						   ? $"({elt})"
						   : $"{elt}";
			}));

			return result;
		}
	}
}