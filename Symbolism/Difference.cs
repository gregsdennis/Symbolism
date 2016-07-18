using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	class Difference : MathObject
	{
		public List<MathObject> Elements { get; }

		public Difference(params MathObject[] ls)
			: this((IEnumerable<MathObject>) ls) {}
		public Difference(IEnumerable<MathObject> ls)
		{
			Elements = ls.ToList();
		}

		public MathObject Simplify()
		{
			if (Elements.Count == 1) return -1 * Elements[0];

			if (Elements.Count == 2) return Elements[0] + -1 * Elements[1];

			throw new Exception();
		}
	}
}