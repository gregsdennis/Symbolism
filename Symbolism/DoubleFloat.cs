using System;

namespace Symbolism
{
	public class DoubleFloat : Number, IEquatable<DoubleFloat>
	{
		public static implicit operator DoubleFloat(double val) => new DoubleFloat(val);

		public static double? Tolerance { get; set; }

		public double Value { get; }

		public DoubleFloat(double n) { Value = n; }

		public override string FullForm() => Value.ToString("R");

		//public bool EqualWithinTolerance(DoubleFloat obj)
		//{
		//    if (tolerance.HasValue)
		//        return Math.Abs(val - obj.val) < tolerance;

		//    throw new Exception();
		//}

		public override bool Equals(object obj) => Equals(obj as DoubleFloat);

		public bool Equals(DoubleFloat obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			if (Tolerance.HasValue)
				return Math.Abs(Value - obj.Value) < Tolerance;

			return Value == obj.Value;
		}

		public override int GetHashCode() => Value.GetHashCode();

		public override DoubleFloat ToDouble() => this;
	}
}