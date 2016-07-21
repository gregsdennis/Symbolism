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
		public static int GetCollectionHashCode<T>(this IEnumerable<T> collection)
		{
			var value = 0;
			unchecked
			{
				foreach (var item in collection)
				{
					value = value*397 + value.GetHashCode();
				}
			}
			return value;
		}
	}
}