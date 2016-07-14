using System;

namespace Symbolism
{
	// TODO: Add Acos
	public class Asin : Function
	{
		private static MathObject AsinProc(params MathObject[] ls)
		{
			var f = ls[0] as DoubleFloat;
			if (f != null)
				return new DoubleFloat(Math.Asin(f.Value));

			return new Asin(ls[0]);
		}

		public Asin(MathObject param)
			: base("asin", AsinProc, param) {}

		public override MathObject Map(Func<MathObject, MathObject> map) => new Asin(map(Parameters[0])).Simplify();
	}
}