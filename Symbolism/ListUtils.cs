using System.Collections.Generic;

namespace Symbolism
{
	public static class ListUtils
	{
		public static IReadOnlyList<MathObject> Cons(this IReadOnlyList<MathObject> obj, MathObject elt)
		{
			var res = new List<MathObject>(obj);
			res.Insert(0, elt);
			return res;
		}
	}
}