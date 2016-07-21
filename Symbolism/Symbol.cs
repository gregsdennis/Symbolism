using System;

namespace Symbolism
{
	public class Symbol : MathObject, IEquatable<Symbol>
	{
		public string Name { get; }

		public Symbol(string str) { Name = str; }

		internal override MathObject Expand() => this;

		public override string ToString() => Name;

		public override int GetHashCode() => Name.GetHashCode();

		public override bool Equals(object obj) => Equals(obj as Symbol);

		public bool Equals(Symbol obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return Name == obj.Name;
		}
	}
}