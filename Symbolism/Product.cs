using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public class Product : MathObject, IEquatable<Product>
	{
		public IReadOnlyList<MathObject> Elements { get; }

		public Product(params MathObject[] ls)
			: this((IEnumerable<MathObject>) ls) {}
		public Product(IEnumerable<MathObject> ls)
		{
			Elements = ls.ToList();
		}

		public override string FullForm() =>
			string.Join(" * ", Elements.Select(elt => elt.Precedence < Precedence ? $"({elt})" : $"{elt}"));

		public override string StandardForm()
		{
			var expr_b = Denominator();

			if (expr_b == 1)
			{
				var coefficient = this.Coefficient();
				if (coefficient < 0 && this/coefficient is Sum) return $"-({this*-1})";

				if (coefficient < 0) return $"-{this*-1}";

				return string.Join(" * ",
				                   // ReSharper disable once TryCastAlwaysSucceeds
				                   // NOTE: elt is less likely to be a power, so we allow the precedence to try to handle it without casting first
				                   Elements.Select(elt => elt.Precedence < Precedence || (elt is Power && (elt as Power).Exponent != new Integer(1)/2)
					                                          ? $"({elt})"
					                                          : $"{elt}"));
			}

			var expr_a = Numerator();

			var expr_a_ = expr_a is Sum || (expr_a is Power && (expr_a as Power).Exponent != new Integer(1)/2) ? $"({expr_a})" : $"{expr_a}";

			var expr_b_ = expr_b is Sum || expr_b is Product || (expr_b is Power && (expr_b as Power).Exponent != new Integer(1)/2) ? $"({expr_b})" : $"{expr_b}";

			return $"{expr_a_} / {expr_b_}";
		}

		public override int GetHashCode() => Elements.GetHashCode();

		public override bool Equals(object obj) => Equals(obj as Product);

		public bool Equals(Product obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			// TODO: does this need to require sequence equality or just set equality?
			return Elements.SequenceEqual(obj.Elements);
		}

		private static IReadOnlyList<MathObject> MergeProducts(IReadOnlyList<MathObject> pElts, IReadOnlyList<MathObject> qElts)
		{
			if (pElts.Count == 0) return qElts;
			if (qElts.Count == 0) return pElts;

			var p = pElts[0];
			var ps = pElts.Skip(1).ToList();

			var q = qElts[0];
			var qs = qElts.Skip(1).ToList();

			var res = RecursiveSimplify(new List<MathObject> {p, q});

			if (res.Count == 0) return MergeProducts(ps, qs);

			if (res.Count == 1) return MergeProducts(ps, qs).Cons(res[0]);

			if (res.SequenceEqual(new [] {p, q})) return MergeProducts(ps, qElts).Cons(p);

			if (res.SequenceEqual(new [] {q, p})) return MergeProducts(pElts, qs).Cons(q);

			throw new Exception();
		}

		private static List<MathObject> SimplifyDoubleNumberProduct(DoubleFloat a, Number b)
		{
			double val = 0.0;

			var d = b as DoubleFloat;
			if (d != null) val = a.Value * d.Value;

			var i = b as Integer;
			if (i != null) val = a.Value * i.Value;

			var f = b as Fraction;
			if (f != null) val = a.Value * f.ToDouble().Value;

			if (val == 1.0) return new List<MathObject>();

			return new List<MathObject> {new DoubleFloat(val)};
		}

		private static MathObject Base(MathObject u)
		{
			var power = u as Power;
			return power != null ? power.Base : u;
		}

		private static MathObject Exponent(MathObject u)
		{
			var power = u as Power;
			return power != null ? power.Exponent : 1;
		}

		private static IReadOnlyList<MathObject> RecursiveSimplify(IReadOnlyList<MathObject> elts)
		{
			Product prod0 = elts[0] as Product, prod1 = elts[1] as Product;
			DoubleFloat df0 = elts[0] as DoubleFloat, df1 = elts[1] as DoubleFloat;
			Number n0 = elts[0] as Number, n1 = elts[1] as Number;
			Integer i0 = elts[0] as Integer, i1 = elts[1] as Integer;
			Fraction f0 = elts[0] as Fraction, f1 = elts[1] as Fraction;

			if (elts.Count == 2)
			{
				if (prod0 != null && prod1 != null)
					return MergeProducts(prod0.Elements, prod1.Elements);

				if (prod0 != null) return MergeProducts(prod0.Elements, new List<MathObject> { elts[1]});

				if (prod1 != null) return MergeProducts(new List<MathObject> {elts[0]}, prod1.Elements);

				//////////////////////////////////////////////////////////////////////

				if (df0 != null && n1 != null)
					return SimplifyDoubleNumberProduct(df0, n1);

				if (n0 != null && df1 != null)
					return SimplifyDoubleNumberProduct(df1, n0);

				//////////////////////////////////////////////////////////////////////

				if ((i0 != null || f0 != null) &&
					(i1 != null || f1 != null))
				{
					var P = Rational.SimplifyRNE(new Product(elts[0], elts[1]));

					if (P == 1) return new List<MathObject>();

					return new List<MathObject> {P};
				}

				if (elts[0] == 1) return new List<MathObject> {elts[1]};
				if (elts[1] == 1) return new List<MathObject> {elts[0]};

				var p = elts[0];
				var q = elts[1];

				if (Base(p) == Base(q))
				{
					var res = Base(p) ^ (Exponent(p) + Exponent(q));

					if (res == 1) return new List<MathObject>();

					return new List<MathObject> {res};
				}

				if (q.ComesBefore(p)) return new List<MathObject> {q, p};

				return new List<MathObject> {p, q};
			}

			if (prod0 != null)
				return MergeProducts(prod0.Elements,
									 RecursiveSimplify(elts.Skip(1).ToList()));

			return MergeProducts(new List<MathObject> {elts[0]},
								 RecursiveSimplify(elts.Skip(1).ToList()));
		}

		public MathObject Simplify()
		{
			if (Elements.Count == 1) return Elements[0];

			if (Elements.Any(elt => elt == 0)) return 0;

			var res = RecursiveSimplify(Elements);

			if (!res.Any()) return 1;

			if (res.Count == 1) return res[0];

			// Without the below, the following throws an exception:
			// sqrt(a * b) * (sqrt(a * b) / a) / c

			if (res.OfType<Product>().Any()) return new Product(res).Simplify();

			return new Product(res);
		}

		public override MathObject Numerator() => 
			new Product(Elements.Select(elt => elt.Numerator()).ToList()).Simplify();

		public override MathObject Denominator() =>
			new Product(Elements.Select(elt => elt.Denominator()).ToList()).Simplify();
	}
}