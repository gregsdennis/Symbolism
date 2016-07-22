using System;

namespace Symbolism
{
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

		public override string ToString()
		{
			if (Operator == Operators.Equal) return $"{a} == {b}";
			if (Operator == Operators.NotEqual) return $"{a} != {b}";
			if (Operator == Operators.LessThan) return $"{a} < {b}";
			if (Operator == Operators.GreaterThan) return $"{a} > {b}";
			throw new Exception();
		}

		public override bool Equals(object obj) => Equals(obj as Equation);

		public bool Equals(Equation obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return a.Equals(obj.a) && b.Equals(obj.b) &&
			       Operator == obj.Operator;
		}

		private bool CheckComponentEquality()
		{
			if (ReferenceEquals(null, a) && ReferenceEquals(null, b)) return true;
			if (ReferenceEquals(null, a) || ReferenceEquals(null, b)) return false;

			return Equals(a.Simplify(), b.Simplify());
		}
		
		public static implicit operator bool(Equation eq)
		{
			Number nA, nB;

			switch (eq.Operator)
			{
				case Operators.Equal:
					return (eq.a == eq.b).CheckComponentEquality();
				case Operators.NotEqual:
					return !(eq.a == eq.b).CheckComponentEquality();
				case Operators.LessThan:
					nA = eq.a as Number;
					nB = eq.b as Number;
					if (nA != null && nB != null)
						return nA.ToDouble().Value < nB.ToDouble().Value;
					throw new InvalidOperationException("Cannot perform comparisons of expressions that do not evaluate to numbers.");
				case Operators.GreaterThan:
					nA = eq.a as Number;
					nB = eq.b as Number;
					if (nA != null && nB != null)
						return nA.ToDouble().Value > nB.ToDouble().Value;
					throw new InvalidOperationException("Cannot perform comparisons of expressions that do not evaluate to numbers.");
				default:
					throw new ArgumentOutOfRangeException(nameof(Operator));
			}
		}

		public override MathObject Simplify()
		{
			return new Equation(a.Simplify(), b.Simplify(), Operator);
		}
		internal override MathObject Expand() => new Equation(a.Expand(), b.Expand(), Operator);

		public override int GetHashCode() => new {a, b}.GetHashCode();
	}
}