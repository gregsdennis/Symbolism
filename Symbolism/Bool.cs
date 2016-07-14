using System;

namespace Symbolism
{
	public class Bool : MathObject, IEquatable<Bool>
	{
		public static implicit operator Bool(bool val) => new Bool(val);

		public bool Value { get; }

		public Bool(bool b) { Value = b; }
        
		public override string FullForm() => Value.ToString();
        
		public override bool Equals(object obj) => Equals(obj as Bool);

		public bool Equals(Bool obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return Value == obj.Value;
		}
        
		public override int GetHashCode() => Value.GetHashCode();
	}
}