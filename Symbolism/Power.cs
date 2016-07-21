using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	internal class Power : MathObject, IEquatable<Power>
	{
		public MathObject Base { get; }
		public MathObject Exponent { get; }

		public Power(MathObject a, MathObject b)
		{
			Base = a;
			Exponent = b;
		}

		public override string ToString()
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

		public override MathObject Simplify()
		{
			var simpleBase = Base.Simplify();
			var simpleExp = Exponent.Simplify();

			DoubleFloat bd = simpleBase as DoubleFloat, ed = simpleExp as DoubleFloat;
			Integer bi = simpleBase as Integer, ei = simpleExp as Integer;
			Fraction bf = simpleBase as Fraction, ef = simpleExp as Fraction;

			if (simpleBase == 0) return 0;
			if (simpleBase == 1) return 1;
			if (simpleExp == 0) return 1;
			if (simpleExp == 1) return simpleBase;

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
				return Rational.SimplifyRNE(new Power(simpleBase, simpleExp));

			if (bd != null && ei != null)
				return new DoubleFloat(Math.Pow(bd.Value, ei.Value));

			if (bd != null && ef != null)
				return new DoubleFloat(Math.Pow(bd.Value, ef.ToDouble().Value));

			if (bi != null && ed != null)
				return new DoubleFloat(Math.Pow(bi.Value, ed.Value));

			if (bf != null && ed != null)
				return new DoubleFloat(Math.Pow(bf.ToDouble().Value, ed.Value));

			var bpow = simpleBase as Power;
			if (bpow != null && ei != null)
				return (bpow.Base ^ (bpow.Exponent*simpleExp).Simplify()).Simplify();

			var bprod = simpleBase as Product;
			if (bprod != null && ei != null)
			{
				var list = new List<MathObject>();

				foreach (var elt in bprod.Elements)
					list.Add(elt ^ simpleExp);

				return new Product(list).Simplify();
			}

			return new Power(Base, simpleExp);
		}

		internal override MathObject Expand()
		{
			var bPower = Base.Expand() as Product;
			if (bPower != null) return new Product(bPower.Elements.Select(elt => elt ^ Exponent));

			return this;
		}

		public override int GetHashCode() => new {Bas = Base, Exp = Exponent}.GetHashCode();
	}
}