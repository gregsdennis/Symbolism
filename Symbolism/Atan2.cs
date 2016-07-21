using System;

namespace Symbolism
{
	public class Atan2 : Function
	{
		private static MathObject Atan2Proc(params MathObject[] ls)
		{
			//if (
			//    (ls[0] is DoubleFloat || ls[0] is Integer)
			//    &&
			//    (ls[1] is DoubleFloat || ls[1] is Integer)
			//    )
			//    return new DoubleFloat(
			//        Math.Atan2(
			//            (ls[0] as Number).ToDouble().val,
			//            (ls[1] as Number).ToDouble().val));


			DoubleFloat d0 = ls[0] as DoubleFloat, d1 = ls[1] as DoubleFloat;
			Integer i0 = ls[0] as Integer, i1 = ls[1] as Integer;

			if (d0 != null && d1 != null)
				return new DoubleFloat(Math.Atan2(d0.Value,d1.Value));

			if (i0 != null && d1 != null)
				return new DoubleFloat(Math.Atan2(i0.Value, i1.Value));

			if (d0 != null && i1 != null)
				return new DoubleFloat(Math.Atan2(d0.Value, i1.Value));

			if (i0 != null && i1 != null)
				return new DoubleFloat(Math.Atan2(i0.Value, i1.Value));

			return new Atan2(ls[0], ls[1]);
		}

		public Atan2(MathObject a, MathObject b)
			: base("atan2", Atan2Proc, a, b) {}

		public override MathObject Map(Func<MathObject, MathObject> map) => new Atan2(map(Parameters[0]), map(Parameters[1]));
	}
}