using System;

namespace Symbolism
{
	public class Tan : Function
	{
		private static MathObject TanProc(params MathObject[] ls)
		{
			var f = ls[0] as DoubleFloat;
			if (f != null)
				return new DoubleFloat(Math.Tan(f.Value));

			return new Tan(ls[0]);
		}

		public Tan(MathObject param)
			: base("tan", TanProc, param) {}

		public override MathObject Map(Func<MathObject, MathObject> map) => new Tan(map(Parameters[0]));
	}
}