using System;

namespace Symbolism
{
	public class Sqrt : Function
	{
		private static MathObject SqrtProc(MathObject[] ls)
		{
			return ls[0] ^ new Fraction(1, 2);
		}

		public Sqrt(MathObject param)
			: base("sqrt", SqrtProc, param) { }

		public override MathObject Map(Func<MathObject, MathObject> map) => new Sqrt(map(Parameters[0]));
	}
}