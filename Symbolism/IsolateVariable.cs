using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject IsolateVariableEq(this Equation eq, Symbol sym)
		{
			if (eq.Operator == Equation.Operators.NotEqual) return eq;

			if (eq.FreeOf(sym)) return eq;

			// sin(x) / cos(x) == y   ->   tan(x) == y

			// TODO: break out into separate methods.
			var a_prod = eq.a as Product;
			var b_prod = eq.b as Product;
			if (a_prod != null)
			{
				if (a_prod.Elements.Any(elt => elt == new Sin(sym)) && a_prod.Elements.Any(elt => elt == 1/new Cos(sym)))
					return (eq.a/new Sin(sym)*new Cos(sym)*new Tan(sym) == eq.b).IsolateVariableEq(sym);

				// A sin(x)^2 == B sin(x) cos(x)   ->   A sin(x)^2 / (B sin(x) cos(x)) == 1

				Func<MathObject, bool> check = elt =>
					{
						var power = elt as Power;
						return (elt == new Sin(sym)) ||
						       (power != null && power.Base == new Sin(sym) && power.Exponent is Number);
					};

				if (a_prod.Elements.Any(check) &&
				    b_prod != null &&
				    b_prod.Elements.Any(check))
					return (eq.a/eq.b == 1).IsolateVariableEq(sym);
			}

			if (eq.b.Has(sym)) return IsolateVariableEq(new Equation(eq.a - eq.b, 0), sym);

			if (eq.a == sym) return eq;

			// (a x^2 + c) / x == - b

			if (a_prod != null &&
			    a_prod.Elements.Any(elt =>
				    {
					    var power = elt as Power;
					    return power != null &&
					           power.Base == sym &&
					           power.Exponent == -1;
				    }))
				return IsolateVariableEq(eq.a*sym == eq.b*sym, sym);

			//if (eq.a is Product &&
			//    (eq.a as Product).elts.Any(
			//        elt =>
			//            elt is Power &&
			//            (elt as Power).bas == sym &&
			//            (elt as Power).exp is Integer &&
			//            ((elt as Power).exp as Integer).val < 0))
			//    return IsolateVariableEq(eq.a * sym == eq.b * sym, sym);


			// if (eq.a.Denominator() is Product &&
			//     (eq.a.Denominator() as Product).Any(elt => elt.Base() == sym)
			// 
			// 


			// (x + y)^(1/2) == z
			//
			// x == -y + z^2   &&   z >= 0

			var a_pow = eq.a as Power;
			if (a_pow != null)
			{
				if (a_pow.Exponent == new Fraction(1, 2))
					return ((eq.a ^ 2) == (eq.b ^ 2)).IsolateVariableEq(sym);

				// 1 / sqrt(x) == y

				if (a_pow.Exponent == -new Fraction(1, 2))
					return (eq.a/eq.a == eq.b/eq.a).IsolateVariable(sym);
			}

			// x ^ 2 == y
			// x ^ 2 - y == 0

			if (eq.a.AlgebraicExpand().DegreeGpe(new List<MathObject> {sym}) == 2 && eq.b != 0)
				return (eq.a - eq.b == 0).IsolateVariable(sym);

			// a x^2 + b x + c == 0

			if (eq.a.AlgebraicExpand().DegreeGpe(new List<MathObject>() {sym}) == 2)
			{
				var a = eq.a.AlgebraicExpand().CoefficientGpe(sym, 2);
				var b = eq.a.AlgebraicExpand().CoefficientGpe(sym, 1);
				var c = eq.a.AlgebraicExpand().CoefficientGpe(sym, 0);

				if (a == null || b == null || c == null) return eq;

				return new Or(

					new And(
						sym == (-b + (((b ^ 2) - 4*a*c) ^ (new Fraction(1,2))))/(2*a),
						(a != 0).Simplify()
						).Simplify(),

					new And(
						sym == (-b - (((b ^ 2) - 4*a*c) ^ (new Fraction(1,2))))/(2*a),
						(a != 0).Simplify()
						).Simplify(),

					new And(sym == -c/b, a == 0, (b != 0).Simplify()).Simplify(),

					new And(
						(a == 0).Simplify(),
						(b == 0).Simplify(),
						(c == 0).Simplify()
						).Simplify()

					).Simplify();
			}


			// (x + y == z).IsolateVariable(x)

			var a_sum = eq.a as Sum;
			if (a_sum != null)
			{
				if (a_sum.Elements.Any(elt => elt.FreeOf(sym)))
				{
					var items = a_sum.Elements.Where(elt => elt.FreeOf(sym));

					//return IsolateVariable(
					//    new Equation(
					//        eq.a - new Sum() { elts = items }.Simplify(),
					//        eq.b - new Sum() { elts = items }.Simplify()),
					//    sym);


					//var new_a = eq.a; items.ForEach(elt => new_a = new_a - elt);
					//var new_b = eq.b; items.ForEach(elt => new_b = new_b - elt);

					var new_a = new Sum(a_sum.Elements.Where(elt => items.Contains(elt) == false).ToList()).Simplify();
					var new_b = eq.b;
					foreach (var elt in items) new_b = new_b - elt;

					// (new_a as Sum).Where(elt => items.Contains(elt) == false)

					return IsolateVariableEq(new Equation(new_a, new_b), sym);

					//return IsolateVariable(
					//    new Equation(
					//        eq.a + new Sum() { elts = items.ConvertAll(elt => elt * -1) }.Simplify(),
					//        eq.b - new Sum() { elts = items }.Simplify()),
					//    sym);
				}

				// a b + a c == d

				// a + a c == d

				if (a_sum.Elements.All(elt => elt.DegreeGpe(new List<MathObject> {sym}) == 1))
				{
					//return 
					//    (new Sum() { elts = (eq.a as Sum).elts.Select(elt => elt / sym).ToList() }.Simplify() == eq.b / sym)
					//    .IsolateVariable(sym);

					return (sym*new Sum(a_sum.Elements.Select(elt => elt/sym).ToList()).Simplify() == eq.b)
						.IsolateVariable(sym);
				}

				// -sqrt(x) + z * x == y

				if (eq.a.Has(sym ^ (new Fraction(1,2)))) return eq;

				// sqrt(a + x) - z * x == -y

				if (eq.a.Has(elt =>
					{
						var power = elt as Power;
						return power != null && power.Exponent == new Fraction(1, 2) && power.Base.Has(sym);
					}))
					return eq;

				if (eq.AlgebraicExpand().Equals(eq)) return eq;

				return eq.AlgebraicExpand().IsolateVariable(sym);
			}

			// (x + 1) / (x + 2) == 3

			if (eq.a.Numerator().Has(sym) && eq.a.Denominator().Has(sym))
			{
				return IsolateVariableEq(eq.a*eq.a.Denominator() == eq.b*eq.a.Denominator(), sym);
			}

			// sqrt(2 + x) * sqrt(3 + x) == y

			if (a_prod != null)
			{
				if (a_prod.Elements.All(elt => elt.Has(sym))) return eq;

				var items = ((Product) eq.a).Elements.Where(elt => elt.FreeOf(sym)).ToList();

				return IsolateVariableEq(new Equation(eq.a/new Product(items).Simplify(),
				                                      eq.b/new Product(items).Simplify()),
				                         sym);
			}

			// x ^ -2 == y

			if (a_pow != null)
			{
				var exp_int = a_pow.Exponent as Integer;
				if (a_pow.Base == sym && exp_int != null && exp_int.Value < 0)
					return (eq.a/eq.a == eq.b/eq.a).IsolateVariableEq(sym);

				return eq;
			}

			// sin(x) == y

			// Or(x == asin(y), x  == Pi - asin(y))

			var a_sin = eq.a as Sin;
			if (a_sin != null)
				return new Or(a_sin.Parameters[0] == new Asin(eq.b),
				              a_sin.Parameters[0] == new Symbol("Pi") - new Asin(eq.b)).IsolateVariable(sym);

			// tan(x) == y

			// x == atan(t)

			var a_tan = eq.a as Tan;
			if (a_tan != null)
				return (a_tan.Parameters[0] == new Atan(eq.b)).IsolateVariable(sym);

			// asin(x) == y
			//
			// x == sin(y)

			var a_asin = eq.a as Asin;
			if (a_asin != null)
				return (a_asin.Parameters[0] == new Sin(eq.b)).IsolateVariable(sym);

			throw new Exception();
		}

		public static MathObject IsolateVariable(this MathObject obj, Symbol sym)
		{
			var or = obj as Or;
			if (or != null) return new Or (or.Parameters.Select(elt => elt.IsolateVariable(sym))).Simplify();

			var and = obj as And;
			if (and != null) return new And (and.Parameters.Select(elt => elt.IsolateVariable(sym))).Simplify();

			var equation = obj as Equation;
			if (equation != null) return equation.IsolateVariableEq(sym);

			throw new Exception();
		}
	}
}
