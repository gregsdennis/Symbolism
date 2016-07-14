using System;

namespace Symbolism
{
	public abstract class MathObject
	{
		//////////////////////////////////////////////////////////////////////

		public static implicit operator MathObject(int n) => new Integer(n);

		public static implicit operator MathObject(bool val) => new Bool(val);

		public static implicit operator MathObject(double val) => new DoubleFloat(val);

		//////////////////////////////////////////////////////////////////////

		#region overloads for 'int'

		public static MathObject operator +(MathObject a, int b) => a + new Integer(b);
		public static MathObject operator -(MathObject a, int b) => a - new Integer(b);
		public static MathObject operator *(MathObject a, int b) => a*new Integer(b);
		public static MathObject operator /(MathObject a, int b) => a/new Integer(b);
		public static MathObject operator ^(MathObject a, int b) => a ^ new Integer(b);
		public static MathObject operator +(int a, MathObject b) => new Integer(a) + b;
		public static MathObject operator -(int a, MathObject b) => new Integer(a) - b;
		public static MathObject operator *(int a, MathObject b) => new Integer(a)*b;
		public static MathObject operator /(int a, MathObject b) => new Integer(a)/b;
		public static MathObject operator ^(int a, MathObject b) => new Integer(a) ^ b;

		#endregion

		//////////////////////////////////////////////////////////////////////

		#region overloads for 'double'

		public static MathObject operator +(MathObject a, double b) => a + new DoubleFloat(b);
		public static MathObject operator -(MathObject a, double b) => a - new DoubleFloat(b);
		public static MathObject operator *(MathObject a, double b) => a*new DoubleFloat(b);
		public static MathObject operator /(MathObject a, double b) => a/new DoubleFloat(b);
		public static MathObject operator ^(MathObject a, double b) => a ^ new DoubleFloat(b);
		public static MathObject operator +(double a, MathObject b) => new DoubleFloat(a) + b;
		public static MathObject operator -(double a, MathObject b) => new DoubleFloat(a) - b;
		public static MathObject operator *(double a, MathObject b) => new DoubleFloat(a)*b;
		public static MathObject operator /(double a, MathObject b) => new DoubleFloat(a)/b;
		public static MathObject operator ^(double a, MathObject b) => new DoubleFloat(a) ^ b;

		#endregion

		//////////////////////////////////////////////////////////////////////
		public static Equation operator ==(MathObject a, MathObject b) => new Equation(a, b);
		public static Equation operator !=(MathObject a, MathObject b) => new Equation(a, b, Equation.Operators.NotEqual);
		public static Equation operator <(MathObject a, MathObject b) => new Equation(a, b, Equation.Operators.LessThan);
		public static Equation operator >(MathObject a, MathObject b) => new Equation(a, b, Equation.Operators.GreaterThan);

		public static Equation operator ==(MathObject a, double b) => new Equation(a, new DoubleFloat(b));
		public static Equation operator ==(double a, MathObject b) => new Equation(new DoubleFloat(a), b);

		public static Equation operator !=(MathObject a, double b) => new Equation(a, new DoubleFloat(b), Equation.Operators.NotEqual);
		public static Equation operator !=(double a, MathObject b) => new Equation(new DoubleFloat(a), b, Equation.Operators.NotEqual);

		public static Equation operator ==(MathObject a, int b) => new Equation(a, new Integer(b));
		public static Equation operator ==(int a, MathObject b) => new Equation(new Integer(a), b);
		public static Equation operator !=(MathObject a, int b) => new Equation(a, new Integer(b), Equation.Operators.NotEqual);
		public static Equation operator !=(int a, MathObject b) => new Equation(new Integer(a), b, Equation.Operators.NotEqual);
		//////////////////////////////////////////////////////////////////////
		public static MathObject operator +(MathObject a, MathObject b) => new Sum(a, b).Simplify();
		public static MathObject operator -(MathObject a, MathObject b) => new Difference(a, b).Simplify();
		public static MathObject operator *(MathObject a, MathObject b) => new Product(a, b).Simplify();
		public static MathObject operator /(MathObject a, MathObject b) => new Quotient(a, b).Simplify();
		public static MathObject operator ^(MathObject a, MathObject b) => new Power(a, b).Simplify();

		public static MathObject operator -(MathObject a)
		{
			return new Difference(a).Simplify();
		}

		// Precedence is used for printing purposes.
		// Thus, the precedence values below do not necessarily reflect 
		// the C# operator precedence values.
		// For example, in C#, the precedence of ^ is lower than +.
		// But for printing purposes, we'd like ^ to have a 
		// higher precedence than +.

		private int GetPrecedence()
		{
			if (this is Integer ||
				this is DoubleFloat ||
				this is Symbol ||
				this is Function ||
				this is Fraction) return 1000;
			if (this is Power) return 130;
			if (this is Product) return 120;
			if (this is Sum) return 110;

			throw new ArgumentOutOfRangeException();
		}

		private int? _precedence;

		public int Precedence => _precedence ?? (_precedence = GetPrecedence()).Value;

		public static ToStringForms ToStringForm = ToStringForms.Full;

		public virtual string FullForm() => base.ToString();

		public virtual string StandardForm() => FullForm();

		public override string ToString()
		{
			switch (ToStringForm)
			{
				case ToStringForms.Full:
					return FullForm();
				case ToStringForms.Standard:
					return StandardForm();
				default:
					throw new ArgumentOutOfRangeException(nameof(ToStringForm));
			}
		}

		// TODO: this should only be part of fractions (or an extension method).
		public virtual MathObject Numerator() => this;

		public virtual MathObject Denominator() => 1;

		public override bool Equals(object obj)
		{
			throw new InvalidOperationException("MathObject.Equals called - abstract class");
		}

		public override int GetHashCode() => base.GetHashCode();
	}
}