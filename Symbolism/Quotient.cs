using System.Collections.Generic;
using System.Diagnostics;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	class Quotient : MathObject
	{
		public List<MathObject> Elements { get; }

		public Quotient(params MathObject[] ls)
		{
			Elements = new List<MathObject>(ls);
		}

		public MathObject Simplify() => Elements[0]*(Elements[1] ^ -1);
	}
}