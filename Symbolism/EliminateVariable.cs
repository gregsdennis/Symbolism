using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		private static MathObject CheckVariableEqLs(this List<Equation> eqs, Symbol sym)
		{
			// (a == 10, a == 0)   ->   10 == 0   ->   false

			if (eqs.EliminateVariableEqLs(sym) == false) return false;

			// (1/a != 0  &&  a != 0)   ->   a != 0

			if (eqs.Any(eq => eq.Operator == Equation.Operators.NotEqual && eq.a == sym && eq.b == 0)&&
				eqs.Any(eq => eq.Operator == Equation.Operators.NotEqual && eq.a == 1/sym && eq.b == 0))
				return eqs
					.Where(eq => (eq.Operator == Equation.Operators.NotEqual && eq.a == 1/sym && eq.b == 0) == false)
					.ToList()
					.CheckVariableEqLs(sym);

			// x + y == z && x / y == 0 && x != 0   -> false

			if (eqs.Any(eq => eq.Operator == Equation.Operators.Equal && eq.a.Numerator() == sym && eq.a.Denominator().FreeOf(sym) && eq.b == 0) &&
			    eqs.Any(eq => eq == (sym != 0)))
				return false;

			return new And(eqs.Select(eq => eq as MathObject));
		}

		public static MathObject CheckVariable(this MathObject expr, Symbol sym)
		{
			// 1 / x == 0
			// 1 / x^2 == 0

			var equation = expr as Equation;
			if (equation != null)
			{
				var simplified = equation.SimplifyEquation() as Equation;
				if (simplified != null)
				{
					var power = simplified.a as Power;
					if (power != null)
					{
						var integer = power.Exponent as Integer;
						if (equation.Operator == Equation.Operators.Equal &&
						    equation.b == 0 &&
						    equation.a.Has(sym) &&
						    integer != null &&
						    integer.Value < 0)
							return false;
					}
				}
			}

			var and = expr as And;
			if (and != null)
			{
				var result = and.Map(elt => elt.CheckVariable(sym));

				if (result is And)
				{
					// TODO: Check if OfType<T>() or Cast<T>() would work better here.
					var eqs = and.Parameters.Select(elt => elt as Equation).ToList();

					return eqs.CheckVariableEqLs(sym);
				}

				return result;
			}


			var or = expr as Or;
			if (or != null &&
			    or.Parameters.All(elt => elt is And))
				return or.Map(elt => elt.CheckVariable(sym));

			return expr;
		}



		// EliminateVarAnd
		// EliminateVarOr
		// EliminateVarLs
		// EliminateVar

		// EliminateVars

		private static MathObject EliminateVariableEqLs(this List<Equation> eqs, Symbol sym)
		{
			if (eqs.Any(elt =>
			            elt.Operator == Equation.Operators.Equal &&
			            elt.Has(sym) &&
			            elt.AlgebraicExpand().Has(sym) &&
			            elt.IsolateVariableEq(sym).Has(obj =>
				            {
					            var equation = obj as Equation;
					            return equation != null && equation.a == sym && equation.b.FreeOf(sym);
				            })) == false)
				return new And(eqs.Select(elt => elt as MathObject));

			var eq = eqs.First(elt =>
			                   elt.Operator == Equation.Operators.Equal &&
			                   elt.Has(sym) &&
			                   elt.AlgebraicExpand().Has(sym) &&
			                   elt.IsolateVariableEq(sym).Has(obj =>
				                   {
					                   var equation = obj as Equation;
					                   return equation != null && equation.a == sym && equation.b.FreeOf(sym);
				                   }));

			var rest = eqs.Except(new List<Equation> {eq}).ToList();

			var result = eq.IsolateVariableEq(sym);

			// sym was not isolated

			var res_eq = result as Equation;
			if (res_eq != null)
			{
				if (res_eq.a != sym || res_eq.b.Has(sym))
					return new And(eqs.Select(elt => elt as MathObject));

				return new And(rest.Select(elt => elt.Substitute(sym, res_eq.b)));

				// return new And() { args = rest.Select(rest_eq => rest_eq.SubstituteEq(eq_sym)).ToList() };

				// rest.Map(rest_eq => rest_eq.Substitute(eq_sym)
			}

			// Or(
			//     And(eq0, eq1, eq2, ...)
			//     And(eq3, eq4, eq5, ...)
			// )

			var or = result as Or;
			if (or != null && or.Parameters.All(elt => elt is And))
			{
				var parameters = new List<MathObject>();
				foreach (var elt in or.Parameters.Cast<And>())
					parameters.Add(new And(new List<MathObject>(elt.Parameters).Concat(rest)));

				return new Or(parameters.Select(elt => EliminateVariable(elt, sym)));
			}

			if (or != null)
			{
				var parameters = new List<MathObject>();

				foreach (Equation eq_sym in or.Parameters)
					parameters.Add(new And(rest.Select(rest_eq => rest_eq.Substitute(sym, eq_sym.b))));

				return new Or(parameters);

				// (result as Or).Map(eq_sym => new And() { args = rest.Select(rest_eq => rest_eq.SubstituteEq(eq_sym)).ToList() });

				// (result as Or).Map(eq_sym => rest.Map(rest_eq => rest_eq.Substitute(eq_sym))
			}

			throw new Exception();
		}

		public static MathObject EliminateVariable(this MathObject expr, Symbol sym)
		{
			var and = expr as And;
			if (and != null)
			{
				var eqs = and.Parameters.Select(elt => elt as Equation);

				return EliminateVariableEqLs(eqs.ToList(), sym);
			}

			var or = expr as Or;
			if (or != null)
			{
				return new Or(or.Parameters.Select(and_expr => and_expr.EliminateVariable(sym)));

				// expr.Map(and_expr => and_expr.EliminateVar(sym))
			}

			throw new Exception();
		}

		public static MathObject EliminateVariables(this MathObject expr, params Symbol[] syms)
		{
			return syms.Aggregate(expr, (result, sym) => result.EliminateVariable(sym));
		}
	}
}
