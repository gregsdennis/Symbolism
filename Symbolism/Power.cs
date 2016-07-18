using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	public class Power : MathObject, IEquatable<Power>
	{
		public MathObject Base { get; }
		public MathObject Exponent { get; }

		public Power(MathObject a, MathObject b)
		{
			Base = a;
			Exponent = b;
		}

		public override string FullForm() =>
			// ReSharper disable once UseStringInterpolation
			// NOTE: Ignoring for readability.
			string.Format("{0} ^ {1}",
			              Base.Precedence < Precedence ? $"({Base})" : $"{Base}",
			              Exponent.Precedence < Precedence ? $"({Exponent})" : $"{Exponent}");


		public override string StandardForm()
		{
			// x ^ 1/2   ->   sqrt(x)

			if (Exponent == new Fraction(1, 2)) return $"sqrt({Base})";

			// ReSharper disable once UseStringInterpolation
			// NOTE: Ignoring for readability.
			return string.Format("{0} ^ {1}",
			                     Base.Precedence < Precedence ? $"({Base})" : $"{Base}",
			                     Exponent.Precedence < Precedence ? $"({Exponent})" : $"{Exponent}");
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Power);
		}

		public bool Equals(Power obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return Base == obj.Base && Exponent == obj.Exponent;
		}

		public MathObject Simplify()
		{
			DoubleFloat bd = Base as DoubleFloat, ed = Exponent as DoubleFloat;
			Integer bi = Base as Integer, ei = Exponent as Integer;
			Fraction bf = Base as Fraction, ef = Exponent as Fraction;

			if (Base == 0) return 0;
			if (Base == 1) return 1;
			if (Exponent == 0) return 1;
			if (Exponent == 1) return Base;

			// Logic from MPL/Scheme:
			//
			//if (v is Integer && w is Integer)
			//    return
			//        new Integer(
			//            (int)Math.Pow(((Integer)v).val, ((Integer)w).val));

			// C# doesn't have built-in rationals. So:
			// 1 / 3 -> 3 ^ -1 -> 0.333... -> (int)... -> 0

			//if (v is Integer && w is Integer && ((Integer)w).val > 1)
			//    return
			//        new Integer(
			//            (int)Math.Pow(((Integer)v).val, ((Integer)w).val));

			if ((bi != null || bf != null) && ei != null)
				return Rational.SimplifyRNE(new Power(Base, Exponent));

			if (bd != null && ei != null)
				return new DoubleFloat(Math.Pow(bd.Value, ei.Value));

			if (bd != null && ef != null)
				return new DoubleFloat(Math.Pow(bd.Value, ef.ToDouble().Value));

			if (bi != null && ed != null)
				return new DoubleFloat(Math.Pow(bi.Value, ed.Value));

			if (bf != null && ed != null)
				return new DoubleFloat(Math.Pow(bf.ToDouble().Value, ed.Value));

			var bpow = Base as Power;
			if (bpow != null && ei != null)
				return bpow.Base ^ (bpow.Exponent*Exponent);

			var bprod = Base as Product;
			if (bprod != null && ei != null)
			{
				var list = new List<MathObject>();

				foreach (var elt in bprod.Elements)
					list.Add(elt ^ Exponent);

				return new Product(list).Simplify();
			}

			return new Power(Base, Exponent);
		}

		public override MathObject Numerator()
		{
			if (Exponent is Integer && Exponent < 0) return 1;

			if (Exponent is Fraction && Exponent < 0) return 1;

			return this;
		}

		public override MathObject Denominator()
		{
			if (Exponent is Integer && Exponent < 0) return this ^ -1;

			if (Exponent is Fraction && Exponent < 0) return this ^ -1;

			return 1;
		}

		public override int GetHashCode() => new {Bas = Base, Exp = Exponent}.GetHashCode();
	}
}