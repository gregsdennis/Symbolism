using System;
using Symbolism;

namespace Tests
{
	public static class Extensions
	{
		public static MathObject AssertEqTo(this MathObject a, MathObject b)
		{
			if (!(a == b)) Console.WriteLine((a == b).ToString());

			return a;
		}

		public static MathObject AssertEqToDouble(this MathObject a, MathObject b, double tolerance = 0.000001)
		{
			var mathObject = a as Equation;
			if (mathObject != null)
			{
				var doubleFloat = mathObject.b as DoubleFloat;
				var equation = b as Equation;
				if (equation != null)
				{
					var f = equation.b as DoubleFloat;
					if (f != null && doubleFloat != null && Math.Abs(doubleFloat.Value - f.Value) > tolerance)
						Console.WriteLine($"{a} and {b} are not equal");
				}
			}

			return a;
		}

		public static MathObject DispLong(this MathObject obj, int indent = 0, bool comma = false)
		{
			if (obj is Or || obj is And)
			{
				var function = (Function) obj;

				Console.WriteLine(new string(' ', indent) + function.Name + "(");
								
				var i = 0;

				foreach (var elt in function.Parameters)
				{
					if (i < function.Parameters.Count - 1)
						elt.DispLong(indent + 2, comma: true);
					else
						elt.DispLong(indent + 2);

					i++;
				}

				Console.WriteLine(new string(' ', indent) + ")" + (comma ? "," : ""));
			}

			else Console.WriteLine(new string(' ', indent) + obj + (comma ? "," : ""));
			
			return obj;
		}

		public static MathObject MultiplyBothSidesBy(this MathObject obj, MathObject item)
		{
			//if (obj is Equation)
			//    return (obj as Equation).a * item == (obj as Equation).b * item;

			var equation = obj as Equation;
			if (equation != null)
				return new Equation(
					equation.a * item,
					equation.b * item,
					equation.Operator);

			var and = obj as And;
			if (and != null) return and.Map(elt => elt.MultiplyBothSidesBy(item));

			throw new Exception();
		}

		public static MathObject AddToBothSides(this MathObject obj, MathObject item)
		{
			var equation = obj as Equation;
			if (equation != null)
				return equation.a + item == equation.b + item;

			throw new Exception();
		}


	}
}