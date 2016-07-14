using System;

namespace Symbolism
{
	public class Integer : Number, IEquatable<Integer>
	{
		public static implicit operator Integer(int n) => new Integer(n);

		public int Value { get; }

		public Integer(int n) { Value = n; }
        
		public override string FullForm() => Value.ToString();

		public override bool Equals(object obj) => Equals(obj as Integer);

		public bool Equals(Integer obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return Value == obj.Value;
		}

		public override int GetHashCode() => Value.GetHashCode();

		public override DoubleFloat ToDouble() => new DoubleFloat(Value);
	}
}