using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	internal static class TermSequencer
	{
		private static bool O3(IReadOnlyList<MathObject> uElts, IReadOnlyList<MathObject> vElts)
		{
			if (!uElts.Any()) return true;
			if (!vElts.Any()) return false;

			var u = uElts[0];
			var v = vElts[0];

			return u == v
				       ? O3(uElts.Skip(1).ToList(), vElts.Skip(1).ToList())
				       : ComesBefore(u, v);
		}

		public static bool ComesBefore(this MathObject u, MathObject v)
		{
			if (u is Number && !(v is Number)) return true;

			DoubleFloat ud = u as DoubleFloat, vd = v as DoubleFloat;
			Integer ui = u as Integer, vi = v as Integer;
			Fraction uf = u as Fraction, vf = v as Fraction;

			if (ud != null)
			{
				if (vd != null)
					return ud.Value < vd.Value;
				if (vi != null)
					return ud.Value < vi.Value;
				if (vf != null)
					return ud.Value * vf.Denominator.Value < vf.Numerator.Value;
			}

			if (ui != null)
			{
				if (vd != null)
					return ui.Value < vd.Value;
				if (vi != null)
					return ui.Value < vi.Value;
				if (vf != null)
					return ui.Value * vf.Denominator.Value < vf.Numerator.Value;
			}

			if (uf != null)
			{
				if (vd != null)
					return uf.Numerator.Value < vd.Value * uf.Denominator.Value;
				if (vi != null)
					return uf.Numerator.Value < vi.Value * uf.Denominator.Value;
				if (vf != null)
					return uf.Numerator.Value * vf.Denominator.Value < vf.Numerator.Value * uf.Denominator.Value;
			}

			Symbol uSym = u as Symbol, vSym = v as Symbol;
			if (uSym != null && vSym != null)
				return string.CompareOrdinal(uSym.Name, vSym.Name) < 0;

			Product uProd = u as Product, vProd = v as Product;
			if (uProd != null && vProd != null)
			{
				var a = new List<MathObject>(uProd.Elements);
				a.Reverse();

				var b = new List<MathObject>(vProd.Elements);
				b.Reverse();

				return O3(a, b);
			}

			IAdditiveOperation uSum = u as IAdditiveOperation, vSum = v as IAdditiveOperation;
			if (uSum != null && vSum != null)
				return O3(uSum.Elements.Reverse().ToList(),
						  vSum.Elements.Reverse().ToList());

			Power uPow = u as Power, vPow = v as Power;
			if (uPow != null && vPow != null)
				return uPow.Base == vPow.Base
					       ? ComesBefore(uPow.Exponent, vPow.Exponent)
					       : ComesBefore(uPow.Base, vPow.Base);

			Function uFunc = u as Function, vFunc = v as Function;
			if (uFunc != null && vFunc != null)
				return uFunc.Name == vFunc.Name
					       ? O3(uFunc.Parameters, vFunc.Parameters)
					       : string.CompareOrdinal(uFunc.Name, vFunc.Name) < 0;

			if (uProd != null &&
			    (v is Power || vSum != null || vFunc != null || vSym != null))
				return ComesBefore(u, new Product(v));

			if (uPow != null && (vSum != null || vFunc != null || vSym != null))
				// do not simplify this; it dictates logic flow.
				return ComesBefore(uPow, new Power(v, 1));

			if (uSum != null && (vFunc != null || vSym != null))
				return ComesBefore(u, new Sum(v));

			if (uFunc != null && vSym != null)
				return string.CompareOrdinal(uFunc.Name, vSym.Name) < 0;

			return !ComesBefore(v, u);
		}
	}
}