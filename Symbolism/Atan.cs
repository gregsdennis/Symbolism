using System;
using System.Diagnostics;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	public class Atan : Function
	{
		private static MathObject AtanProc(params MathObject[] ls)
		{
			var f = ls[0] as DoubleFloat;
			if (f != null)
				return new DoubleFloat(Math.Atan(f.Value));

			return new Atan(ls[0]);
		}

		public Atan(MathObject param)
			: base("atan", AtanProc, param) {}

		public override MathObject Map(Func<MathObject, MathObject> map) => new Atan(map(Parameters[0])).Simplify();
	}
}