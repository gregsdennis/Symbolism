using System;
using System.Linq;

namespace Symbolism
{
	public static partial class Extensions
	{
		private static bool Check<T>(MathObject obj, Func<T, bool> func)
			where T : MathObject
		{
			var t = obj as T;
			return t != null && func(t);
		}

		public static bool Has(this MathObject obj, MathObject a)
		{
			if (obj == a) return true;

			if (obj is Equation) return Check<Equation>(obj, e => e.a.Has(a) || e.b.Has(a));
			if (obj is Power) return Check<Power>(obj, e => e.Base.Has(a) || e.Exponent.Has(a));
			if (obj is Product) return Check<Product>(obj, e => e.Elements.Any(elt => elt.Has(a)));
			if (obj is Sum) return Check<Sum>(obj, e => e.Elements.Any(elt => elt.Has(a)));
			if (obj is Function) return Check<Function>(obj, e => e.Parameters.Any(elt => elt.Has(a)));

			return false;
		}

		public static bool Has(this MathObject obj, Func<MathObject, bool> proc)
		{
			if (proc(obj)) return true;

			if (obj is Equation) return Check<Equation>(obj, e => e.a.Has(proc) || e.b.Has(proc));
			if (obj is Power) return Check<Power>(obj, e => e.Base.Has(proc) || e.Exponent.Has(proc));
			if (obj is Product) return Check<Product>(obj, e => e.Elements.Any(elt => elt.Has(proc)));
			if (obj is Sum) return Check<Sum>(obj, e => e.Elements.Any(elt => elt.Has(proc)));
			if (obj is Function) return Check<Function>(obj, e => e.Parameters.Any(elt => elt.Has(proc)));

			return false;
		}

		public static bool FreeOf(this MathObject obj, MathObject a) => !obj.Has(a);
	}
}
