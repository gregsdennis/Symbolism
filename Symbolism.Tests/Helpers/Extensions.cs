using System;

namespace Symbolism.Tests.Helpers
{
	public static class Extensions
	{
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
