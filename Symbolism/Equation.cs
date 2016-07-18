using System;
using System.Diagnostics;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	public class Equation : MathObject, IEquatable<Equation>
	{
		public enum Operators
		{
			Equal,
			NotEqual,
			LessThan,
			GreaterThan
		}

		public readonly MathObject a;
		public readonly MathObject b;

		public readonly Operators Operator;

		public Equation(MathObject x, MathObject y, Operators op = Operators.Equal)
		{
			a = x;
			b = y;
			Operator = op;
		}
		
		public override string FullForm()
		{
			if (Operator == Operators.Equal) return a + " == " + b;
			if (Operator == Operators.NotEqual) return a + " != " + b;
			if (Operator == Operators.LessThan) return a + " < " + b;
			if (Operator == Operators.GreaterThan) return a + " > " + b;
			throw new Exception();
		}

		public override bool Equals(object obj) => Equals(obj as Equation);

		public bool Equals(Equation obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return a.Equals(obj.a) &&
			       b.Equals(obj.b) &&
			       Operator == obj.Operator;
		}

		private bool ToBoolean()
		{
			// ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
			if (a is Bool && b is Bool) return ((Bool) a).Equals(b);

			if (a is Equation && b is Equation) return ((Equation) a).Equals(b);

			if (a is Integer && b is Integer) return ((Integer)a).Equals(b);
			if (a is DoubleFloat && b is DoubleFloat) return ((DoubleFloat)a).Equals(b);
			if (a is Symbol && b is Symbol) return ((Symbol)a).Equals(b);
			if (a is Sum && b is Sum) return ((Sum)a).Equals(b);
			if (a is Product && b is Product) return ((Product)a).Equals(b);
			if (a is Fraction && b is Fraction) return ((Fraction)a).Equals(b);
			if (a is Power && b is Power) return ((Power)a).Equals(b);
			if (a is Function && b is Function) return ((Function)a).Equals(b);
			// ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

			if (((object)a == null) && ((object)b == null)) return true;

			if ((object)a == null) return false;

			if ((object)b == null) return false;

			if (a.GetType() != b.GetType()) return false;

			Console.WriteLine("" + a.GetType() + " " + b.GetType());

			throw new Exception();
		}
		
		public static implicit operator bool(Equation eq)
		{
			if (eq.Operator == Operators.Equal)
				return (eq.a == eq.b).ToBoolean();

			if (eq.Operator == Operators.NotEqual)
				return !(eq.a == eq.b).ToBoolean();

			if (eq.Operator == Operators.LessThan)
			{
				var nA = eq.a as Number;
				var nB = eq.b as Number;
				if (nA != null && nB != null)
					return nA.ToDouble().Value < nB.ToDouble().Value;
			}

			if (eq.Operator == Operators.GreaterThan)
			{
				var nA = eq.a as Number;
				var nB = eq.b as Number;
				if (nA != null && nB != null)
					return nA.ToDouble().Value > nB.ToDouble().Value;
			}

			throw new Exception();
		}
		
		public MathObject Simplify()
		{
			if (a is Number && b is Number) return (bool)this;

			return this;
		}

		public override int GetHashCode() => new { a, b }.GetHashCode();
		
	}
}