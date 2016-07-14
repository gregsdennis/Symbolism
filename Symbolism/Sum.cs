using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public class Sum : MathObject
	{
		private readonly List<MathObject> _elements;

		public IReadOnlyList<MathObject> Elements => _elements;

		public Sum(params MathObject[] ls)
			: this((IEnumerable<MathObject>) ls) {}
		public Sum(IEnumerable<MathObject> ls)
		{
			_elements = ls.ToList();
		}

		public override int GetHashCode() => Elements.GetHashCode();

		public override bool Equals(object obj) => Equals(obj as Sum);

		public bool Equals(Sum obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			// TODO: does this need to require sequence equality or just set equality?
			return Elements.SequenceEqual(obj.Elements);
		}

		public static MathObject Term(MathObject u)
		{
			var product = u as Product;
			if (product != null && product.Elements[0] is Number)
				return new Product(product.Elements.Skip(1));
			;

			if (product != null) return u;

			return new Product(u);
		}

		private static IReadOnlyList<MathObject> MergeSums(IReadOnlyList<MathObject> pElts, IReadOnlyList<MathObject> qElts)
		{
			if (pElts.Count == 0) return qElts;
			if (qElts.Count == 0) return pElts;

			var p = pElts[0];
			var ps = pElts.Skip(1).ToList();

			var q = qElts[0];
			var qs = qElts.Skip(1).ToList();

			var res = RecursiveSimplify(new List<MathObject> {p, q});

			if (res.Count == 0) return MergeSums(ps, qs);

			if (res.Count == 1) return MergeSums(ps, qs).Cons(res[0]);

			if (res.SequenceEqual(new[] {p, q})) return MergeSums(ps, qElts).Cons(p);

			if (res.SequenceEqual(new[] {q, p})) return MergeSums(pElts, qs).Cons(q);

			throw new Exception();
		}

		private static IReadOnlyList<MathObject> SimplifyDoubleNumberSum(DoubleFloat a, Number b)
		{
			double val = 0.0;

			var d = b as DoubleFloat;
			if (d != null) val = a.Value + d.Value;

			var integer = b as Integer;
			if (integer != null) val = a.Value + integer.Value;

			var fraction = b as Fraction;
			if (fraction != null) val = a.Value + fraction.ToDouble().Value;

			if (val == 0.0) return new List<MathObject>();

			return new List<MathObject> {new DoubleFloat(val)};
		}

		private static IReadOnlyList<MathObject> RecursiveSimplify(IReadOnlyList<MathObject> elts)
		{
			var p = elts[0];
			var q = elts[1];

			Sum sp = p as Sum, sq = q as Sum;

			if (elts.Count == 2)
			{

				if (sp != null && sq != null)
					return MergeSums(sp._elements, sq._elements);

				if (sp != null)
					return MergeSums(sp._elements, new List<MathObject> {q});

				if (sq != null)
					return MergeSums(new List<MathObject> {p}, sq._elements);

				//////////////////////////////////////////////////////////////////////

				DoubleFloat dp = p as DoubleFloat, dq = q as DoubleFloat;
				Number np = p as Number, nq = q as Number;

				if (dp != null && nq != null)
					return SimplifyDoubleNumberSum(dp, nq);

				if (np != null && dq != null)
					return SimplifyDoubleNumberSum(dq, np);

				//////////////////////////////////////////////////////////////////////

				Integer ip = p as Integer, iq = q as Integer;
				Fraction fp = p as Fraction, fq = q as Fraction;

				if ((ip != null || fp != null) &&
				    (iq != null || fq != null))
				{
					var P = Rational.SimplifyRNE(new Sum(p, q));

					if (P == 0) return new List<MathObject>();

					return new List<MathObject> {P};
				}

				if (p == 0) return new List<MathObject> {q};

				if (q == 0) return new List<MathObject> {p};

				var pTerm = Term(p);
				if (pTerm == Term(q))
				{
					var res = pTerm*(p.Const() + q.Const());

					if (res == 0) return new List<MathObject>();

					return new List<MathObject> {res};
				}

				if (q.ComesBefore(p)) return new List<MathObject> {q, p};

				return new List<MathObject> {p, q};
			}

			if (sp != null)
				return MergeSums(sp._elements, RecursiveSimplify(elts.Skip(1).ToList()));

			return MergeSums(new List<MathObject> {p}, RecursiveSimplify(elts.Skip(1).ToList()));
		}

		public MathObject Simplify()
		{
			if (Elements.Count() == 1) return _elements[0];

			var res = RecursiveSimplify(_elements);

			if (res.Count == 0) return 0;
			if (res.Count == 1) return res[0];

			return new Sum(res);
		}

		public override string FullForm() => string.Join(" + ", _elements.ConvertAll(elt => elt.Precedence < Precedence
			                                                                                   ? $"({elt})"
			                                                                                   : $"{elt}"));

		public override string StandardForm()
		{
			var result = string.Join(" ", _elements.ConvertAll(elt =>
				{
					var elt_ = elt.Const() < 0 ? elt*-1 : elt;

					var elt__ = elt.Const() < 0 && elt_ is Sum || (elt is Power && (elt as Power).Exponent != new Fraction(1,2))
						            ? $"({elt_})"
						            : $"{elt_}";

					return elt.Const() < 0 ? $"- {elt__}" : $"+ {elt__}";
				}));
            
			if (result.StartsWith("+ ")) return result.Remove(0, 2); // "+ x + y"   ->   "x + y"

			if (result.StartsWith("- ")) return result.Remove(1, 1); // "- x + y"   ->   "-x + y"

			return result;
		}
	}
}