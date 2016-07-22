using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	internal class TermComparer : IComparer<MathObject>
	{
		public static TermComparer Instance { get; private set; }

		static TermComparer()
		{
			Instance = new TermComparer();
		}
		private TermComparer() {}

		public int Compare(MathObject x, MathObject y)
		{
			return DoCompare(x, y);
		}

		private static int DoCompare(MathObject x, MathObject y)
		{
			return Compare(x as Number, y as Number) ??
			       Compare(x as INamedMathObject, y as INamedMathObject) ??
			       Compare(x as Number) ??
			       Compare(x as Symbol) ??
			       Compare(x as Function) ??
			       Compare(x as IAdditiveOperation, y) ??
			       Compare(x as IMultiplicativeOperation, y) ?? 0;
		}

		private static int? SimpleCompare(MathObject x, MathObject y)
		{
			return Compare(x as Number, y as Number) ??
			       Compare(x as INamedMathObject, y as INamedMathObject) ??
			       Compare(x as Number) ??
			       Compare(x as Symbol) ??
			       Compare(x as Function);
		}

		private static int? Compare<T>(T x)
		{
			if (ReferenceEquals(null, x)) return null;

			return -1;
		}

		private static int? Compare(Number x, Number y)
		{
			if (ReferenceEquals(null, x) || ReferenceEquals(null, y)) return null;

			return x.ToDouble() < y.ToDouble() ? -1 : 1;
		}

		private static int? Compare(INamedMathObject x, INamedMathObject y)
		{
			if (ReferenceEquals(null, x) || ReferenceEquals(null, y)) return null;

			return string.CompareOrdinal(x.Name, y.Name);
		}

		private static int? Compare(IAdditiveOperation x, MathObject y)
		{
			if (ReferenceEquals(null, x)) return null;

			return x.Elements.Select(elt => DoCompare(elt, y)).Min();
		}

		private static int? Compare(IMultiplicativeOperation x, MathObject y)
		{
			if (ReferenceEquals(null, x)) return null;

			return x.Elements.Select(elt => DoCompare(elt, y)).Min();
		}
	}
}