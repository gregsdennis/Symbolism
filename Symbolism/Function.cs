using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public class Function : MathObject, IEquatable<Function>, INamedMathObject
	{
		public string Name { get; }

		public IReadOnlyList<MathObject> Parameters { get; }

		public delegate MathObject Proc(params MathObject[] ls);

		public Proc Procedure { get; }

		public Function(string name, Proc proc, params MathObject[] args)
			: this(name, proc, (IEnumerable<MathObject>) args) {}
		public Function(string name, Proc proc, IEnumerable<MathObject> args)
		{
			Name = name;
			Procedure = proc;
			Parameters = args.ToList();
		}

		public override bool Equals(object obj) => Equals(obj as Function);

		public bool Equals(Function obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return GetType() == obj.GetType() &&
				   Name == obj.Name &&
				   Parameters.SequenceEqual(obj.Parameters);
		}

		public override MathObject Simplify() => Procedure == null ? this : Procedure(Parameters.Select(p => p.Simplify()).ToArray());

		// TODO: the expand/simply split needs to happen with Procedure
		internal override MathObject Expand() => this;

		public override string ToString() => $"{Name}({string.Join(", ", Parameters)})";

		public virtual MathObject Map(Func<MathObject, MathObject> map)
		{
			// This is a minor hack since it can only be caught at run-time,
			// but it allows Parameters to be read only.
			if (GetType() != typeof(Function))
				throw new NotImplementedException($"Function.Map() must be overridden to return type {GetType()}");
			return new Function(Name, Procedure, Parameters.Select(map));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Parameters.GetCollectionHashCode() * 397 + Name.GetHashCode();
			}
		}
	}
}