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
			if (
				Math.Abs(
				         ((a as Equation).b as DoubleFloat).Value
				         -
				         ((b as Equation).b as DoubleFloat).Value)
				> tolerance)
			{
				Console.WriteLine("{0} and {1} are not equal", a, b);
			}

			return a;
		}

		public static MathObject DispLong(this MathObject obj, int indent = 0, bool comma = false)
		{
			if (obj is Or || obj is And)
			{
				Console.WriteLine(new String(' ', indent) + (obj as Function).Name + "(");
                                
				var i = 0;

				foreach (var elt in (obj as Function).Parameters)
				{
					if (i < (obj as Function).Parameters.Count - 1)
						elt.DispLong(indent + 2, comma: true);
					else
						elt.DispLong(indent + 2);

					i++;
				}

				Console.WriteLine(new String(' ', indent) + ")" + (comma ? "," : ""));
			}

			else Console.WriteLine(new String(' ', indent) + obj + (comma ? "," : ""));
            
			return obj;
		}

		public static MathObject MultiplyBothSidesBy(this MathObject obj, MathObject item)
		{
			//if (obj is Equation)
			//    return (obj as Equation).a * item == (obj as Equation).b * item;
            
			if (obj is Equation)
				return new Equation(
					(obj as Equation).a * item,
					(obj as Equation).b * item,
					(obj as Equation).Operator);
            
			if (obj is And) return (obj as And).Map(elt => elt.MultiplyBothSidesBy(item));

			throw new Exception();
		}

		public static MathObject AddToBothSides(this MathObject obj, MathObject item)
		{
			if (obj is Equation)
				return (obj as Equation).a + item == (obj as Equation).b + item;

			throw new Exception();
		}


	}
}