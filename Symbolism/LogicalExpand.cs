using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		public static MathObject LogicalExpand(this MathObject obj)
		{
			return TryExpandOr(obj as Or) ?? TryExpandAnd(obj as And) ?? obj;
		}

		private static MathObject TryExpandOr(Or or)
		{
			return or?.Map(elt => elt.LogicalExpand());
		}

		private static MathObject TryExpandAnd(And and)
		{
			if (and == null || !(and.Parameters.OfType<Or>().Any() &&
								 and.Parameters.Count > 1))
				return null;

			var before = new List<MathObject>();
			Or or = null;
			var after = new List<MathObject>();

			foreach (var elt in and.Parameters)
			{
				if (elt is Or && or == null) or = elt as Or;
				else if (or == null) before.Add(elt);
				else after.Add(elt);
			}

			return or.Map(or_elt => new And(new And(before).LogicalExpand(),
											or_elt,
											new And(after).LogicalExpand())).LogicalExpand();
		}
	}
}
